using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : EventTrigger
{
    GameManagerHanoi _myGameManagerHanoi;
    [SerializeField]
    private Image imagePalo;
    public override void OnDrop(PointerEventData eventData)
    {
       
        if(eventData.pointerDrag.gameObject!=null)
        {
            eventData.pointerDrag.gameObject.GetComponent<DragDrop>().ComprobarLimites(eventData);
        }
       
        //solo hacemos drop sino se han superado los limites y sino se ha puesto un disco en la posicion o area de otro
        //info del objeto que ha sido cogido
        if (eventData.pointerDrag!=null && !eventData.pointerDrag.gameObject.GetComponent<DragDrop>().GetLimitesSuperados() &&!eventData.pointerDrag.gameObject.GetComponent<DragDrop>().GetBoolDiscosEncimaOtro())
        {
            GameObject huecoLibre;
            //llamamos a metodo de GameManager que devuelva el hueco libre y su GameObject
            huecoLibre = _myGameManagerHanoi.BuscarHuecoEnPalo(this.gameObject);


            //y ponemos nombre del disco que esta ocupando el hueco en un principio
            huecoLibre.GetComponent<Libre>().SetNombreDiscoActual(eventData.pointerDrag.gameObject.name);
               
            
            

            //para ver si colocamos el disco en el hueco de la posicion Libre por encima debemos ver si
            //el width del discoNuevo es menor que el width del discoMasAlto
            //para esto llamamos a metodo de GameManager que compare width del discoMasAlto y el discoNuevo
            bool posibilidadDiscoEncima = _myGameManagerHanoi.ComparacionWidthDiscos(eventData.pointerDrag.gameObject, this.gameObject);

            //miramos tambien si esta vacio el palo
           bool paloVacio = _myGameManagerHanoi.PaloVacioONo(this.gameObject);

            //cambiamos la posicion del hueco libre a false para indicar que está ocupada
            huecoLibre.GetComponent<Libre>().SetHuecoLibre(false);


            //antes de hacer OnDrop
            //miramos si nuestra pos hueco al quue vamos a ir coincidio con algun disco
            //si coincidio eentonces 2 discos tendrán misma posicion y no podemos permitir ese movimiento
            //COMPROBACION EXTRA
            if (_myGameManagerHanoi.ComprobarSihayDiscoEnPos(huecoLibre.transform))
            {
                posibilidadDiscoEncima = false;
                //para que así vuelva a su sitio
            }


            //si es true se hace todo normal, o si el palo está vacio
            if (posibilidadDiscoEncima || paloVacio)
            {
                //debemos saber si el ultimo movimiento hecho ha caido en la misma posicion para volver a poner su nombre
                //esto es, comprobamos si el huecoLibre es igual a la ultimaPosSeleccionada por el disco
                if(huecoLibre == eventData.pointerDrag.gameObject.GetComponent<DragDrop>().ReturnPosSeleccionada())
                {
                    //ha vuelto al mismo hueco, ponemos en este hueco
                    //huecoLibre.GetComponent<Libre>().SetNombreDiscoActual(eventData.pointerDrag.gameObject.name);
                    //huecoLibre.GetComponent<Libre>().SetHuecoLibre(false);
                }

                AudioManagerHanoi.Instance.PlaySFX("colocarDisco");
                //para así poder colocar el disco sobre la posicion del hueco libre
                eventData.pointerDrag.GetComponent<RectTransform>().position = huecoLibre.GetComponent<RectTransform>().position;
                //cambiamos la posicion del hueco libre a false para indicar que está ocupada
                huecoLibre.GetComponent<Libre>().SetHuecoLibre(false);
                huecoLibre.GetComponent<Libre>().SetNombreDiscoActual(eventData.pointerDrag.gameObject.name);
                ////pasamos info al GameManager de cual es el ultimo disco seleccionado
                //_myGameManagerHanoi.SetUltimoDiscoSeleccionado(eventData.pointerDrag.gameObject);
                ////enviamos esa info al GameManager del ultimo palo y posicion del disco para luego conectar con script DragAndDrop del disco seleccionado para que sepa el palo y la posición donde se ha dejado
                //_myGameManagerHanoi.SetPaloYPosicionUltimoDiscoSeleccionado(this.gameObject, huecoLibre);
                //si cuando dejamos el disco quitamos el raycast target a la imagen del palo, se puede volver a coger.
                imagePalo.raycastTarget = false;
                //si cuando dejamos el disco quitasemos todos los raycast target de la imagen de los palos, se pueden volver a coger.
                _myGameManagerHanoi.DesHabilitarPalos();
            }
            //sino se puede poner disco
            else
            {
                
                //como no se puede hacer la acción ponemos por el UI el consejito
                _myGameManagerHanoi.Incorrect();
                //si al final no se ocupa el huecoLibre, sino el huecoAnterior
                huecoLibre.GetComponent<Libre>().SetNombreDiscoActual("");
                
                //sino se puede poner disco encima ponemos posicion anterior en el hueco anterior
                eventData.pointerDrag.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().ultimaPos;
                //cambiamos la posicion del hueco libre a true para indicar que está libre ya que volvemos al anterior
                huecoLibre.GetComponent<Libre>().SetHuecoLibre(true);
                //falta indicar que el hueco anterior al que se vuelve se debe poner a false
                GameObject huecoAnterior = eventData.pointerDrag.GetComponent<DragDrop>().ReturnPosSeleccionada();
                if(huecoAnterior != null)
                {
                    huecoAnterior.GetComponent<Libre>().SetNombreDiscoActual(eventData.pointerDrag.gameObject.name);
                    huecoAnterior.GetComponent<Libre>().SetHuecoLibre(false);
                }
               
                //pasamos info al GameManager de cual es el ultimo disco seleccionado
                //_myGameManagerHanoi.SetUltimoDiscoSeleccionado(eventData.pointerDrag.gameObject);
                //enviamos esa info al GameManager del ultimo palo y posicion del disco para luego conectar con script DragAndDrop del disco seleccionado para que sepa el palo y la posición donde se ha dejado
                //_myGameManagerHanoi.SetPaloYPosicionUltimoDiscoSeleccionado(this.gameObject, huecoAnterior);
                //si cuando dejamos el disco quitamos el raycast target a la imagen del palo, se puede volver a coger.
                imagePalo.raycastTarget = false;
                //si cuando dejamos el disco quitasemos todos los raycast target de la imagen de los palos, se pueden volver a coger.
                _myGameManagerHanoi.DesHabilitarPalos();
            }


            //si este gameObject es el palo3, pasamos metodo a GameManager con disco y huecoLibre para que modifique la combinacion ganadora
            if (this.gameObject.name == "palo3")
            {
                _myGameManagerHanoi.ActualizarCombinacionGanadora();
            }


        }
    }


   
    private void Start()
    {
        _myGameManagerHanoi = GameManagerHanoi.GetInstance();
        
    }

}
