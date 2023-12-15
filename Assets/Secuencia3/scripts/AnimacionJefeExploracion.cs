using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class AnimacionJefeExploracion : MonoBehaviour
{
    [Header("DoTweenAnimations")]
    [SerializeField]
    private DOTweenAnimation DOScaleAstronautaJefe;
    [SerializeField]
    private DOTweenAnimation DOMoveAstronautaJefe;

    [Header("DoTweenAnimations")]
    [SerializeField]
    private float duracionPasoSiguienteEscena;

    public void ActivarJefeExploracionn()
    {

        DOScaleAstronautaJefe.DOPlay();
        DOMoveAstronautaJefe.DOPlay();


        Invoke("NextEffect", duracionPasoSiguienteEscena);
    }

    private void NextEffect()
    {
        //pasar siguiente escena
        SceneManager.LoadScene("3.2ConversacionJefeExploracion");
    }
}
