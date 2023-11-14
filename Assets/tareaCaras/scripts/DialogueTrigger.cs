using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueTrigger : MonoBehaviour
{
    RaycastHit HitInfo;
    public GameObject dialogue;


    private void Update()
    {
        // Selecciona la capa que contiene tus sprites 2D
        int layerMask = LayerMask.GetMask("objetoDialogue");

        // Raycast desde la posici�n de la c�mara en la direcci�n hacia adelante
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position, Camera.main.transform.forward, 10, layerMask);

        // Verifica si se ha golpeado un objeto
        if (hit.collider != null)
        {
            // Verifica si se hizo clic derecho
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Hey");
                // Inicia el di�logo
                dialogue.SetActive(true);
            }
        }
    }
}
