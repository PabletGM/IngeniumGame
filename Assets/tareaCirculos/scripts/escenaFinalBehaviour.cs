using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escenaFinalBehaviour : MonoBehaviour
{

    [SerializeField]
    private GameObject nextSceneBoton;


    private void Start()
    {
        //invocamos en 3 segundos activar boton empezar
        Invoke("ActivarPlayGame", 3f);
    }
    public void PasarNextScene()
    {
        LevelLoader.LoadLevel("EscenaInicial3EnRaya");
    }

    public void ActivarPlayGame()
    {
        nextSceneBoton.SetActive(true);
    }
}
