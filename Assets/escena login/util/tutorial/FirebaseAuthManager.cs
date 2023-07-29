using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Auth;
using System;
using System.Threading.Tasks;
using Firebase.Extensions;

public class FirebaseAuthManager : MonoBehaviour
{
    
   // Firebase variable
   [Header("Firebase")]
   public DependencyStatus dependencyStatus;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    // Login Variables
    [Space]
   [Header("Login")]
   public TextMeshProUGUI emailLoginField;
   public TextMeshProUGUI passwordLoginField;

   // Registration Variables
   [Space]
   [Header("Registration")]
   public TextMeshProUGUI nameRegisterField;
   public TextMeshProUGUI emailRegisterField;
   public TextMeshProUGUI passwordRegisterField;
   public TextMeshProUGUI confirmPasswordRegisterField;


    bool isSignIn = false;
    bool isSigned = false;

    #region Inicializar

    //checking dependencies 
    private void Awake()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                InitializeFirebase();

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
   
    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
        Debug.Log(auth);
    }


    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                isSignIn = true;
            }
        }

    }

    #endregion




    bool CheckError(AggregateException exception, int firebaseExceptionCode)
    {
        Firebase.FirebaseException fbEx = null;
        foreach (Exception e in exception.Flatten().InnerExceptions)
        {
            fbEx = e as Firebase.FirebaseException;
            if (fbEx != null)
                break;
        }

        if (fbEx != null)
        {
            if (fbEx.ErrorCode == firebaseExceptionCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    #region LOGIN SIGNUP
    public void LoginUser()
    {
        if (string.IsNullOrEmpty(emailLoginField.text) && string.IsNullOrEmpty(passwordLoginField.text))
        {
            return;
        }

        //DO LOGIN
        SignInUser(emailLoginField.text, passwordLoginField.text);
    }

    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(emailRegisterField.text) && string.IsNullOrEmpty(passwordRegisterField.text)&& string.IsNullOrEmpty(nameRegisterField.text) && string.IsNullOrEmpty(confirmPasswordRegisterField.text))
        {
            return;
        }

        //DO Register
        CreateUser(emailRegisterField.text, passwordRegisterField.text, nameRegisterField.text);
    }

    #endregion


    #region signUpComplementarios
    public void CreateUser(string email, string password, string Username )
    {
        //Debug.Log(auth);
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (CheckError(task.Exception, (int)Firebase.Auth.AuthError.EmailAlreadyInUse))
            {
                // do whatever you want in this case
                Debug.LogError("Email already in use");
            }
            Debug.LogError("UpdateEmailAsync encountered an error: " + task.Exception);

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.Log(result);
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            UpdateUserProfile(Username);
        });
    }

    public void UpdateUserProfile(string Username)
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = Username,
                PhotoUrl = new System.Uri("https://via.placeholder.com/360x360"),
            };
            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User profile updated successfully.");
            });
        }
    }

    #endregion


    #region signInComplementarios
    public void SignInUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (CheckError(task.Exception, (int)Firebase.Auth.AuthError.EmailAlreadyInUse))
            {
                // do whatever you want in this case
                Debug.LogError("Email already in use");
            }
            //Debug.LogError("UpdateEmailAsync encountered an error: " + task.Exception);

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

        });
    }
    #endregion

    // Handle removing subscription and reference to the Auth instance.
    // Automatically called by a Monobehaviour after Destroy is called on it.
    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }


    private void Update()
    {
        if(isSignIn)
        {
            if(isSigned)
            {
                isSigned = true;
                
            }
        }
    }



}
