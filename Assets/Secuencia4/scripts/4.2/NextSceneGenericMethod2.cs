using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneGenericMethod2 : MonoBehaviour
{
    [SerializeField]
    private float tiempoEsperaPasoEscena;

    [SerializeField]
    private string nombreCambioEscena;
    
    public void NextScene()
    {
        SceneManager.LoadScene(nombreCambioEscena);
    }
}
