using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;
using TMPro;
using System;

using Firebase.Firestore;
using Firebase.Extensions;
public class DataBaseManagerHoyos : MonoBehaviour
{
    #region realTime database arguments

    public InputField NamePlayer;
    public InputField MailPlayer;

    //public Text NamePlayerText;
    //public Text MailPlayerText;

    private string userID;
    private DatabaseReference dbReference;

    //conexion con GameManager
    GameManager _myGameManager;

    //numero de picadas en cada hoyo
    private int[] numpicadasHoyosIndiv;

    #endregion

    #region fireBase arguments
    private FirebaseApp _app;
    #endregion


    //Start is called before the first frame update
    void Start()
    {
        //coge el identificador unico de cada ordenador 
        userID = SystemInfo.deviceUniqueIdentifier;
        //hace referencia a el firebase
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        //gameManager
       _myGameManager = GameManager.GetInstance();

        #region ServiciosGooglePlay

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                _app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.


                //si todo ha funcionado bien decimos que llame a login
                LoginTutorial();
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

        #endregion
    }


    #region realTimeDatabase

    //creamos nuevo usuario en base de datos
            public void CreateUserInicial()
            {
                //Habiendo puesto al principio el nombre y el correo electronico estará guardado en las 2 variables InputField
                //que se podrán acceder desde un método
                //despues debemos registrar con llamada a GameManager con otro método el time y el numero de ticks totales

                //registramos los 2 valores que es nombre y correo en el objeto clase user
                //los otros 2 valores de 0 por ahora se quedan así
                int[] valorInicialNumeroPicadas = new int[0];
                HoyoInfo newUser = new HoyoInfo(NamePlayer.text, MailPlayer.text,0,0, valorInicialNumeroPicadas);
                //guardamos info del objeto como un string
                string json = JsonUtility.ToJson(newUser);
                //escribimos en carpeta users ese string con la info guardada
                dbReference.Child("pruebaHoyos").Child(userID).SetRawJsonValueAsync(json);
            }

            //metodo que se llama cuando se acaba la prueba y que actualiza los datos del usuario
            public void CreateUserTotal()
            {
                //declaramos tamaño array hoyos
                numpicadasHoyosIndiv = new int[6];
                //Habiendo puesto al principio el nombre y el correo electronico estará guardado en las 2 variables InputField
                //que se podrán acceder desde un método
                //despues debemos registrar con llamada a GameManager con otro método el time y el numero de ticks totales
                int totalTime = _myGameManager.NumSecsPartidaReturn();
                int numExcavacionesTotales = _myGameManager.NumExcavacionesTotales();
                //numero picadas totales cada hoyo
                numpicadasHoyosIndiv = _myGameManager.DevolverPicadasHoyo();

                //registramos los 2 valores que es nombre y correo en el objeto clase user
                //los otros 2 valores de 0 por ahora se quedan así
                HoyoInfo newUser = new HoyoInfo(NamePlayer.text, MailPlayer.text, numExcavacionesTotales, totalTime,numpicadasHoyosIndiv);
                //guardamos info del objeto como un string
                string json = JsonUtility.ToJson(newUser);
                //escribimos en carpeta users ese string con la info guardada
                dbReference.Child("pruebaHoyos").Child(userID).SetRawJsonValueAsync(json);
            }



            //obtener nombre de carpeta users, valor de namePlayer
            public IEnumerator GetNamePlayer(Action<string> onCallback)
            {
                //hacemos carpeta Users
                var userNamePlayerData = dbReference.Child("pruebaHoyos").Child(userID).Child("namePlayer").GetValueAsync();

                yield return new WaitUntil(predicate: () => userNamePlayerData.IsCompleted);

                if (userNamePlayerData != null)
                {
                    DataSnapshot snapshot = userNamePlayerData.Result;

                    onCallback.Invoke(snapshot.Value.ToString());
                }
            }

            //obtener numero de carpeta users, valor de mailPlayer
            public IEnumerator GetMailPlayer(Action<string> onCallback)
            {
                //hacemos carpeta Users
                var userMailPlayerData = dbReference.Child("pruebaHoyos").Child(userID).Child("mailPlayer").GetValueAsync();

                yield return new WaitUntil(predicate: () => userMailPlayerData.IsCompleted);

                if (userMailPlayerData != null)
                {
                    DataSnapshot snapshot = userMailPlayerData.Result;

                    onCallback.Invoke(snapshot.Value.ToString());
                }
            }

    ////coge datos de la base de datos namePlayer y mailPlayer, su resultado y lo escribe
    //public void GetUserInfo()
    //{
    //    StartCoroutine(GetMailPlayer((string mailPlayer) => {
    //        MailPlayerText.text = "Mail: " + mailPlayer.ToString();


    //    }));

    //    StartCoroutine(GetNamePlayer((string namePlayer) => {
    //        NamePlayerText.text = "Name: " + namePlayer.ToString();


    //    }));


    //}

    //public void UpdateName()
    //{
    //    dbReference.Child("users").Child(userID).Child("namePlayer").SetValueAsync(NamePlayerText.text);
    //}

