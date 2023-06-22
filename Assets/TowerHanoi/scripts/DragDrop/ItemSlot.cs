using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private Image imagePalo;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        //info del objeto que ha sido cogido
        if(eventData.pointerDrag!=null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            //si cuando dejamos el disco quitamos el raycast target a la imagen del palo, se puede volver a coger.
            imagePalo.raycastTarget = false;

        }
    }

}
