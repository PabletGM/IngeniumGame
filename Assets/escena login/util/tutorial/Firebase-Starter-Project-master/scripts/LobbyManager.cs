using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{



    public static LobbyManager instance;



    [Header("UI References")]
    [SerializeField]
    private GameObject profileUI;
    [SerializeField]
    private GameObject changePfpUI;
    [SerializeField]
    private GameObject changeEmailUI;
    [SerializeField]
    private GameObject changePasswordUI;
    [SerializeField]
    private GameObject reverifyUI;
    [SerializeField]
    private GameObject resetPasswordConfirmUI;
    [SerializeField]
    private GameObject actionSuccessPanelUI;
    [SerializeField]
    private GameObject deleteUserConfirmUI;
    [Space(5f)]

    [Header("Basic Info References")]
    [SerializeField]
    private TMP_Text usernameText;
    [SerializeField]
    private TMP_Text emailText;
    [SerializeField]
    private string token;
    [Space(5f)]

    [Header("Profile Picture References")]
    [SerializeField]
    private Image profilePicture;
    [SerializeField]
    private TMP_InputField profilePictureLink;
    [SerializeField]
    private TMP_Text outputText;
    [Space(5f)]

    [Header("Change Email References")]
    [SerializeField]
    private TMP_InputField changeEmailEmailInputField;
    [Space(5f)]

    [Header("Change Password References")]
    [SerializeField]
    private TMP_InputField changePasswordInputField;
    [SerializeField]
    private TMP_InputField changePasswordConfirmInputField;
    [Space(5f)]

    [Header("Reverify References")]
    [SerializeField]
    private TMP_InputField reverifyEmailInputField;
    [SerializeField]
    private TMP_InputField reverifyPasswordInputField;
    [Space(5)]

    [Header("Action Success Panel References")]
    [SerializeField]
    private TMP_Text actionSuccessText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(FirebaseManager.instance.user != null)
        {
            LoadProfile();
        }
    }

    private void LoadProfile()
    {
        if (FirebaseManager.instance.user != null)
        {
            //Get variables
            System.Uri photoUrl = FirebaseManager.instance.user.PhotoUrl;
            string name = FirebaseManager.instance.user.DisplayName;
            string email = FirebaseManager.instance.user.Email;

            //Set UI
            if(photoUrl!=null)
            {
                StartCoroutine(LoadImage(photoUrl.ToString()));
            }
            
            usernameText.text = name;
            emailText.text = email;
        }
    }

    private IEnumerator LoadImage(string _photoUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(_photoUrl);
        yield return request.SendWebRequest();
        if(request.error != null)
        {
            string output = "Unknown Error! Try again!";

            if(request.isHttpError)
            {
                output = "Image Type not Supported! Please try Another Image";
            }

           Output(output);
        }
        else
        {
            Texture2D image = ((DownloadHandlerTexture)request.downloadHandler).texture;
            profilePicture.sprite= Sprite.Create(image, new Rect(0, 0, image.width, image.height), Vector2.zero);
        }
    }

    public void Output(string _output)
    {
        outputText.text = _output;
    }

    public void ClearUI()
    {
        outputText.text = "";
        profileUI.SetActive(false);
        changePfpUI.SetActive(false);

        actionSuccessPanelUI.SetActive(false);
    }

    public void ProfileUI()
    {
        ClearUI();
        profileUI.SetActive(true);
        LoadProfile();
    }

    public void ChangePfpUI()
    {
        ClearUI();
        changePfpUI.SetActive(true);
    }

    public void ChangePfpSuccess()
    {
        ClearUI();
        actionSuccessPanelUI.SetActive(true);
        actionSuccessText.text = "Profile Picture Changed Successfully";
    }

    public void SubmitProfileImageButton()
    {
        FirebaseManager.instance.UpdateProfilePicture(profilePictureLink.text);
    }

    //volvemos a login
    public void SoundsGood()
    {
        ClearUI();
        ProfileUI();
    }
}
