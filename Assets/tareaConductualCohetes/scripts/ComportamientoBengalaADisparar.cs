using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoBengalaADisparar : MonoBehaviour
{
    private float velocidad = 1f;
    private float aceleracion = 0.25f;
    private bool permisoParaDespegar = false;
    private float tiempoParaAcelerar = 0f;
    private float tiempoMaxParaAcelerar = 0.5f;


    #region vfx
    [SerializeField]
    private GameObject vfxDespegue;
    [SerializeField]
    private GameObject vfxAcelerar;
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
        Destroy(this.gameObject);
    }

    #endregion
}
