using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConversationManager : MonoBehaviour
{

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
        DialogueAstronauta1Text.text = "Bua no veas que partidaza al 3 en raya...";
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
    }

    public void NumeroClicks()
    {
        numeroClicks++;
    }

    public void IniciarPartida()
    {
        SceneManager.LoadScene("mecanicas3EnRayaModoDificil");
    }


    

    // Update is called once per frame
    void Update()
    {
        //primer texto
        if(numeroClicks == 1) 
        {
            EmpezarConversation();
        }
        //segundo texto
        else if (numeroClicks == 2)
        {
            EmpezarConversation2();
        }
        //tercer texto
        else if (numeroClicks == 3)
        {
            EmpezarConversation3();
        }
        //activa boton
        else if (numeroClicks == 4)
        {
            ActivarBotonNextLevel();
        }
    }
}
