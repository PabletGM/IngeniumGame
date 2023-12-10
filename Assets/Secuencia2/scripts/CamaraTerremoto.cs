using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraTerremoto : MonoBehaviour
{
    public float duracionTerremoto = 1.0f;
    public float intensidad = 0.1f;

    private Vector3 posicionInicial;
    private float tiempoInicio;

    void Start()
    {
        // Guardamos la posición inicial de la cámara
        posicionInicial = transform.position;
        IniciarTerremoto();
    }

    void Update()
    {
        if (Time.time - tiempoInicio < duracionTerremoto)
        {
            // Simulamos el efecto de terremoto cambiando la posición de la cámara
            float offsetX = Random.Range(-intensidad, intensidad);
            float offsetY = Random.Range(-intensidad, intensidad);
            transform.position = new Vector3(posicionInicial.x + offsetX, posicionInicial.y + offsetY, posicionInicial.z);
        }
        else
        {
            // Restauramos la posición original de la cámara
            transform.position = posicionInicial;
        }
    }

    public void IniciarTerremoto()
    {
        // Iniciamos el efecto de terremoto
        tiempoInicio = Time.time;
    }
}
