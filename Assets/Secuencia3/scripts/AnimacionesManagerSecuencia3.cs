using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesManagerSecuencia3 : MonoBehaviour
{

    [Header("Animaciones ")]
    [SerializeField]
    private Animator animRampa;

    [SerializeField]
    private Animator animCajasRampa;


    [Header("DuracionAnimaciones")]
    [SerializeField]
    private float duracionAnimRampa;

    [SerializeField]
    private float duracionCajasRampa;
    // Start is called before the first frame update
    void Start()
    {
        //Iniciamos escaleras
        AnimacionEscalera();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetAnimacionProtaAcercaCamara()
    {

    }

    #region AnimacionEscalera

    private void AnimacionEscalera()
    {
       
        //transform.DOMove(Vector3.zero, 1f).on  OnComplete(() => QuitAnimacionEscalera());
        //activamos animacion
        SetAnimacionEscalera(true);
        //desactivamos animacion al acabar duracion
        Invoke("QuitAnimacionEscalera", duracionAnimRampa);
    }

    private void SetAnimacionEscalera(bool set)
    {
        //se activa animacion rampa
        animRampa.enabled = set;
       
    }

    private void QuitAnimacionEscalera()
    {
        //se quita animacion rampa
        animRampa.enabled = false;
        
        //comenzar animacion cajas
        //activamos animacion
        AnimacionCajas();
    }

    #endregion


    #region AnimacionCajas

    private void AnimacionCajas()
    {
        //activamos animacion
        SetAnimacionCajas(true);
        //desactivamos animacion al acabar duracion
        Invoke("QuitAnimacionCajas", duracionCajasRampa);
    }

    private void SetAnimacionCajas(bool set)
    {
        animCajasRampa.enabled = set;
    }

    private void QuitAnimacionCajas()
    {
        //se quita animacion cajas
        animCajasRampa.enabled = false;
        
    }

    #endregion



    private void SetAnimacionPersonasBajando()
    {

    }

    private void ColocacionSitiosCajasPersonasAnimacion()
    {

    }

    private void SetAnimacionMiltitud()
    {

    }
}
