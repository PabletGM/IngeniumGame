using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    GameManagerHanoi _myGameManagerHanoi;
    [SerializeField]
    private Image imagePalo;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        //info del objeto que ha sido cogido
        if(eventData.pointerDrag!=null)
        {
            //llamamos a metodo de GameManager que devuelva el hueco libre y su GameObject
            GameObject huecoLibre = _myGameManagerHanoi.BuscarHuecoEnPalo(this.gameObject);
            //para así poder colocar el disco sobre la posicion del hueco libre
            eventData.pointerDrag.GetComponent<RectTransform>().position = huecoLibre.GetComponent<RectTransform>().position;
            //si cuando dejamos el disco quitamos el raycast target a la imagen del palo, se puede volver a coger.
            imagePalo.raycastTarget = false;

        }
    }
    private void Start()
    {
        _myGameManagerHanoi = GameManagerHanoi.GetInstance();
    }

}
