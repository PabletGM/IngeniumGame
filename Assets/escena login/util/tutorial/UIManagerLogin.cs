using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerLogin : MonoBehaviour
{
    //singleton
    static private UIManagerLogin _instanceUILogin;

    [SerializeField]
    private GameObject loginPanel;

    [SerializeField]
    private GameObject registrationPanel;


    [SerializeField]
    private GameObject buttonChangeStateLoginToRegister;

    [SerializeField]
    private GameObject buttonChangeStateRegisterToLogin;

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


}
