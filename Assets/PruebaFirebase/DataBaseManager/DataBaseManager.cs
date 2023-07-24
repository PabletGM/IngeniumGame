using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.UI;
using TMPro;
using System;


public class DataBaseManager : MonoBehaviour
{
    public InputField Name;
    public InputField Gold;

    public Text NameText;
    public Text GoldText;

    private string userID;
    private string CustomUserID;
    private DatabaseReference dbReference;
    #region tutorial
        private FirebaseApp _app;
    #endregion


    //Start is called before the first frame update
    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;

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

    private void AddDataFirestore()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(auth.CurrentUser.UserId);
        Dictionary<string, object> user = new Dictionary<string, object>
        {
                { "First", "QQQ" },
                { "Last", "Lovelace" },
                { "Born", 1815 },
        };
        docRef.UpdateAsync(user).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the alovelace document in the users collection.");

            GetDataFirestore();
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
                Debug.Log(message:$"User: {document.Id}");
                Dictionary<string, object> documentDictionary = document.ToDictionary();
                Debug.Log(message: $"First: {documentDictionary["First"]}");
                if (documentDictionary.ContainsKey("Middle"))
                {
                    Debug.Log(message: $"Middle: {documentDictionary["Middle"]}");
                }

                Debug.Log(message: $"Last: { documentDictionary["Last"]}");
                Debug.Log(message: $"Born: {documentDictionary["Born"]}");
            }

            Debug.Log("Read all data from the users collection.");
        });
    }

    private void LoginTutorial()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        //si el usuario se ha identificado antes currentUser poseerá sus datos
        if(auth.CurrentUser!=null)
        {
            Debug.Log("Ya estaba autentificado");
            //se conecta con base de datos firebase local data(no realtime)
            AddDataFirestore();
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
            AddDataFirestore();
        });
    }

    //creamos nuevo usuario en base de datos
    public void CreateUser()
    {
        //registramos los 2 valores que es nombre y numero en el objeto
        User newUser = new User(Name.text, int.Parse(Gold.text));
        //guardamos info del objeto como un string
        string json = JsonUtility.ToJson(newUser);
        //escribimos en carpeta users ese string con la info guardada
        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }

    


    //obtener nombre de carpeta users
    public IEnumerator GetName(Action<string> onCallback)
    {
        //hacemos carpeta Users
        var userNameData = dbReference.Child("users").Child(userID).Child("name").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if(userNameData!=null)
        {
            DataSnapshot snapshot = userNameData.Result;

            onCallback.Invoke(snapshot.Value.ToString());
        }
    }

    //obtener numero de carpeta users
    public IEnumerator GetGold(Action<int> onCallback)
    {
        //hacemos carpeta Users
        var userGoldData = dbReference.Child("users").Child(userID).Child("gold").GetValueAsync();

        yield return new WaitUntil(predicate: () => userGoldData.IsCompleted);

        if (userGoldData != null)
        {
            DataSnapshot snapshot = userGoldData.Result;

            onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
        }
    }

    //coge datos de la base de datos gold y name y pone su resultado y lo escribe
    public void GetUserInfo()
    {
        StartCoroutine(GetGold((int gold) => {
            GoldText.text = "Gold: " + gold.ToString();


        }));

        StartCoroutine(GetName((string name) => {
            NameText.text = "Name: " +name;


        }));

        
    }

    public void UpdateName()
    {
        dbReference.Child("users").Child(userID).Child("name").SetValueAsync(Name.text);
    }

    public void UpdateGold()
    {
        dbReference.Child("users").Child(userID).Child("gold").SetValueAsync(Gold.text);
    }


   
}
