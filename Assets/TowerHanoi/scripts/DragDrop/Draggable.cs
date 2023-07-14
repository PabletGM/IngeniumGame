using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    Vector2 difference = Vector2.zero;

    [HideInInspector]
    public bool PermitirDraggable = false;


    private void OnMouseDown()
    {
        if (PermitirDraggable)
        {
            difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
        }
           
    }

    private void OnMouseDrag()
    {
        if(PermitirDraggable)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
            Debug.Log("Drag");
        }
        
    }

    public void SetDraggable(bool set)
    {
        PermitirDraggable = set;
    }
    private void Start()
    {
        
    }

}
