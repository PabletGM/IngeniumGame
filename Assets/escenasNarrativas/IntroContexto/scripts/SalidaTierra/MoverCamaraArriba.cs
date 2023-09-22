using UnityEngine;

public class MoverCamaraArriba : MonoBehaviour
{
    public float velocidad = 2.0f; // Velocidad de movimiento de la cámara hacia arriba
    public float limiteSuperior = 10.0f; // Límite superior de movimiento de la cámara

    private void Update()
    {
        // Obtén la posición actual de la cámara
        Vector3 posicionCamara = transform.position;

        // Calcula la nueva posición de la cámara
        Vector3 nuevaPosicion = posicionCamara + Vector3.up * velocidad * Time.deltaTime;

        // Limita la posición de la cámara al límite superior
        nuevaPosicion.y = Mathf.Min(nuevaPosicion.y, limiteSuperior);

        // Establece la nueva posición de la cámara
        transform.position = nuevaPosicion;
    }
}
