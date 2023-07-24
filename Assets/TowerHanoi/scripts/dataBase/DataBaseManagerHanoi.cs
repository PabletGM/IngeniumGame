using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using Firebase.Firestore;
using Firebase.Extensions;
using Firebase;

public class DataBaseManagerHanoi : MonoBehaviour
{
    #region fireBase arguments
    private FirebaseApp _app;
    #endregion

    #region realtime firebase
        private string userID;
        private DatabaseReference dbReference;
        //conexion con GameManager
        GameManagerHanoi _myGameManagerHanoi;
    #endregion


    //Start is called before the first frame update
    void Start()
    {
        //coge el identificador unico de cada ordenador 
        userID = SystemInfo.deviceUniqueIdentifier;
        //hace referencia a el firebase
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        //gameManager
        _myGameManagerHanoi = GameManagerHanoi.GetInstance();

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


    #region realTimeDataBase

            //creamos nuevo usuario en base de datos
            public void CreateUserInicial()
            {

                //recogemos info
                int TotalTime = _myGameManagerHanoi.GetTiempoTotalHanoiRegistrado();
                int numJugadas = _myGameManagerHanoi.GetnumJugadasTotalHanoiRegistrado();
                int numMovimientosIncorrectos = _myGameManagerHanoi.GetnumMovsIncorrectosHanoiRegistrado();
                int numMovimientosOutOfLimits = _myGameManagerHanoi.GetnumMovsOutOfLimitsHanoiRegistrado();


                //Habiendo puesto al principio el nombre y el correo electronico estará guardado en las 2 variables InputField
                //que se podrán acceder desde un método
                //despues debemos registrar con llamada a GameManager con otro método el time y el numero de ticks totales
                HanoiInfo newUser = new HanoiInfo(TotalTime, numJugadas, numMovimientosIncorrectos, numMovimientosOutOfLimits);
                //guardamos info del objeto como un string
                string json = JsonUtility.ToJson(newUser);
                //escribimos en carpeta users ese string con la info guardada
                dbReference.Child("pruebaHanoi").Child(userID).SetRawJsonValueAsync(json);
            }

            //metodo que se llama cuando se acaba la prueba y que actualiza los datos del usuario
            public void CreateUserTotal()
            {
                //recogemos info
                int TotalTime = _myGameManagerHanoi.GetTiempoTotalHanoiRegistrado();
                int numJugadas = _myGameManagerHanoi.GetnumJugadasTotalHanoiRegistrado();
                int numMovimientosIncorrectos = _myGameManagerHanoi.GetnumMovsIncorrectosHanoiRegistrado();
                int numMovimientosOutOfLimits = _myGameManagerHanoi.GetnumMovsOutOfLimitsHanoiRegistrado();


                //registramos los 2 valores que es nombre y correo en el objeto clase user
                //los otros 2 valores de 0 por ahora se quedan así
                HanoiInfo newUser = new HanoiInfo(TotalTime, numJugadas, numMovimientosIncorrectos, numMovimientosOutOfLimits);
                //guardamos info del objeto como un string
                string json = JsonUtility.ToJson(newUser);
                //escribimos en carpeta users ese string con la info guardada
                dbReference.Child("pruebaHanoi").Child(userID).SetRawJsonValueAsync(json);
            }


    #endregion

    public void LoginTutorial()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        //si el usuario se ha identificado antes currentUser poseerá sus datos
        if (auth.CurrentUser != null)
        {
            Debug.Log("Ya estaba autentificado");
            //se conecta con base de datos firebase local data(no realtime)
            AddDataFirestoreInfoHanoi();
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
            AddDataFirestoreInfoHanoi();
        });
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

    private void AddDataFirestoreInfoHanoi()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        #region recolectar documentos
        //hacemos referencia al documento de infoHoyos, concretamente al documento de la subcoleccion "numeros"
        DocumentReference docRefNumerosInfoHanoi = db.Collection("users").Document(auth.CurrentUser.UserId).Collection("infoHanoi").Document("infoHanoiDocument").Collection("numeros").Document("numerosDocument");
        //hacemos 3 diccionarios, numeros, strings y arrays para sobreescribir o cambiar los 3
        #endregion




        #region dictionary numeros
        int TotalTime = _myGameManagerHanoi.GetTiempoTotalHanoiRegistrado();
        int numJugadas = _myGameManagerHanoi.GetnumJugadasTotalHanoiRegistrado();
        int numMovimientosIncorrectos = _myGameManagerHanoi.GetnumMovsIncorrectosHanoiRegistrado();
        int numMovimientosOutOfLimits = _myGameManagerHanoi.GetnumMovsOutOfLimitsHanoiRegistrado();

        //el segundo es diccionario de numeros, donde hay un valor y un string
            Dictionary<string, object> numerosHanoi = new Dictionary<string, object>
            {
                //asignamos a mailPlayer el valor de MailPlayer.text
                //y asignamos a namePlayer el valor de NamePlayer.text

                    { "numJugadas" , numJugadas },
                    { "tiempoPlayer",  TotalTime},
                    { "numMovimientosOutOfLimits" , numMovimientosOutOfLimits },
                    { "numMovsIncorrect" , numMovimientosIncorrectos },
            };
        //cambiamos el documento stringDocument con diccionario string lo actualizamos con valores nuevos stringHoyos
        docRefNumerosInfoHanoi.UpdateAsync((IDictionary<string, object>)numerosHanoi).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the alovelace document in the users collection.");
            GetDataFirestore();
        });

        #endregion


    }

}



