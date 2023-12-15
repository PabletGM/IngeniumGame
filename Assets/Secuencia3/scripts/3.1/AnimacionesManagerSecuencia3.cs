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
        ActivarAnimacionEscaleraObjetos();
    }

    public void ActivarAnimacionEscaleraObjetos()
    {
        //iniciamos primera animacion ESCALERA OBJETOS
        listaDeGameObjectsAnimacionOrden[0].GetComponent<AnimacionRampaObjetos>().ActivarAnimacionEscalera();
    }

    public void ActivarAnimacionCajas()
    {
        //iniciamos primera animacion ESCALERA OBJETOS
        listaDeGameObjectsAnimacionOrden[1].GetComponent<AnimacionCajasBajando>().ActivarAnimacionCajasBajando();
    }

    public void ActivarRampaPersonas()
    {
        //iniciamos primera animacion ESCALERA OBJETOS
        listaDeGameObjectsAnimacionOrden[2].GetComponent<AnimacionRampaPersonas>().ActivarRampaPersonas();
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
