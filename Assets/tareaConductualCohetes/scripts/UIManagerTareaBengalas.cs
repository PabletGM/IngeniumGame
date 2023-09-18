using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerTareaBengalas : MonoBehaviour
{
    //singleton
    static private UIManagerTareaBengalas _instanceUITareaBengalas;

    GameManagerTareaBengalas GMTareaBengalas;


    [SerializeField]
    private Button botonStart;

    [SerializeField]
    private Image imagenBoton;


    [SerializeField]
    private GameObject Interfaz;

    [SerializeField]
    private GameObject panelText;

    [SerializeField]
    private GameObject TestPanel;

    [SerializeField]
    private GameObject IntentosPanel;

    //carpeta generica con todo el boton, imagen y boton
    [SerializeField]
    private GameObject boton;

    //texto Bengalas Left
    [SerializeField]
    private TMP_Text bengalasTexto;

    private void Awake()
    {
       
        //si la instancia no existe se hace este script la instancia
        if (_instanceUITareaBengalas == null)
        {
            _instanceUITareaBengalas = this;
        }
        //si la instancia existe , destruimos la copia
        else
        {
            Destroy(this.gameObject);
        }
    }

    static public UIManagerTareaBengalas GetInstanceUI()
    {
        return _instanceUITareaBengalas;
    }

    public void SetBoton(bool set)
    {
        //si set = true
        if(set)
        {
            //activamos boton
            botonStart.gameObject.SetActive(true);
            //desactivamos imagen del boton
            imagenBoton.gameObject.SetActive(false);
        }
        else
        {
            //desactivamos boton
            botonStart.gameObject.SetActive(false);
            //activamos imagen del boton
            imagenBoton.gameObject.SetActive(true);
        }
    }

    //metodo que activa la funcionalidad del boton
    //activa interfaz y quitaPaneltext
    public void EmpezarJuegoBengalas()
    {
        panelText.SetActive(false);
        Interfaz.SetActive(true);
    }

    //quitamos jugabilidad ocultando el boton y quitando interfaz y poniendo panelText
    public void PanelJuegoBengalas()
    {
        boton.SetActive(false);
        Interfaz.SetActive(false);
        panelText.SetActive(true);
        SetBoton(false);

    }

    //al cerrar este panel, se activa intentos panel y juego normal con boton
    public void CloseTestPanel()
    {
        boton.SetActive(true);
        SetBoton(true);
        TestPanel.SetActive(false);
        IntentosPanel.SetActive(true);
    }

    public void ActualizarTextoBengalasLeft(string newBengalasLeft)
    {
        bengalasTexto.text = newBengalasLeft;
    }


}
