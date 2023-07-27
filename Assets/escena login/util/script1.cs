using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UserData;

using Firebase.Firestore;
using Firebase.Extensions;
using Firebase;
using Firebase.Database;
using System.Threading.Tasks;

using Firebase.Auth;




public class Usuario
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
}

public class script1 : MonoBehaviour
{
    /*
    private string username;
    private string password;
    */
    private FirebaseApp _app;
    private UserData userData;

    // Start is called before the first frame update
    void Start()
    {
        //userData = FindObjectOfType<UserData>();
        userData = new UserData();

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
                //LoginTutorial();
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

    private void UpdateDataFirestoreInfoHanoi()
    {
        //cpnsulta a la base de datos para coger el documento donde está el usuario y modificar y actualizar ese usuario sin crear uno nuevo
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        #region dictionary login
        //el segundo es diccionario de numeros, donde hay un valor y un string
            Dictionary<string, object> login = new Dictionary<string, object>
            {
                //asignamos a mailPlayer el valor de MailPlayer.text
                //y asignamos a namePlayer el valor de NamePlayer.text

                    { "username", userData.username},
                    { "password", userData.password},


            };
        #endregion


        // Referencia a la colección "usuarios"
        CollectionReference usuariosRef = db.Collection("users");
        DocumentReference dr;

        usuariosRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            // Recorre los documentos obtenidos y encuentra nuestro usuario
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Dictionary<string, object> documentDictionary = document.ToDictionary();


                if (documentDictionary.ContainsKey("username") && (string)documentDictionary["username"] == userData.username)
                {
                    DocumentReference dr = document.Reference;

                    dr.UpdateAsync((IDictionary<string, object>)login).ContinueWithOnMainThread(task => {
                        Debug.Log("Added data to the alovelace document in the users collection.");

                    });
                    Debug.Log(documentDictionary["username"] + " " + documentDictionary["password"]);
                }


            }
        });
    }

    private bool ComprobacionExiste()
    {
        bool flag = false;
        //cpnsulta a la base de datos para coger el documento donde está el usuario y modificar y actualizar ese usuario sin crear uno nuevo
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        #region dictionary login
        //el segundo es diccionario de numeros, donde hay un valor y un string
            Dictionary<string, object> login = new Dictionary<string, object>
            {
                //asignamos a mailPlayer el valor de MailPlayer.text
                //y asignamos a namePlayer el valor de NamePlayer.text

                    { "username", userData.username},
                    { "password", userData.password},


            };

        #endregion

        // Referencia a la colección "usuarios"
        CollectionReference usuariosRef = db.Collection("users");
        DocumentReference dr;

        usuariosRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            // Recorre los documentos obtenidos y encuentra nuestro usuario
            QuerySnapshot snapshot = task.Result;
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Dictionary<string, object> documentDictionary = document.ToDictionary();


                if (documentDictionary.ContainsKey("username") && (string)documentDictionary["username"] == userData.username)
                {
                    DocumentReference dr = document.Reference;
                    flag = true;
                    
                }
            }
            
        });
        return flag;
    }

    //obtiene usuarios de una coleccion concreta, como todos los documentos de una coleccion
    public void ObtenerUsuarios()
    {
       
       
    }

    public void LoginTutorial()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        //si el usuario se ha identificado antes currentUser poseerá sus datos

        Debug.Log(auth.CurrentUser.UserId);
        //ya esta registrado el usuario, existe, actualizamos datos
        if (ComprobacionExiste())
        {
            Debug.Log("Ya estaba autentificado");
            //se conecta con base de datos firebase local data(no realtime)
            //AddDataFirestoreInfoHanoi();
            UpdateDataFirestoreInfoHanoi();
            return;
        }
        //sino existe el usuario lo crea
        else
        {
            //se conecta con base de datos firebase local data(no realtime)
            AddDataFirestoreInfoHanoi();
        }

        
    }

    private void AddDataFirestoreInfoHanoi()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        #region dictionary login


        //el segundo es diccionario de numeros, donde hay un valor y un string
        Dictionary<string, object> login = new Dictionary<string, object>
        {
            //asignamos a mailPlayer el valor de MailPlayer.text
            //y asignamos a namePlayer el valor de NamePlayer.text

                { "username", userData.username},
                { "password", userData.password},


        };

        db.Collection("users").AddAsync(login).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the alovelace document in the users collection.");
            //GetDataFirestore();
        });

        #endregion

    }

    public void ReadStringEmail(string s)
    {
        //username = s;
        userData.username = s;
        Debug.Log("username: " +  userData.username);
    }
    public void ReadStringPassword(string s)
    {
        //password = s;
        userData.password = s;
        
        Debug.Log("password: " +  userData.password);
    }

    public void SendAll()
    {
        Debug.Log("username: " +  userData.username  + "\npassword: " +  userData.password );
    }

    /*
    public void CambioScenaDos()
    {
        Debug.Log("cambiode escena a la 2");
        SceneManager.LoadScene("scene2");
    }
    */
    
}
