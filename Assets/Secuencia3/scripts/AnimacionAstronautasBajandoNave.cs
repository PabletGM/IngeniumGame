using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionAstronautasBajandoNave : MonoBehaviour
{
    [Header("DoTweenAnimations")]
    [SerializeField]
    private DOTweenAnimation DOMovePersona1;
    [SerializeField]
    private DOTweenAnimation DOMovePersona2;
    [SerializeField]
    private DOTweenAnimation DOMovePersona3;

    [Header("DuracionCambioAimacion")]
    [SerializeField]
    private float duracionCambioAnimacion;

    public void ActivarPersonasBajandoRampa()
    {
        //activamos rampa personas
        DOMovePersona1.DOPlay();
        DOMovePersona2.DOPlay();
        DOMovePersona3.DOPlay();
        //solo deja hacer OnComplete si la funcion tween no es void como DoPlay
        //transform.DOMove(DOMovePersona3.transform.position,0f).OnComplete(() => NextAnimation());

        Invoke("NextAnimation", duracionCambioAnimacion);


    }

    private void NextAnimation()
    {
        //al acabar llamamos a MultitudSaltando
        AnimacionesManagerSecuencia3.animsSecuencia3instance.ActivarMultitudPersonasObjetos();
    }
}
