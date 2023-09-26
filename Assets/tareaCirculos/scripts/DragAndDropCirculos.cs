using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropCirculos : MonoBehaviour
{
    //para que se pueda clickar en el objeto con un area no solo un punto
    Vector3 mousePositionoffset;

    private Vector3 GetMouseWorldPosition()
    {
        //capture mouse Position & return world point
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    //primer metodo que se llame
    private void OnMouseDown()
    {
        //capture the mouse offset
        mousePositionoffset= gameObject.transform.position - GetMouseWorldPosition();
    }

    //segundo metodo que se llame mientras se deja pulsado
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + mousePositionoffset;
    }
}