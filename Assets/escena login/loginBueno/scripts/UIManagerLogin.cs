using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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
        string uri = "https://eu-west-1.aws.data.mongodb-api.com/app/ingenuity-app-0-mmgty/endpoint/Ingenuity/Test";

        //se pone el form en el segundo argumento de la funcion post
        //string body = $"{{ \"username\": {s}, \"password\": \"1234\", \"field3\": {{\"field3.-1\": {num},\"field3.0\": [{numbersArrayString}],\"field3.1\": [\"uno\",1], \"field3.2\": \"dos\"}} }}";
        string body = $@"{{
            ""username"": ""{userNameLogin}"",
            ""password"": ""{passwordLogin}""
        }}";

        using (UnityWebRequest request = UnityWebRequest.Post(uri, body, "application/json"))
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
        string uri = "https://eu-west-1.aws.data.mongodb-api.com/app/ingenuity-app-0-mmgty/endpoint/Ingenuity/Test";

        //se pone el form en el segundo argumento de la funcion post
        //string body = $"{{ \"username\": {s}, \"password\": \"1234\", \"field3\": {{\"field3.-1\": {num},\"field3.0\": [{numbersArrayString}],\"field3.1\": [\"uno\",1], \"field3.2\": \"dos\"}} }}";


        string body = $@"{{
            ""firstName"": ""{firstName}"",
            ""lastName"": ""{lastName}"",
            ""age"": ""{age}"",
            ""company"": ""{company}"",
            ""email"": ""{email}"",
            ""userNameRegister"": ""{userNameRegister}"",
            ""passwordRegister"": ""{passwordRegister}"",
            ""confirmPasswordRegister"": ""{confirmPasswordRegister}""
 
        }}";

        using (UnityWebRequest request = UnityWebRequest.Post(uri, body, "application/json"))
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
                Debug.Log("BIEN");
            }
        }

    }

}
