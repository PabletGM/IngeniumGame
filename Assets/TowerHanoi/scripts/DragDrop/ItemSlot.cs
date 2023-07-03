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

            

            //y ponemos nombre del disco que esta ocupando el hueco
            huecoLibre.GetComponent<Libre>().SetNombreDiscoActual(eventData.pointerDrag.gameObject.name);

            
            //para ver si colocamos el disco en el hueco de la posicion Libre por encima debemos ver si
            //el width del discoNuevo es menor que el width del discoMasAlto
            //para esto llamamos a metodo de GameManager que compare width del discoMasAlto y el discoNuevo
            bool posibilidadDiscoEncima = _myGameManagerHanoi.ComparacionWidthDiscos(eventData.pointerDrag.gameObject, this.gameObject);

            //miramos tambien si esta vacio el palo
           bool paloVacio = _myGameManagerHanoi.PaloVacioONo(this.gameObject);

            //cambiamos la posicion del hueco libre a false para indicar que está ocupada
            huecoLibre.GetComponent<Libre>().SetHuecoLibre(false);

            //si es true se hace todo normal, o si el palo está vacio
            if (posibilidadDiscoEncima || paloVacio)
            {
                //para así poder colocar el disco sobre la posicion del hueco libre
                eventData.pointerDrag.GetComponent<RectTransform>().position = huecoLibre.GetComponent<RectTransform>().position;
                //cambiamos la posicion del hueco libre a false para indicar que está ocupada
                huecoLibre.GetComponent<Libre>().SetHuecoLibre(false);
                //pasamos info al GameManager de cual es el ultimo disco seleccionado
                _myGameManagerHanoi.SetUltimoDiscoSeleccionado(eventData.pointerDrag.gameObject);
                //enviamos esa info al GameManager del ultimo palo y posicion del disco para luego conectar con script DragAndDrop del disco seleccionado para que sepa el palo y la posición donde se ha dejado
                _myGameManagerHanoi.SetPaloYPosicionUltimoDiscoSeleccionado(this.gameObject, huecoLibre);
                //si cuando dejamos el disco quitamos el raycast target a la imagen del palo, se puede volver a coger.
                imagePalo.raycastTarget = false;
                //si cuando dejamos el disco quitasemos todos los raycast target de la imagen de los palos, se pueden volver a coger.
                _myGameManagerHanoi.DesHabilitarPalos();
            }
            else
            {
                //cambiamos la posicion del hueco libre a true para indicar que está libre
                huecoLibre.GetComponent<Libre>().SetHuecoLibre(true);
                //sino se puede poner disco encima ponemos posicion anterior en el hueco anterior
                eventData.pointerDrag.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().ultimaPos;
                //si cuando dejamos el disco quitamos el raycast target a la imagen del palo, se puede volver a coger.
                imagePalo.raycastTarget = false;
                //si cuando dejamos el disco quitasemos todos los raycast target de la imagen de los palos, se pueden volver a coger.
                _myGameManagerHanoi.DesHabilitarPalos();
            }
            

        }
    }
    private void Start()
    {
        _myGameManagerHanoi = GameManagerHanoi.GetInstance();
        
    }

}
