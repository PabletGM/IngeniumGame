using System;
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

    private float numeroRondasHechas = 0;
    private float numeroMaxRondas= 3;

    private int numSecsPartidaCirculos = 0;


    #region MongoDB
    private int[] patronRondasJugador;
    #endregion

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

    private void AumentarNumeroRondasHechas()
    {
        //ponemos tambien en el nivel que estamos en cada ronda en nuestro array
        PonerNivelRonda();
        numeroRondasHechas++;
    }

    private void PonerNivelRonda()
    {
        //si esta dentro de los limites del array
        if(numeroRondasHechas < numeroMaxRondas)
        {
            //si es nivel 1
            if (SceneManager.GetActiveScene().name == "circulosNave")
            {
                patronRondasJugador[Convert.ToInt32(numeroRondasHechas)] = 1;
            }
            //si estamos en primera escena al chocarnos perdemos 2 puntos
            else if (SceneManager.GetActiveScene().name == "circulosNaveNivel2")
            {
                patronRondasJugador[Convert.ToInt32(numeroRondasHechas)] = 2;
            }

            //si estamos en primera escena al chocarnos perdemos 2 puntos
            else if (SceneManager.GetActiveScene().name == "circulosNaveNivel3")
            {
                patronRondasJugador[Convert.ToInt32(numeroRondasHechas)] = 3;
            }
        }
        
    }

    public int[] DevolverRondasJugador()
    {
        return patronRondasJugador;
    }

    static public GameManagerCirculos GetInstanceGM()
    {
        return _instanceGMCirculos;
    }



    //ponemos puntuacion actual por pantalla, solo se llama una vez en la primera escena, ya que este script luego se reutiliza
    void Start()
    {
        //tamaño de array con numero de Rondas
        patronRondasJugador = new int[3];
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
        // Calcula el tiempo restante hasta el próximo ciclo.
        float tiempoRestante = tiempoEspera - (Time.time - tiempoInicial);

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
            

            UIManagerCirculos.GetInstanceUI().ActualizarCronometroCanvas(tiempoRestante);
            //aumentamos numero de rondas
            AumentarNumeroRondasHechas();
            //comprobamos limite rondas
            LimiteRondas();
        }
        //sino ha acabado la ronda actualizamos cronometro en pantalla
        else
        {
            // Muestra el tiempo restante en un mensaje de depuración (puedes ajustar esto según tus necesidades).
            UIManagerCirculos.GetInstanceUI().ActualizarCronometroCanvas(tiempoRestante);
        }

    }


    //comprueba sino has pasado ya las 3 rondas
    public void LimiteRondas()
    {
        if(numeroRondasHechas>= numeroMaxRondas)
        {
            UIManagerCirculos.GetInstanceUI().LimiteRondas();
        }
    }

    


    //se ha salido fuera del circulo
    public void PerderPuntuacion()
    {
        GameObject nave = GameObject.Find("nave");
        //si estamos en primera escena al chocarnos perdemos 2 puntos
        if (SceneManager.GetActiveScene().name == "circulosNave")
        {
            puntuacionActual -= golpeChoqueNivel1;
            //buscamos en jerarquia objeto nave y llamamos a su metodo ShowDamageNotification(int damageAmount)
            nave.GetComponent<DamageNotification>().ShowDamageNotification();
        }
        //si estamos en primera escena al chocarnos perdemos 2 puntos
        else if (SceneManager.GetActiveScene().name == "circulosNaveNivel2")
        {
            puntuacionActual -= golpeChoqueNivel2;
            nave.GetComponent<DamageNotification>().ShowDamageNotification();
        }

        //si estamos en primera escena al chocarnos perdemos 2 puntos
        else if (SceneManager.GetActiveScene().name == "circulosNaveNivel3")
        {
            puntuacionActual -= golpeChoqueNivel3;
            nave.GetComponent<DamageNotification>().ShowDamageNotification();
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

        //activamos objetos y jugabilidad en GameManager
        UIManagerCirculos.GetInstanceUI().SetObjetosJugabilidad(true);


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

    public void NumSecsPartidaCirculos(int secsPartida)
    {
        numSecsPartidaCirculos = secsPartida;
    }

    public int TiempoPartidaCirculos()
    {
        return numSecsPartidaCirculos;
    }


}
