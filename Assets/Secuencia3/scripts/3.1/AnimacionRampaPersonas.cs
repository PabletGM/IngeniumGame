using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionRampaPersonas : MonoBehaviour
{
    [Header("DoTweenAnimations")]
    [SerializeField]
    private DOTweenAnimation DOScaleRampaPersonas;
    [SerializeField]
    private DOTweenAnimation DOMoveRampaPersonas;




    public void ActivarRampaPersonas()
    {
        //activamos rampa personas
        Debug.Log(DOScaleRampaPersonas);
        Debug.Log(DOMoveRampaPersonas);
        DOScaleRampaPersonas.DOPlay();
        DOMoveRampaPersonas.DOPlay();

        //activar personas bajando rampa
        AnimacionesManagerSecuencia3.animsSecuencia3instance.ActivarPersonasBajandoRampa();
    }


}
