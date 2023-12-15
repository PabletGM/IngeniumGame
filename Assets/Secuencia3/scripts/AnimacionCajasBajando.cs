using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionCajasBajando : MonoBehaviour
{
    [Header("Animaciones ")]
    [SerializeField]
    private Animator animCajasRampa;

    [Header("DuracionAnimaciones")]
    [SerializeField]
    private float duracionCajasRampa;

    #region AnimacionCajas



    public void ActivarAnimacionCajasBajando()
    {
        AnimacionCajas();
    }


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

        //activamos siguiente animacion que es rampa personas
        AnimacionesManagerSecuencia3.animsSecuencia3instance.ActivarRampaPersonas();


    }

    #endregion
}
