using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionRampaObjetos : MonoBehaviour
{
    [Header("Animaciones ")]
    [SerializeField]
    private Animator animRampa;

    [Header("DuracionAnimaciones")]
    [SerializeField]
    private float duracionAnimRampa;

    #region AnimacionEscalera



    public void ActivarAnimacionEscalera()
    {
        AnimacionEscalera();
    }

    private void AnimacionEscalera()
    {

        //transform.DOMove(Vector3.zero, 1f).OnComplete(() => QuitAnimacionEscalera());
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

        //activamos animacion cajas
        AnimacionesManagerSecuencia3.animsSecuencia3instance.ActivarAnimacionCajas();
    }

    #endregion
}
