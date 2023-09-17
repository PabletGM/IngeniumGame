using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float timeBengalaVida = 0;

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
    }

    static public GameManagerTareaBengalas GetInstanceGM()
    {
        return _instanceGMTareaBengalas;
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

    public void MecanicaCohete()
    {
        //llama a la funcion del cohete que lo propulsa para arriba

        //permiso para despegar cohete
        //preguntamos a boton que tiempo vivirá el cohete
        bengalaParaDespegar.GetComponent<ComportamientoBengalaADisparar>().DespegarCohete(timeBengalaVida);
        //efectosVFX despegue
        bengalaParaDespegar.GetComponent<ComportamientoBengalaADisparar>().DespegueVFX();
        bengalaParaDespegar.GetComponent<ComportamientoBengalaADisparar>().PrepararPropulsion();
    }

    public void ExplosionCohete()
    {
        ////desactivamos boton mientras la explosion para no poder pulsar boton
        //UITareaBengalas.SetBoton(false);
        //Explota Cohete
        bengalaParaDespegar.GetComponent<ComportamientoBengalaADisparar>().ExplosionCohete();
    
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
    }


}
