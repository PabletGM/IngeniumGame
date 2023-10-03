using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerCirculos : MonoBehaviour
{
    #region references
    static private GameManagerCirculos _instanceGMCirculos;
    UIManagerCirculos UICirculos;
    #endregion

    private int puntuacionInicial = 100;


    //esta puntuacion estará en cada escena pasando
    private int puntuacionActual;

    private int golpeChoqueNivel1 = 2;
    private int golpeChoqueNivel2 = 5;
    private int golpeChoqueNivel3 = 8;

    private void Awake()
    {

        //si la instancia no existe se hace este script la instancia
        if (_instanceGMCirculos == null)
        {
            _instanceGMCirculos = this;
            DontDestroyOnLoad(gameObject);
        }
        //si la instancia existe , destruimos la copia
        else
        {
            Destroy(this.gameObject);
        }
    }

    static public GameManagerCirculos GetInstanceGM()
    {
        return _instanceGMCirculos;
    }



    //ponemos puntuacion actual por pantalla, solo se llama una vez en la primera escena, ya que este script luego se reutiliza
    void Start()
    {

        //si es la escena 1 y la primera vez ponemos que la puntuacion actual sea la puntuacion Inicial
        if (SceneManager.GetActiveScene().name == "circulosNave")
        {
            puntuacionActual = puntuacionInicial;
        }

        string puntuacionActualString = puntuacionActual.ToString();
        UIManagerCirculos.GetInstanceUI().CambiarPuntuacion(puntuacionActualString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //se ha salido fuera del circulo
    public void PerderPuntuacion()
    {

        //si estamos en primera escena al chocarnos perdemos 2 puntos
        if (SceneManager.GetActiveScene().name == "circulosNave")
        {
            puntuacionActual -= golpeChoqueNivel1;
        }
        //si estamos en primera escena al chocarnos perdemos 2 puntos
        else if (SceneManager.GetActiveScene().name == "circulosNaveNivel2")
        {
            puntuacionActual -= golpeChoqueNivel2;
        }

        //si estamos en primera escena al chocarnos perdemos 2 puntos
        else if (SceneManager.GetActiveScene().name == "circulosNaveNivel3")
        {
            puntuacionActual -= golpeChoqueNivel3;
        }

        string puntuacionActualString = puntuacionActual.ToString();
        //lo ponemos en el UIManager
        UIManagerCirculos.GetInstanceUI().CambiarPuntuacion(puntuacionActualString);
    }

    public int DevolverPuntuacionActual()
    {
        return puntuacionActual;
    }
}
