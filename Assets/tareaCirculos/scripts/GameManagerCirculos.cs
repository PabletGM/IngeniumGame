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

    //una vez que este es true empeiza el cronometro que estará durante la duracion de la ronda
    private bool permitirCronometroRonda = false;
    private float tiempoInicial;
    private float tiempoEspera = 15f;

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
        //si puede empezar el cronometro
        if(permitirCronometroRonda)
        {
            CronometroTime();
        }
    }

    private void CronometroTime()
    {
        // Comprueba si han pasado 15 segundos desde el tiempo inicial.
        if (Time.time - tiempoInicial >= tiempoEspera)
        {
            // Realiza aquí la acción que deseas que ocurra cada 15 segundos.
            Debug.Log("Ronda acabada");
            permitirCronometroRonda = false;
            //quitamos velocidad de juego del radar y jugabilidad
            GameObject radar = GameObject.Find("radar");
            radar.GetComponent<BouncingRadar>().ChangeSpeedSin();
            // Actualiza el tiempo inicial para el próximo ciclo.
            tiempoInicial = Time.time;
            //como ha acabado la ronda activamos panel ronda para ver a que nivel pasamos
            UIManagerCirculos.GetInstanceUI().ActivarPanelRonda();
        }
        
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

    //empieza la ronda: cada 15 segundos despliega panel y te da opcion de pasar o retroceder de ronda
    public void CronometroRonda()
    {
        Debug.Log("Empieza  la ronda");
        //vemos en que escena estamos y ponemos velocidad del bouncing radar a la que sea diferente de 0
        GameObject radar = GameObject.Find("radar");
        if (SceneManager.GetActiveScene().name == "circulosNave")
        {
            radar.GetComponent<BouncingRadar>().ChangeSpeed(0.4f);//0.4
        }
        else if (SceneManager.GetActiveScene().name == "circulosNaveNivel2")
        {
            radar.GetComponent<BouncingRadar>().ChangeSpeed(0.6f);//0.6
        }
        else if (SceneManager.GetActiveScene().name == "circulosNaveNivel3")
        {
            radar.GetComponent<BouncingRadar>().ChangeSpeed(0.7f);//0.7
        }
        // Guarda el tiempo actual cuando comienza la ronda.
        tiempoInicial = Time.time;
        //inicie cronometro
        permitirCronometroRonda = true;
    }

    
}