    //public void UpdateGold()
    //{
    //    dbReference.Child("users").Child(userID).Child("mailPlayer").SetValueAsync(MailPlayerText.text);
    //}

    #endregion


    #region dataBase

    //aqui se crea data de los hoyos
    private void AddDataFirestoreInfoHoyos()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        #region recolectar documentos
            //hacemos referencia al documento de infoHoyos, concretamente al documento de la subcoleccion "numeros"
            DocumentReference docRefNumerosInfoHoyo = db.Collection("users").Document(auth.CurrentUser.UserId).Collection("infoHoyos").Document("infoHoyosDocument").Collection("numeros").Document("numerosDocument");

            //hacemos referencia al documento de infoHoyos, concretamente al documento de la subcoleccion "strings"
            DocumentReference docRefStringsInfoHoyo = db.Collection("users").Document(auth.CurrentUser.UserId).Collection("infoHoyos").Document("infoHoyosDocument").Collection("strings").Document("stringDocument");

            //hacemos referencia al documento de infoHoyos, concretamente al documento de la subcoleccion "array"
            DocumentReference docRefArrayInfoHoyo = db.Collection("users").Document(auth.CurrentUser.UserId).Collection("infoHoyos").Document("infoHoyosDocument").Collection("array").Document("arrayDocument");

            //hacemos 3 diccionarios, numeros, strings y arrays para sobreescribir o cambiar los 3
        #endregion

        #region dictionary string
        //el primero es diccionario de strings
        Dictionary<string, object> stringHoyos = new Dictionary<string, object>
            {
                //asignamos a mailPlayer el valor de MailPlayer.text
                //y asignamos a namePlayer el valor de NamePlayer.text
                    { "mailPlayer", MailPlayer.text },
                    { "namePlayer",  NamePlayer.text},
            };
            //cambiamos el documento stringDocument con diccionario string lo actualizamos con valores nuevos stringHoyos
            docRefStringsInfoHoyo.UpdateAsync(stringHoyos).ContinueWithOnMainThread(task => {
                Debug.Log("Added data to the alovelace document in the users collection.");
                GetDataFirestore();
            });
        #endregion


        #region dictionary numeros
            int totalTime = _myGameManager.NumSecsPartidaReturn();
            int numExcavacionesTotales = _myGameManager.NumExcavacionesTotales();
            //el segundo es diccionario de numeros, donde hay un valor y un string
            Dictionary<string, int> numerosHoyos = new Dictionary<string, int>
            {
                //asignamos a mailPlayer el valor de MailPlayer.text
                //y asignamos a namePlayer el valor de NamePlayer.text

                    { "numExcavacionesTotales" , numExcavacionesTotales },
                    { "totalTime",  totalTime},
            };
            //cambiamos el documento stringDocument con diccionario string lo actualizamos con valores nuevos stringHoyos
            docRefNumerosInfoHoyo.UpdateAsync((IDictionary<string, object>)numerosHoyos).ContinueWithOnMainThread(task => {
                Debug.Log("Added data to the alovelace document in the users collection.");
                GetDataFirestore();
            });

        #endregion

        #region dictionary array
            //declaramos tamaño array hoyos
            numpicadasHoyosIndiv = new int[6];
            //numero picadas totales cada hoyo
            numpicadasHoyosIndiv = _myGameManager.DevolverPicadasHoyo();
            //el primero es diccionario de strings
                Dictionary<string, Array> arrayHoyos = new Dictionary<string, Array>
                {
                        { "numPicadasCadaHoyo",numpicadasHoyosIndiv },
                };
            //cambiamos el documento stringDocument con diccionario string lo actualizamos con valores nuevos stringHoyos
            docRefArrayInfoHoyo.UpdateAsync((IDictionary<string, object>)arrayHoyos).ContinueWithOnMainThread(task => {
                Debug.Log("Added data to the alovelace document in the users collection.");
                GetDataFirestore();
            });
        #endregion


    }

    private void GetDataFirestore()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        //cogemos la coleccion users y tenemos unareferencia a esta usersRef
        CollectionReference usersRef = db.Collection("users");
        //coge como esté la base de datos ahora mismo y te la descargas
        usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            //recorremos nuestro documento
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Debug.Log(message: $"User: {document.Id}");
                Dictionary<string, object> documentDictionary = document.ToDictionary();
                Debug.Log(message: $"First: {documentDictionary["First"]}");
                if (documentDictionary.ContainsKey("Middle"))
                {
                    Debug.Log(message: $"Middle: {documentDictionary["Middle"]}");
                }

                Debug.Log(message: $"Last: {documentDictionary["Last"]}");
                Debug.Log(message: $"Born: {documentDictionary["Born"]}");
            }

            Debug.Log("Read all data from the users collection.");
        });
    }

    public void LoginTutorial()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        //si el usuario se ha identificado antes currentUser poseerá sus datos
        if (auth.CurrentUser != null)
        {
            Debug.Log("Ya estaba autentificado");
            //se conecta con base de datos firebase local data(no realtime)
            AddDataFirestoreInfoHoyos();
            return;
        }
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            //se conecta con base de datos firebase local data(no realtime)
            AddDataFirestoreInfoHoyos();
        });
    }
    #endregion

}
