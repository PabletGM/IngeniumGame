using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{

    [SerializeField] int min, seg;
    [SerializeField] Text tiempo;

    private float restante;
    private bool enMarcha = false;

    [SerializeField]
    private int tiempoMaximo;

    GameManager _myGameManager;

    CapacidadAdaptacionManager _myCapacidadAdaptacionManager;

    ConfianzaManager _myConfianzaManager;

    private string testActualCapacidadAdaptacion = "test1";
    private string testActualConfianza = "test1";

    GameManagerTareaBengalas _myGameManagerBengalas;

    GameManagerCirculos _myGameManagerCirculos;

    PuntuacionTest3EnRaya _myGameController3EnRaya;

    private void Awake()
    {
        restante = 0;

        //si es en escena circulosNave,circulosNaveNivel2,circulosNaveNivel3 o escenaFinal no se destruye
        if (SceneManager.GetActiveScene().name == "circulosNave" || SceneManager.GetActiveScene().name == "circulosNaveNivel2" || SceneManager.GetActiveScene().name == "circulosNaveNivel3")
        {
            DontDestroyOnLoad(this.gameObject);
        }
        //o si el 3EnRaya
        else if (SceneManager.GetActiveScene().name == "mecanicas3EnRayaModoDificil" || SceneManager.GetActiveScene().name == "mecanicas3EnRayaModoFacil" || SceneManager.GetActiveScene().name == "mecanicas3EnRayaModoMedio")
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        
        _myGameManagerBengalas = GameManagerTareaBengalas.GetInstanceGM();
        _myGameManager = GameManager.GetInstance();
        //si la escena activa es CapacidadAdaptacion o confianza se llama al timer inmediatamente
        if(SceneManager.GetActiveScene().name =="capacidadDeAdaptacion" || SceneManager.GetActiveScene().name == "confianza")
        {
            _myCapacidadAdaptacionManager = CapacidadAdaptacionManager.GetInstanceCapacidadAdaptacionManager();
            _myConfianzaManager = ConfianzaManager.GetInstanceConfianzaManager();
            ActivarTimer();
        }
        //si es en circulos
        else if(SceneManager.GetActiveScene().name == "circulosNave")
        {
            ActivarTimer();
        }
        //si es en 3EnRaya
        else if (SceneManager.GetActiveScene().name == "mecanicas3EnRayaModoDificil")
        {
            ActivarTimer();
        }

    }

    public void ActivarTimer()
    {
        enMarcha = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(enMarcha)
        {
            //va sumando segundos
            restante += Time.deltaTime;
            //informamos del tiempo al gameManager en cada segundo
            InformarTimeGameManager();
            //en caso de superar el tiempo maximo 90 segundos
            if(restante >tiempoMaximo)
            {
                enMarcha = false;
            }
            //minutos que llevamos
            int tempMin = Mathf.FloorToInt(restante / 60);
            //segundos
            int tempSeg = Mathf.FloorToInt(restante % 60);
            //cambiamos texto
            tiempo.text = string.Format("{00:00}:{01:00}", tempMin, tempSeg);
        }
    }

    public void InformarTimeGameManager()
    {
        if(SceneManager.GetActiveScene().name == "hoyos")
        {
            _myGameManager.NumSecsPartida((int)restante);
        }
        else if(SceneManager.GetActiveScene().name == "capacidadDeAdaptacion")
        {
            //test1 Capacidad adaptacion
            if(TestActualCapacidadAdaptacion() == "test1")
            {
                
                //test1 capacidad adaptacion
                _myCapacidadAdaptacionManager.NumSecsPartidaCapacidad1((int)restante);
            }
            else if(TestActualCapacidadAdaptacion() == "test2")
            {
                //test2 capacidad adaptacion
                _myCapacidadAdaptacionManager.NumSecsPartidaCapacidad2((int)restante);
            }
        }
        else if (SceneManager.GetActiveScene().name == "confianza")
        {
            //test1 Capacidad adaptacion
            if (TestActualConfianza() == "test1")
            {
                //test1 capacidad adaptacion
                _myConfianzaManager.NumSecsPartidaConfianza1((int)restante);
            }
            else if (TestActualConfianza() == "test2")
            {
                //test2 capacidad adaptacion
                _myConfianzaManager.NumSecsPartidaConfianza2((int)restante);
            }
            
        }
        else if(SceneManager.GetActiveScene().name == "TareaBengalasGame")
        {
            _myGameManagerBengalas.NumSecsPartidaBengalas((int)restante);
        }
        else if (SceneManager.GetActiveScene().name == "circulosNave" )
        {
            //se vuelve a unir la instancia
            _myGameManagerCirculos = GameManagerCirculos.GetInstanceGM();

            if(_myGameManagerCirculos != null)
            {
                _myGameManagerCirculos.NumSecsPartidaCirculos((int)restante);
            }
           
        }
        else if (SceneManager.GetActiveScene().name == "mecanicas3EnRayaModoDificil")
        {
            //se vuelve a unir la instancia
            _myGameController3EnRaya = PuntuacionTest3EnRaya.GetInstanceGM();

            if (_myGameController3EnRaya != null)
            {
                _myGameController3EnRaya.NumSecsPartida3EnRaya((int)restante);
            }

        }

    }

    public void CambiarDeTest1CapACap2(string testActual)
    {
        testActualCapacidadAdaptacion = testActual;
    }
    //devuelve el nombre del testActual CapacidadAdaptacion
    public string TestActualCapacidadAdaptacion()
    {
        return testActualCapacidadAdaptacion;
    }

    public void CambiarDeTest1ConfAConf2(string testActual)
    {
        testActualConfianza = testActual;
    }
    //devuelve el nombre del testActual CapacidadAdaptacion
    public string TestActualConfianza()
    {
        return testActualConfianza;
    }

    public void ReiniciarContador()
    {
        restante = 0;
    }
}
