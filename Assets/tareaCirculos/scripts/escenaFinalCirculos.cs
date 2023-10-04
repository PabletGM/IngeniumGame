using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class escenaFinalCirculos : MonoBehaviour
{
   

    [SerializeField] private GameObject dialogoRobot;
    [SerializeField] private GameObject robot;
    [SerializeField] private GameObject mano;

    private float duracionAnimacion = 1.2f;

    private void Start()
    {

        //invoca en 2f aparece mano
        Invoke("ApareceMano", 2f);
    }

    public void IniciarDialogoRobot()
    {
        //inicias robot
        robot.SetActive(true);
        Invoke("SetDialogoRobotActivar", duracionAnimacion);
    }

    //aparece mano interactuable boton
    public void ApareceMano()
    {
        //mano que es boton y al pulsar inicia dialogo robot
        mano.SetActive(true);
    }

   

    //activar dialogo robot
    private void SetDialogoRobotActivar()
    {
        dialogoRobot.SetActive(true);
        
    }

   

    public void AcabarGameCirculos()
    {
        //SceneManager.LoadScene("PanelRadar");
    }
}


