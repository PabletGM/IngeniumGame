using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaInicial3EnRayaMecanicas : MonoBehaviour
{

    //Animator puerta para ver cuando se abre y cuando se queda abierta entre 1 estado y otro
    public Animator puertaSalaInvitados;

    [SerializeField] private GameObject imagenPuerta;
    [SerializeField] private GameObject animacionPuerta;

    [SerializeField] private GameObject clicking;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    //al pulsar el boton abrir puerta
    public void BotonAbrirPuerta()
    {
        SetClicking(false);
        //quitamos imagen puertaImagen
        imagenPuerta.SetActive(false);
        //ponemos G0 puertaAnimacion y comenzará esta animacion que dura 3 segundos
        animacionPuerta.SetActive(true);
        //sonido abrir puerta
        SonidoAbrirPuerta();
        //inovocamos metodo DejarPuertaAbierta en 3f
        Invoke("DejarPuertaAbierta", 2.9f);
    }

    private void SonidoAbrirPuerta()
    {
        AudioManager3EnRaya.instance.PlaySFX("doorSpace");
    }

    //metodo dejar puerta abierta, forzamos el cambio de animacion dentro del animator
    private void DejarPuertaAbierta()
    {
        //quitamos imagen puertaImagen
        imagenPuerta.SetActive(false);
        //ponemos G0 puertaAnimacion y comenzará esta animacion que dura 3 segundos
        animacionPuerta.SetActive(false);
        //cargar siguiente escena
        NextScene();
    }

    public void NextScene()
    {
        SceneManager.LoadScene("EscenaSalaInvitados");
    }

    private void SetClicking(bool set)
    {
        clicking.SetActive(set);
    }
}
