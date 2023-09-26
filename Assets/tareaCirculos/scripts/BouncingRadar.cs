using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingRadar : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del sprite
    public float minX = -1.5f; // Límite izquierdo de la pantalla
    public float maxX = 1.5f;  // Límite derecho de la pantalla
    public float minY = -0.2f; // Límite inferior de la pantalla
    public float maxY = 0.2f;  // Límite superior de la pantalla

    private Vector2 direction = Vector2.right; // Dirección inicial del movimiento

    void Update()
    {
        // Calcula la nueva posición del sprite
        Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;

        // Verifica si el sprite ha alcanzado los límites de la pantalla
        if (newPosition.x < minX || newPosition.x > maxX)
        {
            // Cambia la dirección horizontal para invertir el movimiento
            direction.x *= -1;
        }

        if (newPosition.y < minY || newPosition.y > maxY)
        {
            // Cambia la dirección vertical para invertir el movimiento
            direction.y *= -1;
        }

        // Actualiza la posición del sprite
        transform.position = newPosition;
    }
}
