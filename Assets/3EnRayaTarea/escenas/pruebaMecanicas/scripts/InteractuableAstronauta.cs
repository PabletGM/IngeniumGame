using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuableAstronauta : MonoBehaviour
{
    public float velocidad = 0.5f; // Velocidad del movimiento
    public float distancia = 0.3f; // Distancia de movimiento vertical

    private Vector3 posicionInicial;
    private bool moviendoseArriba = true;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        if (moviendoseArriba)
        {
            transform.Translate(Vector3.up * velocidad * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.down * velocidad * Time.deltaTime);
        }

        if (transform.position.y >= posicionInicial.y + distancia)
        {
            moviendoseArriba = false;
        }
        else if (transform.position.y <= posicionInicial.y - distancia)
        {
            moviendoseArriba = true;
        }
    }
}
