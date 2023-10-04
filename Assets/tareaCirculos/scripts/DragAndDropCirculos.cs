using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropCirculos : MonoBehaviour
{
    private Vector3 mousePositionOffset;//area para coger objeto
    private Vector3 lastValidPosition; // Guarda la última posición válida del objeto
    private bool isDragging = false;
    private bool puntuacionBajada = false;

    // Para el límite de movimiento
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
            EstaDentroArea();
        }
    }


    //metodo para saber si está dentro del area cuando se hace drag
    public void EstaDentroArea()
    {
        Vector3 newPosition = GetMouseWorldPosition() + mousePositionOffset;

        // Verifica si la nueva posición está dentro del círculo del collider
        Vector2 circleCenter = circleCollider.bounds.center;
        float circleRadius = circleCollider.radius;

        float distanciaPosicionCentroCirculo = Vector2.Distance(newPosition, circleCenter);
        if (distanciaPosicionCentroCirculo <= circleRadius)
        {
            // La nueva posición está dentro del círculo, permite el movimiento
            Vector3 nuevaPosicionZCorrecta = new Vector3(newPosition.x, newPosition.y, -1.39f);
            transform.position = nuevaPosicionZCorrecta;
            lastValidPosition = nuevaPosicionZCorrecta;
        }
        else
        {
            
            // La nueva posición está fuera del círculo, mantén la última posición válida
            transform.position = new Vector3(circleCenter.x, circleCenter.y, -1.39f);
            //hacemos que solo lo llame una vez
            if(!puntuacionBajada)
            {
                
                GameManagerCirculos.GetInstanceGM().PerderPuntuacion();
                puntuacionBajada = true;

                //sonido quitar puntuacion
                AudioManagerCirculos.instance.PlaySFX("quitarPuntuacion");
            }
            
        }
    }


    //metodo para saber si está dentro del area cuando cuando no se hace drag
    public void EstaDentroAreaEspecial()
    {
        //posicion de la nave
        Vector3 newPosition = transform.position;

        // Verifica si la posicion está dentro del círculo del collider
        Vector2 circleCenter = circleCollider.bounds.center;
        float circleRadius = circleCollider.radius;

        float distanciaPosicionCentroCirculo = Vector2.Distance(newPosition, circleCenter);
        if (distanciaPosicionCentroCirculo > circleRadius)
        {
            // La nueva posición está fuera del círculo, al centro del circulo
            transform.position = new Vector3(circleCenter.x, circleCenter.y, -1.39f);
            
                GameManagerCirculos.GetInstanceGM().PerderPuntuacion();
            //sonido quitar puntuacion
            AudioManagerCirculos.instance.PlaySFX("quitarPuntuacion");

        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        puntuacionBajada = false;
    }

    private void Update()
    {
        //comprobamos todo el rato si se ha salido del area aunque no sea Drag para que ponga en la pos central
        EstaDentroAreaEspecial();
    }
}