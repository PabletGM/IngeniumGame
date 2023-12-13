using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using static System.Net.WebRequestMethods;

public class UIManagerLogin : MonoBehaviour
{
    //singleton
    static private UIManagerLogin _instanceUILogin;

    private string errorCode = "";

    private string access_tokenEntreEscenas = "";

    #region CambiarPanelLoginRegister
    [SerializeField]
    private GameObject loginPanel;

    [SerializeField]
    private GameObject registrationPanel;


    [SerializeField]
    private GameObject buttonChangeStateLoginToRegister;

    [SerializeField]
    private GameObject buttonChangeStateRegisterToLogin;

    #endregion

    #region InputFieldParametersRegister

    [SerializeField]
    private TMP_InputField userNameRegister;

    [SerializeField]
    private TMP_InputField company;

    [SerializeField]
    private TMP_InputField emailRegister;

    [SerializeField]
    private TMP_InputField emailLogin;

    [SerializeField]
    private TMP_InputField firstName;

    [SerializeField]
    private TMP_InputField lastName;

    [SerializeField]
    private TMP_InputField age;

    [SerializeField]
    private TMP_InputField passwordRegister;

    [SerializeField]
    private TMP_InputField confirmPasswordRegister;

    private int numeroCaracteresMinContraseñaRegister = 4;

    #endregion

    #region InputFieldParametersLogin

    [SerializeField]
    private TMP_InputField userNameLogin;

    [SerializeField]
    private TMP_InputField passwordLogin;

    [SerializeField]
    private GameObject loading;

    #endregion

    #region Pop-UpsLoginRegisterGO
    [SerializeField]
    private GameObject popUpLoginFallo;
    [SerializeField]
    private GameObject popUpRegisterFallo;
    #endregion

    #region urlConexionMongo

    private string uriBackend = "https://simplebackendingenuity.onrender.com/";

    private string uriRegisterBackend;
    private string uriLoginBackend;
    private string uriRegister = "Users/register";
    private string uriLogin = "Users/login";
    
    #endregion

    private void Awake()
    {
        uriRegisterBackend = uriBackend + uriRegister;
        uriLoginBackend = uriBackend + uriLogin;
        //si la instancia no existe se hace este script la instancia
        if (_instanceUILogin == null)
        {
            _instanceUILogin = this;
        }
        //si la instancia existe , destruimos la copia
        else
        {
            Destroy(this.gameObject);
        }
    }

    static public UIManagerLogin GetInstanceUI()
    {
        return _instanceUILogin;
    }


    #region OpenPanelsMethods
    //quitas el panel de registro y pones el de login
    public void OpenLoginPanel()
    {
        loginPanel.SetActive(true);
        registrationPanel.SetActive(false);
    }

    //quitas el panel de login y pones el de register
    public void OpenRegistrationPanel()
    {
        registrationPanel.SetActive(true);
        loginPanel.SetActive(false);
    }

    #endregion

    #region DebugLoginRegister

        //metodo que escribe parametros de Login
        [Obsolete]
        public void DebugLoginParameters()
        {
            //metodo que envia a la base de datos un post del Login
            StartCoroutine(PostLogin(emailLogin.text, passwordLogin.text));
        }

        //metodo que escribe parametros de Registers
        [Obsolete]
        public void DebugRegisterParameters()
        {
            StartCoroutine(PostRegister(userNameRegister.text, emailRegister.text, passwordRegister.text, company.text));
        }

    #endregion

    #region ExecuteLoginRegister
    
