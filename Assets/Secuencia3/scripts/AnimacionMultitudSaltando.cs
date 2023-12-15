using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimacionMultitudSaltando : MonoBehaviour
{
    [Header("DoTweenAnimations")]
    [SerializeField]
    private DOTweenAnimation DOJumpAstronauta1;
    [SerializeField]
    private DOTweenAnimation DOJumpAstronauta2;
    [SerializeField]
    private DOTweenAnimation DOJumpAstronauta3;
    [SerializeField]
    private DOTweenAnimation DOJumpAstronauta4;
    [SerializeField]
    private DOTweenAnimation DOJumpAstronauta5;
    [SerializeField]
    private DOTweenAnimation DOJumpAstronauta6;
    [SerializeField]
    private DOTweenAnimation DOJumpAstronauta7;
    [SerializeField]
    private DOTweenAnimation DOJumpAstronauta8;




    public void ActivarMultitudPersonas()
    {

        DOJumpAstronauta1.DOPlay();
        DOJumpAstronauta2.DOPlay();
        DOJumpAstronauta3.DOPlay();
        DOJumpAstronauta4.DOPlay();
        DOJumpAstronauta5.DOPlay();
        DOJumpAstronauta6.DOPlay();
        DOJumpAstronauta7.DOPlay();
        DOJumpAstronauta8.DOPlay();

        //activar LO SIGUIENTE
        //AnimacionesManagerSecuencia3.animsSecuencia3instance.ActivarPersonasBajandoRampa();
    }


}
