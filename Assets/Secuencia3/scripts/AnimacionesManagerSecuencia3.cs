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




    #region AnimacionAcercarCamara
    private void SetAnimacionProtaAcercaCamara()
    {

    }

    #endregion

    #region AnimacionPersonasBajando
    private void SetAnimacionPersonasBajando()
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
