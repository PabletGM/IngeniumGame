using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;


    [Header("Firebase")]
    public FirebaseAuth auth;
    public FirebaseUser user;
    [Space(5f)]

    [Header("Login References")]
    [SerializeField]
    private TMP_InputField loginEmail;
    [SerializeField]
    private TMP_InputField loginPassword;
    [SerializeField]
    private TMP_Text loginOutputText;
    [Space(5f)]


    [Header("Register References")]
    [SerializeField]
    private TMP_InputField registerUsername;
    [SerializeField]
    private TMP_InputField registerEmail;
    [SerializeField]
    private TMP_InputField registerPassword;
    [SerializeField]
    private TMP_InputField registerConfirmPassword;
    [SerializeField]
    private TMP_Text registerOutputText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            
            Destroy(gameObject);
            //instance = this;
        }
       

        #region checkDependencies

        //Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        //    var dependencyStatus = task.Result;
        //    if (dependencyStatus == Firebase.DependencyStatus.Available)
        //    {
        //        InitializeFirebase();
        //    }
        //    else
        //    {
        //        UnityEngine.Debug.LogError(System.String.Format(
        //          "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        //        // Firebase Unity SDK is not safe to use here.
        //    }
        //});
        #endregion

    }

    private void Start()
    {
        StartCoroutine(CheckAndFixDependencies());
    }

    private IEnumerator CheckAndFixDependencies()
    {
        var checkAndFixDependenciesTask = FirebaseApp.CheckAndFixDependenciesAsync();

        yield return new WaitUntil(predicate: () => checkAndFixDependenciesTask.IsCompleted);

        var dependencyResult = checkAndFixDependenciesTask.Result;

        if(dependencyResult == DependencyStatus.Available)
        {
            InitializeFirebase();
        }
        else
        {
            Debug.LogError($"Could not resolve all firebase dependencies: {dependencyResult}");
        }
    }

    private IEnumerator CheckAutoLogin()
    {
        yield return new WaitForEndOfFrame();
        if(user!=null)
        {
            var reloadUserTask = user.ReloadAsync();

            yield return new WaitUntil(predicate: () => reloadUserTask.IsCompleted);

            AutoLogin();
        }
        else
        {
            AuthUIManager.instance.LoginScreen();
        }
    }

    private void AutoLogin()
    {
        if(user!=null)
        {
            //Todo : email verification
            if(user.IsEmailVerified)
            {
                GameManagerTuto.instance.ChangeScene("Lobby");
            }
            else
            {
                StartCoroutine(SendVerificationEmail());
            }
        }
        else
        {
            AuthUIManager.instance.LoginScreen();
        }
    }

    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        StartCoroutine(CheckAutoLogin());
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if(auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser !=null;

            if(!signedIn && user !=null)
            {
                Debug.Log("Signed Out");
            }

            user = auth.CurrentUser;

            if(signedIn)
            {
                Debug.Log($"Signed In: {user.DisplayName}");
            }
        }
    }

    public void ClearOutputs()
    {
        loginOutputText.text = "";
        registerOutputText.text = "";
    }

    public void LoginButton()
    {
        StartCoroutine(LoginLogic(loginEmail.text, loginPassword.text));
    }

    public void RegisterButton()
    {
        StartCoroutine(RegisterLogic(registerUsername.text, registerEmail.text, registerPassword.text, registerConfirmPassword.text));
    }

    private IEnumerator LoginLogic( string _email, string _password) 
    { 
        Credential credential = EmailAuthProvider.GetCredential(_email, _password);
        var loginTask = auth.SignInWithCredentialAsync(credential);

        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if(loginTask.Exception!=null)
        {
            FirebaseException firebaseException = (FirebaseException)loginTask.Exception.GetBaseException();
            AuthError error = (AuthError)firebaseException.ErrorCode;
            string output = "Unknown Error";

            switch(error)
            {
                case AuthError.MissingEmail:
                    output= "Please enter your Email";
                    break;

                case AuthError.MissingPassword:
                    output= "Please enter your Password";
                    break;

                case AuthError.InvalidEmail:
                    output = "Invalid Email";
                    break;

                case AuthError.WrongPassword:
                    output = "Incorrect Password";
                    break;

                case AuthError.UserNotFound:
                    output = "Account Does Not Exist";
                    break;
            }
            loginOutputText.text = output;
        }
        else
        {
            if(user.IsEmailVerified)
            {
                yield return new WaitForSeconds(1f);
                GameManagerTuto.instance.ChangeScene("Lobby");
                    //UnityEngine.SceneManagement.SceneManager.LoadScene(nombreEscena);
            }
            else
            {
                //send verification email

                StartCoroutine(SendVerificationEmail());
            }
        }
    }

    private IEnumerator RegisterLogic(string _username, string _email,  string _password, string _confirmPassword)
    {
        if(_username =="")
        {
            registerOutputText.text = "Please Enter A Username";
        }
        else if(_username.ToLower() == "bad word")
        {
            registerOutputText.text = "That Username is Innappropiate";
        }
        else if(_password != _confirmPassword)
        {
            registerOutputText.text = "Passwords Do Not Match!";
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            if(registerTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)registerTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;
                string output = "Unknown Error";

                switch (error)
                {
                    case AuthError.InvalidEmail:
                        output = "InvalidEmail";
                        break;

                    case AuthError.EmailAlreadyInUse:
                        output = "Email Already In Use";
                        break;

                    case AuthError.WeakPassword:
                        output = "Weak Password";
                        break;

                    case AuthError.MissingEmail:
                        output = "Please Enter Your Email";
                        break;

                    case AuthError.MissingPassword:
                        output = "Please Enter Your Password";
                        break;
                }
                registerOutputText.text = output;
            }
            else
            {
                UserProfile profile = new UserProfile
                {
                    DisplayName = _username,

                    //Todo: Give Profile Default Photo
                    PhotoUrl = new System.Uri("https://pbs.twimg.com/media/EFKdt0bWsAIfcj9.jpg"),
                };
                var defaultUserTask = user.UpdateUserProfileAsync(profile);

                yield return new WaitUntil(predicate: () => defaultUserTask.IsCompleted);

                if(defaultUserTask.Exception != null)
                {
                    user.DeleteAsync();
                    FirebaseException firebaseException = (FirebaseException)defaultUserTask.Exception.GetBaseException();
                    AuthError error = (AuthError)firebaseException.ErrorCode;
                    string output = "Unknown Error";

                    switch (error)
                    {
                        case AuthError.Cancelled:
                            output = "Update User Cancelled";
                            break;

                        case AuthError.SessionExpired:
                            output = "Session Expired";
                            break;

                        case AuthError.WeakPassword:
                            output = "Weak Password";
                            break;

                        case AuthError.MissingEmail:
                            output = "Please Enter Your Email";
                            break;

                        case AuthError.MissingPassword:
                            output = "Please Enter Your Password";
                            break;
                    }
                    registerOutputText.text = output;
                }
                else
                {
                    Debug.Log($"Firebase User Created Succesfully: {user.DisplayName} ({user.UserId}");

                    //TODO: Send Verification Email
                    StartCoroutine(SendVerificationEmail());
                }
            }
        }
    }

    private IEnumerator SendVerificationEmail()
    {
        if(user!=null)
        {
            var emailTask = user.SendEmailVerificationAsync();

            yield return new WaitUntil(predicate: () =>emailTask.IsCompleted);

            if(emailTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException) emailTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;

                string output = "Unknown Error, Try again!";

                switch(error)
                {
                    case AuthError.Cancelled:
                        output = "Verification Task was Cancelled";
                        break;

                    case AuthError.InvalidRecipientEmail:
                        output = "Invalid Email";
                        break;


                    case AuthError.TooManyRequests:
                        output = "Too Many Requests";
                        break;
                }

                AuthUIManager.instance.AwaitVerification(false, user.Email, output);
            }
        }
        else
        {
            AuthUIManager.instance.AwaitVerification(true, user.Email, null);
            Debug.Log("Email Sent Succesfully");
        }
    }

    public void UpdateProfilePicture(string _newPfpURL)
    {
        StartCoroutine(UpdateProfilePictureLogic(_newPfpURL));
    }
    private IEnumerator UpdateProfilePictureLogic(string _newPfpURL)
    {
        if(user!=null)
        {
            UserProfile profile = new UserProfile();
            try
            {
                UserProfile _profile = new UserProfile
                {
                    PhotoUrl = new System.Uri(_newPfpURL)
                };

                profile = _profile;
            }
            catch
            {
                //TODO: Add lobby Manager "Output"
                LobbyManager.instance.Output("Error Fetching Image, Make Sure Your Link is Valid!");
                yield break;
            }

            var pfpTask = user.UpdateUserProfileAsync(profile);
            yield return new WaitUntil(predicate: () => pfpTask.IsCompleted);

            if(pfpTask.Exception!=null)
            {
                Debug.LogError($"Updating Profile Picture was unsuccessful: {pfpTask.Exception}");
            }
            else
            {
                //TODO: Add Lobby Manager "ChangePfpSuccess"
                LobbyManager.instance.ChangePfpSuccess();
                Debug.Log("Profile Image Updated Succesfully");
            }

        }
    }
}
