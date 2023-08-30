using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class UIManagerLogin : MonoBehaviour
{
    //singleton
    static private UIManagerLogin _instanceUILogin;

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
    private TMP_InputField email;

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

    #endregion

    #region InputFieldParametersLogin

    [SerializeField]
    private TMP_InputField userNameLogin;

    [SerializeField]
    private TMP_InputField passwordLogin;

    #endregion

    #region urlConexionMongo

    
    private string uriRegister = "http://192.168.1.135:8000/Users/register";
    private string uriLogin = "http://192.168.1.135:8000/Users/login";

    #endregion

    private void Awake()
    {
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

    //metodo que escribe parametros de Login
    public void DebugLoginParameters()
    {
        Debug.Log(userNameLogin.text);
        Debug.Log(passwordLogin.text);

        //metodo que envia a la base de datos un post del Login
        StartCoroutine(PostLogin(userNameLogin.text, passwordLogin.text));
    }

    IEnumerator PostLogin(string userNameLogin, string passwordLogin)
    {
        //direccion con base de datos de MongoDB
        //string uri = "https://eu-west-1.aws.data.mongodb-api.com/app/ingenuity-app-0-mmgty/endpoint/Ingenuity/Test";
        //se pone el form en el segundo argumento de la funcion post
        //string body = $"{{ \"username\": {s}, \"password\": \"1234\", \"field3\": {{\"field3.-1\": {num},\"field3.0\": [{numbersArrayString}],\"field3.1\": [\"uno\",1], \"field3.2\": \"dos\"}} }}";
        //string userName = "dtertre59";
        //string password = "1234";
        //string body = $@"{{
        //    ""username"": ""{userName}"",
        //    ""password"": ""{password}""
        //}}";

        // Crear formulario con los datos, todo en minusculas , porque va predefinido el formulario y username esta vez en minuscula
        WWWForm form = new WWWForm();
        form.AddField("username", userNameLogin);
        form.AddField("password", passwordLogin);

        



        using (UnityWebRequest request = UnityWebRequest.Post(uriLogin, form))
        {
            yield return request.SendWebRequest();
            /*
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }*/
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                Debug.Log("ERRORRRRR");
            }
            else
            {
                Debug.Log( request.downloadHandler.text);
                ComprobacionAccessTokenLoginCorrect(request.downloadHandler.text);
                Debug.Log("BIEN");
            }
        }

    }

    //metodo que escribe parametros de Registers
    public void DebugRegisterParameters()
    {
        Debug.Log(userNameRegister.text);
        Debug.Log(company.text);
        Debug.Log(email.text);
        Debug.Log(firstName.text);
        Debug.Log(lastName.text);
        Debug.Log(age.text);
        Debug.Log(passwordRegister.text);
        Debug.Log(confirmPasswordRegister.text);

        StartCoroutine(PostRegister(userNameRegister.text , company.text, email.text, firstName.text, lastName.text , age.text, passwordRegister.text, confirmPasswordRegister.text));
        
    }


    IEnumerator PostRegister(string userNameRegister, string company, string email, string firstName, string lastName, string age, string passwordRegister, string confirmPasswordRegister)
    {
        //direccion con base de datos de MongoDB
        //string uri = "https://eu-west-1.aws.data.mongodb-api.com/app/ingenuity-app-0-mmgty/endpoint/Ingenuity/Test";
        //acceso a user register

        //se pone el form en el segundo argumento de la funcion post
        //string body = $"{{ \"username\": {s}, \"password\": \"1234\", \"field3\": {{\"field3.-1\": {num},\"field3.0\": [{numbersArrayString}],\"field3.1\": [\"uno\",1], \"field3.2\": \"dos\"}} }}";

        // Cambia esto al valor adecuado de la edad
        string body;
        if (int.TryParse(age, out int age2))
        {
             body = $@"{{
                ""userName"": ""{userNameRegister}"",
                ""company"": ""{company}"",
                ""email"": ""{email}"",
                ""firstName"": ""{firstName}"",
                ""lastName"": ""{lastName}"",
                ""age"": {age2},
                ""password"": ""{passwordRegister}""
            }}";
        }
        else
        {
            //inventado
            body = $@"{{
                ""userName"": "",
                ""company"": "",
                ""email"": "",
                ""firstName"": "",
                ""lastName"": "",
                ""age"": {age2},
                ""password"": ""
            }}";
            Console.WriteLine("El valor de 'age' no es un número entero válido.");
        }

        using (UnityWebRequest request = UnityWebRequest.Post(uriRegister, body, "application/json"))
        {
            yield return request.SendWebRequest();
            /*
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }*/
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                Debug.Log("ERRORRRRR");
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                //comprobamos si es correcto el register
                Comprobacion201RegisterCorrect(request.downloadHandler.text);
                Debug.Log("BIEN");
            }
        }

    }

    //metodo que mira a ver si lo que ha devuelto el register es un codigo 201, esto es register correct
    public void Comprobacion201RegisterCorrect(string registerCorrect)
    {
        //// Obtener una subcadena que comienza en el índice 15(longitud de palabra {"status_code": y tiene una longitud de 3 caracteres  201
        //string codigoCorrecto201 = registerCorrect.Substring(15, 3);
        ////es correcto
        //if(codigoCorrecto201 =="201")
        //{
        //    Debug.Log("Correcto, codigo 201 devuelto");
        //    //notificacion en pantalla unity
        //}
        //else
        //{
        //    Debug.Log("No correcto");
        //}

        // Deserializar el JSON usando JsonUtility
        JsonResponseData response = JsonUtility.FromJson<JsonResponseData>(registerCorrect);

        // Acceder al valor del campo status_code
        int statusCode = response.status_code;

        Debug.Log("Status Code: " + statusCode);

    }

    //metodo que mira a ver si lo que ha devuelto el register es un codigo 201, esto es register correct
    public void ComprobacionAccessTokenLoginCorrect(string loginCorrect)
    {
        

        // Deserializar el JSON usando JsonUtility
        JsonResponseData response = JsonUtility.FromJson<JsonResponseData>(loginCorrect);

        // Acceder al valor del campo status_code
        string token = response.access_token;

        Debug.Log("Access token: " + "" + token + "");

    }




    //metodo para comprobar contraseña con confirmPaswwrod

}


//variables que almacenan comprobaciones de si se ha loggeado bien y registrado bien
[System.Serializable]
public class JsonResponseData
{
    //status code correcto register 201
    public int status_code;
    //token de login para ver que loggea bien
    public string access_token;

}
