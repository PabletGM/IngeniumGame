using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//metodo generico que al acabar una animacion pasa a la siguiente escena
public class NextSceneGenericMethod : MonoBehaviour
{

    [SerializeField]
    private float tiempoEsperaPasoEscena;

    [SerializeField]
    private string nombreCambioEscena;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("NextScene", tiempoEsperaPasoEscena);
    }

   private void NextScene()
    {
        SceneManager.LoadScene(nombreCambioEscena);
    }
}