        [Obsolete]
        IEnumerator PostLogin(string email, string passwordLogin)
        {
            // Crear formulario con los datos, todo en minusculas , porque va predefinido el formulario y username esta vez en minuscula
            Debug.Log(email);
            Debug.Log(passwordLogin);
            WWWForm form = new WWWForm();
            form.AddField("username", email);
            form.AddField("password", passwordLogin);


            using (UnityWebRequest request = UnityWebRequest.Post(uriLoginBackend, form))
            {
                        
                yield return request.SendWebRequest();
                
                //primera barrera de seguridad, para ver fallo
                TipoFalloDetailLogin(request.downloadHandler.text);
                
                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.Log(request.error);
                    Debug.Log("holaaa");
                    errorCode = request.error;
                    //segunda barrera de seguridad, fallo numerico
                    TiposFalloLoginNumero(errorCode);
                }
                else
                {
                    
                    ComprobacionAccessTokenLoginCorrect(request.downloadHandler.text);
                    //en caso de que sea correcto nos movemos a escena hoyos
                    //SceneManager.LoadScene("EscenaInicial3EnRaya");
                    Debug.Log("login hecho");
                    LevelLoader.LoadLevel("tareaCaras2");
                }
            }
        }


        public void Loading()
        {
            //activamos loading
            loading.SetActive(true);
        }

        [Obsolete]
        IEnumerator PostRegister(string userNameRegister, string email, string passwordRegister, string company)
        {

                // Cambia esto al valor adecuado de la edad
                string body;
            //
            // body = $@"{{
            //    ""userName"": ""{userNameRegister}"",
            //    ""company"": ""{company}"",
            //    ""email"": ""{email}"",
            //    ""password"": ""{passwordRegister}""
            //}}";
            Debug.Log(email);
                body = $@"{{
                            ""email"": ""{email}"",
                            ""firstName"": ""{firstName}"",
                            ""lastName"": ""{lastName}"",
                            ""age"": ""0"",
                            ""password"": ""{passwordRegister}""
                        }}";

        using (UnityWebRequest request = UnityWebRequest.Post(uriRegisterBackend, body, "application/json"))
            {
                yield return request.SendWebRequest();
              

                //primera barrera de seguridad para ver fallo
                TipoFalloDetailRegister(request.downloadHandler.text);

                //si es incorrecto, esto es si solicitud no llega a base de datos
                if (request.isNetworkError || request.isHttpError)
                {  
                    //ponemos errorCode
                    errorCode = request.error;
                    TiposFalloRegisterNumerico(errorCode);
                    Debug.Log("ERRORRRRR");
                }
                //si la solicitud llega a base de datos vemos el texto que devuelve
                else
                {
                    
                    //comprobamos si es correcto el register
                    Comprobacion201RegisterCorrect(request.downloadHandler.text);
                    //vamos al login
                    Invoke("OpenLoginPanel", 1f);
                    
                }
            }

        }

    #endregion


    #region ComprobationLoginRegisterCorrect
    //metodo que mira a ver si lo que ha devuelto el register es un codigo 201, esto es register correct
    public void Comprobacion201RegisterCorrect(string registerCorrect)
    {

        // Deserializar el JSON usando JsonUtility
        JsonResponseData response = JsonUtility.FromJson<JsonResponseData>(registerCorrect);

        // Acceder al valor del campo status_code
        int statusCode = response.status_code;
        //si devuelve 201 es correcto
        if(statusCode == 201)
        {
            errorCode = "Status Code: " + statusCode;
            Debug.Log("Status Code: " + statusCode);
        }
        

    }

    //metodo que mira a ver si lo que ha devuelto el register es un codigo 201, esto es register correct
    public void ComprobacionAccessTokenLoginCorrect(string loginCorrect)
    {
        // Deserializar el JSON usando JsonUtility
        JsonResponseData response = JsonUtility.FromJson<JsonResponseData>(loginCorrect);

        // Acceder al valor del campo status_code
        string token = response.access_token;
        SetAccessToken(token);

        Debug.Log("Access token: " + "" + token + "");

    }


    #endregion


    #region TiposLoginIncorrecto
        //metodo que mira a ver si lo que ha devuelto el register es un codigo 201, esto es register correct
        public void TipoFalloDetailLogin(string detailText)
        {
            // Deserializar el JSON usando JsonUtility
            JsonResponseData response = JsonUtility.FromJson<JsonResponseData>(detailText);
            // Acceder al valor del campo detail
            string detail = response.detail;

            //es que es correcto el login
            if(detail==null)
            {
                Debug.Log("El login es correcto, cargando...");
                LoginCorrecto(detail);
            }

            //Segun el detail lo clasificamos de 3 maneras
            switch (detail)
            {

                //significa que la contraseña es incorrecta
                case "Incorrect password":
                    Debug.Log("La contraseña es incorrecta, pruebe otra vez...");
                    ContraseñaIncorrecta(detail);
                    break;

                //significa que el usuario está mal escrito, si pones contraseña y usuario mal, te salta este
                case "User not found":
                    Debug.Log("El usuario está mal escrito...");
                    UsuarioIncorrecto(detail);
                    break;

                default:
                    Console.WriteLine("It's something else.");
                    break;
            }

        }

        //metodo que escribe por pantalla contraseña incorrecta
        public void ContraseñaIncorrecta(string mensaje)
        {
            CambiarMensajeLogin(mensaje);
            
        }

        //metodo que escribe por pantalla usuario incorrecta
        public void UsuarioIncorrecto(string mensaje)
        {
            CambiarMensajeLogin(mensaje);
        }

        //metodo que escribe por pantalla login correcto
        public void LoginCorrecto(string mensaje)
        {
            CambiarMensajeLogin(mensaje);
        }

        //switch con tipos de fallo, ahora con numeros, como comprobacion extra
        public void TiposFalloLoginNumero(string errorDevuelto)
        {
                //HTTP / 1.1 401 Unauthorized     contraseña incorrecta
                //HTTP/1.1 404 Not Found          user incorrecto(si ambos estan mal sale este
            switch (errorDevuelto)
            {
                case "HTTP/1.1 401 Unauthorized":
                    ContraseñaIncorrecta("Contraseña incorrecta");
                    break;

                case "HTTP/1.1 404 Not Found":
                    UsuarioIncorrecto("Usuario incorrecto");
                    break;

                default:
                    Console.WriteLine("It's something else.");
                    break;
            }
        }
    #endregion

    #region TiposRegisterIncorrecto


        public void TipoFalloDetailRegister(string detailText)
        {
            // Deserializar el JSON usando JsonUtility
            JsonResponseData response = JsonUtility.FromJson<JsonResponseData>(detailText);
            // Acceder al valor del campo detail
            string detail = response.detail;
            //Segun el detail lo clasificamos de 3 maneras
            switch (detail)
            {

                //el usuario ya existe y está cogido
                case "The username is already taken":
                    Debug.Log("Usuario ya existe...");
                    UsuarioYaExiste(detail);
                    break;

               
                //cualquier error de creacion del body para hacer solicitud register(por ejemplo username muy corto)
                case "":
                    Debug.Log("error en creacion del usuario, todo minimo 3 caracteres...");
                    ErrorCreacionUsuarioRegister("error en creacion del usuario, todo minimo 3 caracteres...");
                    break;


                //el usuario se ha creado con exito
                case "The user has been created successfully":
                    Debug.Log("The user has been created successfully");
                    RegisterCorrecto(detail);
                    break;
            }

        }
        public void TiposFalloRegisterNumerico(string errorCode)
        {
            
            switch (errorCode)
            {
                //usuario ya existe
                case "HTTP/1.1 409 Conflict":
                    //fallo de register porque usuario ya existente
                    Debug.Log("Usuario ya existe");
                    UsuarioYaExiste("Usuario ya existe");
                    break;

                //error en creacion de usuario y solicutud register
                case "HTTP/1.1 422 Unprocessable Entity":
                    Debug.Log("Error en creacion de usuario, que todo tenga 3 caracteres minimo");
                    ErrorCreacionUsuarioRegister("Error, que todo tenga 3 caracteres minimo");
                    break;

                case "Status Code: 201":
                    Debug.Log("BIEN...el register se ha hecho correctamente, ahora login...");
                    RegisterCorrecto("el register se ha hecho correctamente");
                    break;

                
                default:
                        Console.WriteLine("It's something else.");
                        break;
            }
        }

        //metodo que escribe por pantalla contraseña incorrecta
        public void UsuarioYaExiste(string mensaje)
        {
            CambiarMensajeRegister(mensaje);
            
        }

        //metodo que escribe por pantalla usuario incorrecta
        public void ErrorCreacionUsuarioRegister(string mensaje)
        {
            CambiarMensajeRegister(mensaje);
        }

        //metodo que escribe por pantalla login correcto
        public void RegisterCorrecto(string mensaje)
        {
            CambiarMensajeRegister(mensaje);
        }

    #endregion

    #region PopUpRegister
        //Activa/Desactiva popUp Login
        public void SetPopUpRegister(bool set)
        {
            popUpRegisterFallo.SetActive(set);
        }

        public void DesactivarPopUpRegister()
        {
            popUpRegisterFallo.SetActive(false);
        }
        
        //Cambiar popUpLogin Mensaje
        public void CambiarMensajeRegister(string newMessage)
        {
            SetPopUpRegister(true);
            popUpRegisterFallo.GetComponentInChildren<TextMeshProUGUI>().text = newMessage;
            Invoke("DesactivarPopUpRegister", 1.5f);
        }
    #endregion

    #region PopUpLogin
        //Activa/Desactiva popUp Login
        public void SetPopUpLogin(bool set)
        {
            popUpLoginFallo.SetActive(set);
        }

        public void DesactivarPopUpLogin()
        {
            popUpLoginFallo.SetActive(false);
        }
        //Cambiar popUpLogin Mensaje
        public void CambiarMensajeLogin(string newMessage)
        {
            SetPopUpLogin(true);
            popUpLoginFallo.GetComponentInChildren<TextMeshProUGUI>().text = newMessage;
            Invoke("DesactivarPopUpLogin", 1.5f);
        }
    #endregion

    #region DevolverPoner_access_token
    public string GetAccessToken()
    {
        return access_tokenEntreEscenas;
    }
    public void SetAccessToken(string newToken)
    {
        access_tokenEntreEscenas = newToken;
    }
    #endregion
}


//variables que almacenan comprobaciones de si se ha loggeado bien y registrado bien
[System.Serializable]
public class JsonResponseData
{
    //status code correcto register 201
    public int status_code;
    //token de login para ver que loggea bien
    public string access_token;
    //para coger strings concretos de posibles fallos
    public string detail;

}
