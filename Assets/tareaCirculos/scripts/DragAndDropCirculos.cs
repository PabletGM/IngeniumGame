using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropCirculos : MonoBehaviour
{
    private Vector3 mousePositionOffset;//area para coger objeto
    private Vector3 lastValidPosition; // Guarda la �ltima posici�n v�lida del objeto
    private bool isDragging = false;

    // Para el l�mite de movimiento
    [SerializeField]
    private CircleCollider2D circleCollider;

    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Start()
    {
    }

    private void OnMouseDown()
    {
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = GetMouseWorldPosition() + mousePositionOffset;

            // Verifica si la nueva posici�n est� dentro del c�rculo del collider
            Vector2 circleCenter = circleCollider.bounds.center;
            float circleRadius = circleCollider.radius;

            float distanciaPosicionCentroCirculo = Vector2.Distance(newPosition, circleCenter);
            if (distanciaPosicionCentroCirculo <= circleRadius)
            {
                // La nueva posici�n est� dentro del c�rculo, permite el movimiento
                transform.position = newPosition;
                lastValidPosition = newPosition;
            }
            else
            {
                // La nueva posici�n est� fuera del c�rculo, mant�n la �ltima posici�n v�lida
                transform.position = new Vector3(circleCenter.x, circleCenter.y,0);
            }
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
}