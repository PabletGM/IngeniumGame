using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoBengalaADisparar : MonoBehaviour
{
    private float velocidad = 1f;
    private float velocidadInicial = 1f;
    private float aceleracion = 0.25f;
    private bool permisoParaDespegar = false;
    private float tiempoParaAcelerar = 0f;
    private float tiempoMaxParaAcelerar = 0.5f;

    GameManagerTareaBengalas _myGameManagerBengalas;

    private SpriteRenderer spriteRenderer;


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
    public void DespegarCohete()
    {
        permisoParaDespegar = true;
    }

    private void Update()
    {
        if(permisoParaDespegar && this.gameObject!=null)
        {
            //le aplicamos velocidad y lo movemos en una direccion
            transform.Translate(Vector2.up * velocidad * Time.deltaTime);

            tiempoParaAcelerar += Time.deltaTime;
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
    public void ExplosionCohete()
    {
        //Destroy(this.gameObject);

        //quitamos vfx de cohete
        vfxAcelerar.SetActive(false);
        vfxDespegue.SetActive(false);
        //quitar opacidad
        spriteRenderer.DOFade(0f, 0.2f);
        //quitamos
        //explosion
        ExplosionCoheteVFX();
        //repetir jugada
        Invoke("RepetirLanzada", 0.5f);
        
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
