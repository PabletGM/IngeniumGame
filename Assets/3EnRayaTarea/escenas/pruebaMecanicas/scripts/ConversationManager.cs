using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConversationManager : MonoBehaviour
{
    //para que se llame una vez a cada conversacion
    private bool conversacionAcabada = false;

    [SerializeField]
    private GameObject Board;

    [SerializeField]
    private GameObject WaitingDialogueAstronauta1;
    [SerializeField]
    private GameObject WaitingDialogueAstronauta2;
    [SerializeField]
    private GameObject DialogueAstronauta1;
    [SerializeField]
    private GameObject DialogueAstronauta2;

    [SerializeField]
    private GameObject ButtonIniciarPartida;

    [SerializeField]
    private TMP_Text DialogueAstronauta1Text;
    [SerializeField]
    private TMP_Text DialogueAstronauta2Text;

    [SerializeField]
    private GameObject personajes;

    [SerializeField]
    private GameObject clicking;


    //segun numero de clicks ponemos un texto u otro
    private int numeroClicks = 0;

    //metodo tras pulsar pantalla primera vez
    public void EmpezarConversation()
    {
        DialogueAstronauta1Text.text = "";
        DialogueAstronauta2Text.text = "";
        //desactivamos waiting conversations
        WaitingDialogueAstronauta1.SetActive(false);
        WaitingDialogueAstronauta2.SetActive(false);
        //desactivamos conversacion2
        DialogueAstronauta2.SetActive(false);
        //activamos conversacion1
        DialogueAstronauta1.SetActive(true);
        //Ponemos texto en pantalla
        DialogueAstronauta1Text.text = "Bua no veas que partidaza...";
        SonidoHablar();
    }

    public void SonidoHablar()
    {
        AudioManager3EnRaya.instance.PlaySFX("conversation");
    }

    //metodo tras pulsar pantalla segunda vez
    public void EmpezarConversation2()
    {
        DialogueAstronauta1Text.text = "";
        DialogueAstronauta2Text.text = "";
        //activamos conversacion2
        DialogueAstronauta2.SetActive(true);
        //desactivamos conversacion1
        DialogueAstronauta1.SetActive(false);
        //Ponemos texto en pantalla
        DialogueAstronauta2Text.text = "Calla, que te estoy pegando una paliza...";
        SonidoHablar();
    }

    //metodo tras pulsar pantalla tercera vez
    public void EmpezarConversation3()
    {
        DialogueAstronauta1Text.text = "";
        DialogueAstronauta2Text.text = "";
        //activamos conversacion1
        DialogueAstronauta1.SetActive(true);
        //desactivamos conversacion2
        DialogueAstronauta2.SetActive(false);
        //Ponemos texto en pantalla
        DialogueAstronauta1Text.text = "Ostras, mira quien acaba de entrar, ¿Te echas una partida, novato? ...";
        SonidoHablar();
    }

    //metodo tras pulsar pantalla primera vez
    public void ActivarBotonNextLevel()
    {
        DialogueAstronauta1Text.text = "";
        DialogueAstronauta2Text.text = "";
        //activamos conversacion1
        DialogueAstronauta1.SetActive(false);
        //desactivamos conversacion2
        DialogueAstronauta1.SetActive(false);

        ButtonIniciarPartida.SetActive(true);
        clicking.SetActive(false);
    }

    public void NumeroClicks()
    {
        numeroClicks++;
        //permite llamar a otra conversacion
        conversacionAcabada = false;
    }

    public void IniciarPartida()
    {
        AudioManager3EnRaya.instance.PlaySFXDuracion("IniciarPartida",0.5f);
        //activas objeto decoracion Board y hace animacion que dura 2.5f
        Board.SetActive(true);
        AudioManager3EnRaya.instance.PlaySFXDuracion("Unlocked", 2f);
        //desactivamos resto
        DesactivarEscena();
        //tras 2.4f pasas al nivel
        Invoke("Iniciar3EnRaya", 1.8f);
    }

    

    private void DesactivarEscena()
    {
        DialogueAstronauta1Text.text = "";
        DialogueAstronauta2Text.text = "";
        //activamos conversacion1
        DialogueAstronauta1.SetActive(false);
        //desactivamos conversacion2
        DialogueAstronauta2.SetActive(false);
        ButtonIniciarPartida.SetActive(false);
        personajes.SetActive(false);
        clicking.SetActive(false);
    }

    public void Iniciar3EnRaya()
    {
        SceneManager.LoadScene("mecanicas3EnRayaModoDificil");
    }


    

    // Update is called once per frame
    void Update()
    {
        //sino se ha repetido la conversacion
        if(!conversacionAcabada)
        {
            //primer texto
            if (numeroClicks == 1)
            {
                conversacionAcabada = true;
                EmpezarConversation();
            }
            //segundo texto
            else if (numeroClicks == 2)
            {
                conversacionAcabada = true;
                EmpezarConversation2();
            }
            //tercer texto
            else if (numeroClicks == 3)
            {
                conversacionAcabada = true;
                EmpezarConversation3();
            }
            //activa boton
            else if (numeroClicks == 4)
            {
                conversacionAcabada = true;
                ActivarBotonNextLevel();
            }
        }
        
    }
}
