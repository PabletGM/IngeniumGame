using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoNubes : MonoBehaviour
{
    
    public Vector3 scaleAumento = new Vector3(2f, 2f, 2f); // Escala a la que se aumentará
    public float duracionAumento = 1f; // Duración del aumento
    public float intervaloRepeticion = 2f; // Intervalo entre repeticiones

    //size original del objeto
    private Vector3 escalaOriginal;

    private void Start()
    {
        // Obtiene la escala original del objeto
        escalaOriginal = transform.localScale;

        // Inicia la repetición de la animación
        InvokeRepeating("RealizarAnimacion", 0f, intervaloRepeticion);
    }

    private void RealizarAnimacion()
    {
        // Aumenta la escala del objeto
        transform.DOScale(scaleAumento, duracionAumento)
            .OnComplete(() => ReducirEscalayRepetir());
    }

    private void ReducirEscalayRepetir()
    {
        // Reduce la escala del objeto de nuevo a la escala original
        transform.DOScale(escalaOriginal, duracionAumento);
    }
}
