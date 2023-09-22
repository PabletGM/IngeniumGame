using UnityEngine;

public class MoverCamaraArriba : MonoBehaviour
{
    public float velocidad = 2.0f; // Velocidad de movimiento de la c�mara hacia arriba
    public float limiteSuperior = 10.0f; // L�mite superior de movimiento de la c�mara

    private void Update()
    {
        // Obt�n la posici�n actual de la c�mara
        Vector3 posicionCamara = transform.position;

        // Calcula la nueva posici�n de la c�mara
        Vector3 nuevaPosicion = posicionCamara + Vector3.up * velocidad * Time.deltaTime;

        // Limita la posici�n de la c�mara al l�mite superior
        nuevaPosicion.y = Mathf.Min(nuevaPosicion.y, limiteSuperior);

        // Establece la nueva posici�n de la c�mara
        transform.position = nuevaPosicion;
    }
}
