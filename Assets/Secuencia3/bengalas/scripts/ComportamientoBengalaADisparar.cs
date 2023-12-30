using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoBengalaADisparar : MonoBehaviour
{
    private float velocidad = 1.25f;
    private float velocidadInicial = 1.25f;
    private float aceleracion = 0.5f;
    private bool permisoParaDespegar = false;
    private float tiempoParaAcelerar = 0f;
    private float tiempoMaxParaAcelerar = 0.5f;

    //tiempo en que el cohete subirá hasta explotar
    private float LifeTime=0;
    //tiempo maximo de vida del cohete
    private float LifeTimeMax = 2.5f;
    //tiempo que lleva vivo el cohete
    private float SegundosVidaCohete = 0;

    GameManagerTareaBengalas _myGameManagerBengalas;

    private SpriteRenderer spriteRenderer;

    //booleano para que cada comportamiento del update lo haga una vez por cohete
    private bool turnoCoheteComportamiento = false;


    void Start()
    {
        _myGameManagerBengalas = GameManagerTareaBengalas.GetInstanceGM();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    #region vfx
    [SerializeField]
    private GameObject vfxDespegue;
    [SerializeField]
    private GameObject vfxAcelerar;
    [SerializeField]
    private GameObject vfxExplosion;
    [SerializeField]
    private GameObject vfxExplosion2;
    #endregion

    #region DespegueAceleracionCohete
    public void DespegarCohete(float lifetimeToExplote)
    {
        //ponemos permiso para despegar
        permisoParaDespegar = true;
        //ponemos tiempo de vida del cohete
        LifeTime = lifetimeToExplote;
    }

    private void Update()
    {
        if(permisoParaDespegar && this.gameObject!=null)
        {
            //le aplicamos velocidad y lo movemos en una direccion
            transform.Translate(Vector2.up * velocidad * Time.deltaTime);

            //sumamos tiempo para acelerar cada cierto tiempo
            tiempoParaAcelerar += Time.deltaTime;
            //sumamos tiempo hasta que contador llegue a lifeTimeToExplote
            SegundosVidaCohete += Time.deltaTime;

            //comprobacion a ver si segundosVidaCohete > LifeTime
            if(SegundosVidaCohete > LifeTime || SegundosVidaCohete > LifeTimeMax)
            {
                //por defecto no se ha pasado de distancia
                bool seHaPasadoDistancia = false;
                //si se ha pasado de distancia
                if(SegundosVidaCohete > LifeTimeMax)
                {
                    //aparece dialogo robot y hacemos desaparecer el cohete sin explosion
                    _myGameManagerBengalas.ApareceDialogoRobotAlejadoAviso();
                    //se ha pasado de distancia
                    seHaPasadoDistancia = true;
                }
                //Destruimos cohete
                if(!turnoCoheteComportamiento)
                {
                    ExplosionCoheteBengala(seHaPasadoDistancia);
                    turnoCoheteComportamiento = true;
                }
                
                //reiniciamos contadores
                LifeTime = 0;
                SegundosVidaCohete = 0;
            }

            //cada cierto tiempo aceleramos, si llega al limite de la variable se acelera
            if(tiempoParaAcelerar>tiempoMaxParaAcelerar)
            {
                velocidad += aceleracion;
                //se reinicia contador
                tiempoParaAcelerar=0;
            }
        }
    }

    public void DespegueVFX()
    {
        vfxDespegue.SetActive(true);
        vfxDespegue.GetComponent<ParticleSystem>().Play();
        //vfxDespegue.GetComponent<ParticleSystem>().Stop();
        //vfxDespegue.SetActive(false);
    }

    //invocas metodo en un segundo
    public void PrepararPropulsion()
    {
        Invoke("AcelerarCohete", 0.1f);
    }

    public void AcelerarCohete()
    {
        vfxAcelerar.SetActive(true);
        vfxAcelerar.GetComponent<ParticleSystem>().Play();
    }

    #endregion

    #region ExplosionCohete
    //destruimos el cohete
    public void ExplosionCoheteBengala(bool seHaPasadoDistanciaLimite)
    {
        //paramos sonido
        if(AudioManagerBengalas.instance!= null)
        {
            AudioManagerBengalas.instance.StopSFX();
            AudioManagerBengalas.instance.PlaySFX("explosion", 1f);
        }
        



        //comprobacion si nos hemos pasado el limite
        if(seHaPasadoDistanciaLimite)
        {
            permisoParaDespegar = true;
        }
        //sino la hemos pasado comportamiento normal
        else
        {
            //le quitamos permiso para despegar para que no entre en bucle de update 
            permisoParaDespegar = false;
        }
        



        //Antes de destruir la bengala guardamos su ultima posicion registrada y la compartimos con GameManager
        _myGameManagerBengalas.GuardarUltimaPosicionBengalaDisparada(this.transform.position);




        //comprobacion si nos hemos pasado el limite
        if (seHaPasadoDistanciaLimite)
        {
            Invoke("CoheteAsciendeSinFuncionalidad", 1f);
        }
        //sino lo hemos pasado el limite de las nubes comportamiento normal
        else
        {
            //quitamos vfx de cohete
            vfxAcelerar.SetActive(false);
            vfxDespegue.SetActive(false);
            //quitar opacidad
            spriteRenderer.DOFade(0f, 0.2f);
        }

       
        //quitamos
        //explosion
        //sino se ha pasado de dstancia hacemos explosion, si se ha pasado no hacemos explosion
        if(!seHaPasadoDistanciaLimite)
        {
            ExplosionCoheteVFX();
        } 




        //comprobacion si nos hemos pasado el limite
        if (seHaPasadoDistanciaLimite)
        {
            //repetir jugada
            Invoke("RepetirLanzada", 1.5f);
        }
        //sino lo hemos pasado el limite de las nubes comportamiento normal
        else
        {
            //repetir jugada
            Invoke("RepetirLanzada", 0.5f);
        }
    }

    //si nos hemos pasado de distancia dejamos que el cohete se vaya a las nubes
    private void CoheteAsciendeSinFuncionalidad()
    {
        //quitamos vfx de cohete
        vfxAcelerar.SetActive(false);
        vfxDespegue.SetActive(false);
        //quitar opacidad
        spriteRenderer.DOFade(0f, 0.2f);
    }

    public void ExplosionCoheteVFX()
    {
        vfxExplosion.SetActive(true);
        vfxExplosion.GetComponent<ParticleSystem>().Play();
        vfxExplosion2.SetActive(true);
        vfxExplosion2.GetComponent<ParticleSystem>().Play();

    }

    public void RepetirLanzada()
    {
        //volvemos a activar posibilidad lanzamiento automático en siguiente jugada y activamos boton
        UIManagerTareaBengalas.GetInstanceUI().SetBoton(true);
        GameManagerTareaBengalas.GetInstanceGM().SetBotonStartPermisoParaLanzamientoAutomatico(true);

        turnoCoheteComportamiento = false;

        //por ahora en vez de destruir el objeto simplemente lo desactivamos
        this.gameObject.SetActive(false);

        //le quitamos permiso para despegar
        permisoParaDespegar = false;

        //ponemos velocidad a velocidadInicial
        velocidad = velocidadInicial;

        //llamaremos a metodo del GameManager que se llamará Siguiente lanzamiento
        _myGameManagerBengalas.SiguienteLanzamiento();
    }

    #endregion
}
