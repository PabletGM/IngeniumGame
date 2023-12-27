using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField]
    private GameObject panelPasarModoJuego;

    //carpeta generica con todo el boton, imagen y boton
    [SerializeField]
    private GameObject boton;

    //texto Bengalas Left
    [SerializeField]
    private TMP_Text bengalasTexto;

    //carpeta generica con todo el boton, imagen y boton
    [SerializeField]
    private GameObject playPanel;


    [SerializeField]
    private GameObject testModeParpadeo;


    [SerializeField]
    private GameObject fadeInBlack;

    [SerializeField]
    private GameObject robotPanel;

    [SerializeField]
    private GameObject robotPocaPequeño;

    [SerializeField]
    private GameObject testPanel;

    [SerializeField]
    private GameObject dialogoRobotAlejado;

    private void Start()
    {
        
        
    }

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
        // panelText.SetActive(false);
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
        //si estamos en escena prueba activamos parpadeo
        if (SceneManager.GetActiveScene().name == "BengalasPrueba")
        {
            SetTestModeParpadeo(true);
        }
    }

    //quita panel play e indica juego
    public void Play()
    {
        //boton.SetActive(true);
        //SetBoton(true);
        Interfaz.SetActive(true);
        //Play indica inicio juego
        playPanel.SetActive(false);
    }

    public void ActualizarTextoBengalasLeft(string newBengalasLeft)
    {
        bengalasTexto.text = newBengalasLeft;
    }

    public void QuitarInterfaz()
    {
        Interfaz.SetActive(false);
    }

    public void ActivarPanelPasarModoJuego()
    {
        panelPasarModoJuego.SetActive(true);
        panelPasarModoJuego.GetComponent<DOTweenAnimation>().DORestart();
    }

    //parpadeo testMode
    public void SetTestModeParpadeo(bool set)
    {
        //activas GameObject Parpadeo
        testModeParpadeo.SetActive(set);
    }

    public void SetEnd(bool set)
    {
        fadeInBlack.SetActive(set);
    }

    public void ApareceRobotHablando()
    {
        robotPanel.SetActive(true);
        //tras animacion entrada robot aparece dialogo
        Invoke("DialogoAppearAfterAnimation", 1.3f);
    }

    public void DialogoAppearAfterAnimation()
    {

        TestPanel.SetActive(true);
    }

    public void DesactivarRobot()
    {
        Invoke("DesactivarRobotoTrasAnimacion",1f);
    }

    public void DesactivarRobotoTrasAnimacion()
    {
        robotPanel.SetActive(false);
        //activar robot en pradera al final
        robotPocaPequeño.SetActive(true);
    }


    public void ApareceDialogoRobotAlejadoAviso()
    {
        dialogoRobotAlejado.SetActive(true);
        //dejas la notificacion 1.5 segundos
        Invoke("QuitarDialogoRobotAlejadoAviso", 2f);
    }
    public void QuitarDialogoRobotAlejadoAviso()
    {
        dialogoRobotAlejado.SetActive(false);
    }



}
