using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draggable : MonoBehaviour
{
    Vector2 difference = Vector2.zero;


    public bool PermitirDraggable = false;



    private void OnMouseDown()
    {
       
            difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position; 
    }

    

    public void OnMouseDragDisco()
    {
       
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
            Debug.Log("Drag");
       
            
    }

    public void SetDraggable(bool set)
    {
        PermitirDraggable = set;
    }

    private void Start()
    {
        
    }




}
