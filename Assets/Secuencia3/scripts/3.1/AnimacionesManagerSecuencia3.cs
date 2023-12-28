using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesManagerSecuencia3 : MonoBehaviour
{
    public static AnimacionesManagerSecuencia3 animsSecuencia3instance;

    //de primera animacion a ultima animacion
    public List<GameObject> listaDeGameObjectsAnimacionOrden;

    [SerializeField]
    public List<GameObject> listaGOEscena;

    private int numeroCajasTiradasAnimacion = 4;


    private void Awake()
    {

        if (animsSecuencia3instance == null)
        {
            animsSecuencia3instance = this;
          
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        CancionSecuencia3Intro();
        ActivarAnimacionEscaleraObjetos();
    }

    private void CancionSecuencia3Intro()
    {
        //musica inicial intro
        AudioManagerBengalas.instance.PlayMusic("introSecuencia3", 1f);
    }

    public void ActivarAnimacionEscaleraObjetos()
    {
        //humo
        HumoIntroVFX();
        //rampa
        Invoke("RampaIntroVFX", 0.2f);
        //AudioManagerBengalas.instance.PlaySFX("rampa");
        //iniciamos primera animacion ESCALERA OBJETOS
        listaDeGameObjectsAnimacionOrden[0].GetComponent<AnimacionRampaObjetos>().ActivarAnimacionEscalera();
        Invoke("StopRampaIntroVFX", 1f);
    }

    private void HumoIntroVFX()
    {
        AudioManagerBengalas.instance.PlaySFX("humo", 1f);
    }

    private void RampaIntroVFX()
    {
        AudioManagerBengalas.instance.PlaySFX("rampa", 0.5f);
    }

    private void StopRampaIntroVFX()
    {
        AudioManagerBengalas.instance.StopSFX();
    }

    private void StopRampaIntroVFX2()
    {
        AudioManagerBengalas.instance.StopSFX();
        AudioManagerBengalas.instance.PlaySFX("hello", 1f);
    }

    public void ActivarAnimacionCajas()
    {
        
        //iniciamos primera animacion ESCALERA OBJETOS
        listaDeGameObjectsAnimacionOrden[1].GetComponent<AnimacionCajasBajando>().ActivarAnimacionCajasBajando();
        //sonido cajas
        Invoke("CajasVFX", 0.8f);
    }

    private void CajasVFX()
    {
        AudioManagerBengalas.instance.PlaySFX("cajaSaltando", 1f);
        Invoke("CajasVFX2", 0.8f);
    }

    private void CajasVFX2()
    {
        AudioManagerBengalas.instance.PlaySFX("cajaSaltando", 1f);
        Invoke("CajasVFX3", 0.8f);
    }

    private void CajasVFX3()
    {
        AudioManagerBengalas.instance.PlaySFX("cajaSaltando", 1f);
        Invoke("CajasVFX4", 0.8f);
    }

    private void CajasVFX4()
    {
        AudioManagerBengalas.instance.PlaySFX("cajaSaltando", 1f);
    }

    public void ActivarRampaPersonas()
    {
        Invoke("RampaIntroVFX", 0.2f);
        //iniciamos primera animacion ESCALERA OBJETOS
        listaDeGameObjectsAnimacionOrden[2].GetComponent<AnimacionRampaPersonas>().ActivarRampaPersonas();
        Invoke("StopRampaIntroVFX", 1f);
    }

    public void ActivarPersonasBajandoRampa()
    {
        listaDeGameObjectsAnimacionOrden[3].GetComponent<AnimacionAstronautasBajandoNave>().ActivarPersonasBajandoRampa();
    }

    public void ActivarMultitudPersonasObjetos()
    {
        //quitamos objetos anteriores
        listaDeGameObjectsAnimacionOrden[0].SetActive(false);
        listaDeGameObjectsAnimacionOrden[1].SetActive(false);
        listaDeGameObjectsAnimacionOrden[2].SetActive(false);
        listaDeGameObjectsAnimacionOrden[3].SetActive(false);
        //activamos multitud
        listaGOEscena[0].SetActive(true);
        //efecto multitud personas
        listaDeGameObjectsAnimacionOrden[4].GetComponent<AnimacionMultitudSaltando>().ActivarMultitudPersonas();
        MultitudVFX();
        //a los 3 o 4 segundos
        Invoke("StopRampaIntroVFX2", 3.2f);
        
    }

    private void HelloVFX()
    {
        Debug.Log("G");
        
    }

    private void MultitudVFX()
    {
        AudioManagerBengalas.instance.PlaySFX("multitud", 1f);
    }

    public void ActivarJefeExploracionZoom()
    {
        //activamos jefe de exploracion
        listaGOEscena[1].SetActive(true);
        listaDeGameObjectsAnimacionOrden[5].GetComponent<AnimacionJefeExploracion>().ActivarJefeExploracionn();
        
    }




    #region AnimacionAcercarCamara
    private void SetAnimacionProtaAcercaCamara()
    {

    }

    #endregion


    #region Animacion SitiosCajasPersonasColocar
    private void ColocacionSitiosCajasPersonasAnimacion()
    {

    }

    #endregion

    #region SetAnimacionMultitud
    private void SetAnimacionMiltitud()
    {

    }

    #endregion
}
