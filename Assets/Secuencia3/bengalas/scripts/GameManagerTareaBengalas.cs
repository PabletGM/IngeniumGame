using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerTareaBengalas : MonoBehaviour
{
    #region references
    static private GameManagerTareaBengalas _instanceGMTareaBengalas;
    UIManagerTareaBengalas UITareaBengalas;
    #endregion

    [SerializeField]
    private GameObject bengalaParaDespegar;

    //transform inicial bengala
    [SerializeField]
    private Transform transformBengala;
    private Vector3 posInicialBengala;

    [SerializeField]
    private GameObject astronautaEscena;

    [SerializeField]
    private GameObject botonStart;

    [SerializeField]
    private GameObject marcadores;


    private float timeBengalaVida = 0;


    [SerializeField]
    private GameObject AudioManagerObject;


    #region MongoDB
    private float[] alturaCohetes;

    private int numeroAlturaCohetesRegistradas = 0;

    [SerializeField]
    private GameObject mongoDB;
    #endregion

    private int numSecsPartidaBengalas;



    #region Marcador
    //habrá 2 de prueba de 0 a 2
    private int contadorNumeroTiradas = 0;
    //numero de tiradas totales
    private int numeroTiradasTotal = 2;
    //array de GameObjects marcadores
    [SerializeField]
    private Marcador[] marcadoresTiradas;

    [SerializeField]
    private GameObject[] listaBengalasLeft;

    #endregion

    private void Awake()
    {

        //si la instancia no existe se hace este script la instancia
        if (_instanceGMTareaBengalas == null)
        {
            _instanceGMTareaBengalas = this;
        }
        //si la instancia existe , destruimos la copia
        else
        {
            Destroy(this.gameObject);
        }
    }


    

    private void Start()
    {
        //ponemos pos inicial bengala
        posInicialBengala = transformBengala.position;
        UITareaBengalas = UIManagerTareaBengalas.GetInstanceUI();
        alturaCohetes = new float[3];

        //depende del nombre de la escena ponemos mecanica de 3 cohetes o 2
        if (SceneManager.GetActiveScene().name == "TareaBengalasGame")
        {
           
            numeroTiradasTotal = 3;
        }
        //si es bengalasPrueba inicializamos musica
        else
        {
            AudioManagerBengalas.instance.PlayMusic("nature", 1f);
        }
    }

    static public GameManagerTareaBengalas GetInstanceGM()
    {
        return _instanceGMTareaBengalas;
    }

    public void SetBotonStartPermisoParaLanzamientoAutomatico(bool set)
    {
        botonStart.GetComponent<ComportamientoBotonStart>().permitirLanzamientoAutomatico = set;
    }

    //el cohete aguanta sin explotar el tiempo que se deje pulsado el boton
    public void LanzamientoCohete(float timeToExplote)
    {
        timeBengalaVida = timeToExplote;
        //hacemos impulsable boton hasta nueva tirada
        UITareaBengalas.SetBoton(false);
        EncenderCohete();
        Invoke("MecanicaCohete",1.5f);
    }

    public void EncenderCohete()
    {
        //hacemos animacion encender cohete
        astronautaEscena.GetComponent<ComportamientoAstronauta>().AnimacionEncenderBengala();
    }

    [Obsolete]
    public void MecanicaCohete()
    {
        //llama a la funcion del cohete que lo propulsa para arriba

        //permiso para despegar cohete
        //preguntamos a boton que tiempo vivirá el cohete
        bengalaParaDespegar.GetComponent<ComportamientoBengalaADisparar>().DespegarCohete(timeBengalaVida);
        //si estamos en escena de bengalas buena, no la de prueba registramos las alturas de los cohetes
        if(SceneManager.GetActiveScene().name == "TareaBengalasGame")
        {
            //mientras que no supere numero de tiradas maximas
            if(numeroAlturaCohetesRegistradas<= numeroTiradasTotal)
            {
                float timeBengalaVidaRedondeado = Mathf.Round(timeBengalaVida * 10) / 10.0f;
                alturaCohetes[numeroAlturaCohetesRegistradas] = timeBengalaVidaRedondeado;
                numeroAlturaCohetesRegistradas++;

                if(numeroAlturaCohetesRegistradas == numeroTiradasTotal)
                {
                    mongoDB.GetComponent<InfoBengalasMongoDB>().RecolectarArgumentosBengalas();
                }         
            }
        }
        //efectosVFX despegue
        bengalaParaDespegar.GetComponent<ComportamientoBengalaADisparar>().DespegueVFX();
        bengalaParaDespegar.GetComponent<ComportamientoBengalaADisparar>().PrepararPropulsion();
        //sonido vuelo despegue
        if(AudioManagerBengalas.instance != null)
        {
            AudioManagerBengalas.instance.PlaySFX("vueloDespegue", 1f);
        }
        
    }

    //metodo que pone en pos inicial la bengala y la activa de nuevo
    public void SiguienteLanzamiento()
    {
        //lo ponemos visible
        bengalaParaDespegar.GetComponent<SpriteRenderer>().DOFade(1f, 0.1f);
        //Activa bengala
        bengalaParaDespegar.SetActive(true);
        //la pone en pos inicial
        bengalaParaDespegar.transform.position = posInicialBengala;
        //activamos boton de nuevo para poder jugar otra vez
        UITareaBengalas.SetBoton(true);

        //se quita una en el UIManager para que aparezca en pantalla
        UITareaBengalas.ActualizarTextoBengalasLeft((numeroTiradasTotal - contadorNumeroTiradas).ToString());
        //vemos si se ha acabado ya, esto es si lanzadas de prueba se han acabado
        if (contadorNumeroTiradas>=numeroTiradasTotal)
        {
            Debug.Log("YOU WIN!");
            //llama a metodo que desactiva jugabilidad
            Invoke("DesactivarJugabilidad", 0.25f);
        }
    }

    public void DesactivarJugabilidad()
    {
        DesactivarJugabilidadFinPractica();
    }


    public void GuardarUltimaPosicionBengalaDisparada(Vector3 posicion)
    {
        //guardamos ultima posicion bengala
        Vector3 lastPosBengala = posicion;
        //activamos gameObject marcador adecuado
        //marcadoresTiradas[contadorNumeroTiradas].gameObject.SetActive(true);
        //conectamos con marcador adecuado segun tirada y le pasamos la info de lastPosBengala
        marcadoresTiradas[contadorNumeroTiradas].RegisterUltimaPosBengala(lastPosBengala);
        //quitamos 1 bengala del suelo al haberse utilizado
        listaBengalasLeft[contadorNumeroTiradas].SetActive(false);
        //sumamos una tirada mas hecha
        contadorNumeroTiradas++;
    }


    //devuelve tiempo de vida o altura que llegará el cohete
    public float[] AlturasCohetes()
    {
        return alturaCohetes;
    }

    public void DesactivarJugabilidadFinPractica()
    {
        //desactivamos boton start y cohete
        botonStart.SetActive(false);
        bengalaParaDespegar.SetActive(false);
        //quitamos interfaz
        UITareaBengalas.QuitarInterfaz();
        //quitamos marcadores
        marcadores.SetActive(false);
        //activamos panel de pasar a modo juego de verdad
        //si la escena es la de bengalas prueba se puede pasar
        if(SceneManager.GetActiveScene().name == "BengalasPrueba")
        {
            UITareaBengalas.ActivarPanelPasarModoJuego();
        }
        //sino ponemos felicidades has ganado
        else
        {
            UITareaBengalas.SetEnd(true);
        }
        
    }


    public void PasarEscenaBengalasJuego()
    {
        // Cargamos la escena aditiva
        Invoke("EscenaBengalasTrasPausa", 1f);     

    }

    private void EscenaBengalasTrasPausa()
    {
        SceneManager.LoadScene("TareaBengalasGame");
    }

    public void ApareceDialogoRobotAlejadoAviso()
    {
        UITareaBengalas.ApareceDialogoRobotAlejadoAviso();
    }

    //metodo que guardará el numero de segundos totales de partida que lleva
    public void NumSecsPartidaBengalas(int secsPartida)
    {
        numSecsPartidaBengalas = secsPartida;
    }

    //tiempo de duracion de partida bengalas
    public int TiempoPartidaBengalas()
    {
        return numSecsPartidaBengalas;
    }

    public void PasarSiguienteEscena()
    {
        //LevelLoader.LoadLevel("escenaIntro");
        SceneManager.LoadScene("3.8Rescate");
    }


}
