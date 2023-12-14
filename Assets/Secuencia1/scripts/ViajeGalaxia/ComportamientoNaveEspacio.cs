using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComportamientoNaveEspacio : MonoBehaviour
{

     [SerializeField]
     private GameObject botonEscena;

    [SerializeField]
    private GameObject naveEspacial2;

    [SerializeField]
    private GameObject naveEspacial3;


    private void Start()
    {
        Invoke("DesbloquearBoton", 3f);
    }

    private void Update()
    {

       
    }

    //activas boton que es un dialogo
    private void DesbloquearBoton()
    {
        botonEscena.SetActive(true);
        //quitamos animator
        naveEspacial2.GetComponent<Animator>().enabled = false;
        naveEspacial3.GetComponent<Animator>().enabled = false;
        //activar script de naves HuidaLateral
        naveEspacial2.GetComponent<HuidaLateral>().enabled = true;
        naveEspacial3.GetComponent<HuidaLateral>().enabled = true;
        AudioManagerIntro.instance.PlaySFX("cohete");
    }


    public void PasarEscena()
    {
        SceneManager.LoadScene("LlegadaPlaneta");
    }
}
