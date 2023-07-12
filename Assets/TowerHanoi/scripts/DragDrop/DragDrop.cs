using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
    


public class DragDrop : MonoBehaviour, IPointerDownHandler,IBeginDragHandler,IEndDragHandler, IDragHandler, IDropHandler
{
    GameManagerHanoi _myGameManagerHanoi;
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    [SerializeField]
    private GameObject ultimaPosicionSeleccionadaUltimoDisco = null;

    //para saber el width de cada disco y así diferenciarlos, cada disco guarda su width
    private float widthDisco;

    //ultima posicion
    [HideInInspector]
    public Vector3 ultimaPos;

    private bool limitessuperados = false;
    private bool discoEncimaDeOtro = true;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        _myGameManagerHanoi = GameManagerHanoi.GetInstance();
        ultimaPos =transform.position;
        //registra width del disco
        widthDisco = this.gameObject.GetComponent<RectTransform>().sizeDelta.x;

    }


    //BEGIN DRAG
    public void OnBeginDrag(PointerEventData eventData)
    {
        //si existe el hueco donde está la pos del disco ya seha liberado y lo ponemos
        GameObject hueco = _myGameManagerHanoi.BuscandoHuecoConPosicion(this.gameObject.transform.position);
        if (hueco != null)
        {
            hueco.GetComponent<Libre>().SetHuecoLibre(true);
            hueco.GetComponent<Libre>().SetNombreDiscoActual("");
            ultimaPos = transform.position;
            
        }
        limitessuperados = false;
        //ponemos la ultimPos como posicion correcta si esta es una posicion de un hueco

        

        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnBeginDragManual()
    {
        //quitamos nombre de disco en anterior hueco ya que ya no está
        ultimaPosicionSeleccionadaUltimoDisco.GetComponent<Libre>().SetNombreDiscoActual("");
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    //hacer drag
    public void OnDrag(PointerEventData eventData)
    {

        Vector2 pos = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
        Debug.Log(pos);
        //miramos si ese disco se ha movido a una posicion antes de hacero ahora
        if (ultimaPosicionSeleccionadaUltimoDisco != null)
        {
            //ultimaPos = transform.position;
            //si es el caso, como se ha cogido el disco, ponemos esta posicion a true otra vez
            ultimaPosicionSeleccionadaUltimoDisco.GetComponent<Libre>().SetHuecoLibre(true);
            //quitamos nombre de disco en anterior hueco ya que ya no está
            ultimaPosicionSeleccionadaUltimoDisco.GetComponent<Libre>().SetNombreDiscoActual("");

        }

        Cursor.lockState = CursorLockMode.Confined;
        //Debug.Log("OnDrag");
        if(eventData.delta.x>= -180 && eventData.delta.x <= 230 && eventData.delta.y >= -130 && eventData.delta.y <= 45 && rectTransform.anchoredPosition.x >= -180 && rectTransform.anchoredPosition.x <= 230 && rectTransform.anchoredPosition.y >= -130 && rectTransform.anchoredPosition.y <= 45)
        {
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            
        }
        else
        {
            limitessuperados = true;
        }
        

        //cuando se coja el disco ponemos habilitamos todos los palos
        _myGameManagerHanoi.HabilitarPalos();

    }

    //on drag manual
    public void OnDragManual( GameObject palo ,GameObject huecoLibre)
    {

        //miramos si ese disco se ha movido a una posicion antes de hacero ahora
        if (ultimaPosicionSeleccionadaUltimoDisco != null)
        {
            //ultimaPos = transform.position;
            //si es el caso, como se ha cogido el disco, ponemos esta posicion a true otra vez
            ultimaPosicionSeleccionadaUltimoDisco.GetComponent<Libre>().SetHuecoLibre(true);
            _myGameManagerHanoi.SetPaloYPosicionUltimoDiscoSeleccionado(palo , huecoLibre);
            ultimaPosicionSeleccionadaUltimoDisco = _myGameManagerHanoi.GetPosicionUltimoDiscoSeleccionado();
        }
    }

    //END DRAG
    public void OnEndDrag(PointerEventData eventData)
    {
        //vemos si ha superado limites o ha puesto un disco encima de la imagen de otro
        if (limitessuperados || discoEncimaDeOtro)
        {
            //su posicion la cambia a ultima pos
            transform.position = ultimaPos;
            ////metodo que cambie la posicion ultima con el GO posicion que posea la position de ultimaPos
            //GameObject ultimoHuecoCorrecto = _myGameManagerHanoi.BuscandoHuecoConPosicion(ultimaPos);
            //_myGameManagerHanoi.SetPaloYPosicionUltimoDiscoSeleccionado(_myGameManagerHanoi.BuscandoPaloConPosicion(ultimaPos), ultimoHuecoCorrecto);
            //ultimaPosicionSeleccionadaUltimoDisco = ultimoHuecoCorrecto;

            //al superar limites algun hueco en el que hay disco se borra
            _myGameManagerHanoi.FueraLimites();
            GameObject huecoAnterior = eventData.pointerDrag.GetComponent<DragDrop>().ReturnPosSeleccionada();
            huecoAnterior.GetComponent<Libre>().SetNombreDiscoActual(eventData.pointerDrag.gameObject.name);
            huecoAnterior.GetComponent<Libre>().SetHuecoLibre(false);
            //pasamos info al GameManager de cual es el ultimo disco seleccionado
            _myGameManagerHanoi.SetUltimoDiscoSeleccionado(eventData.pointerDrag.gameObject);
            //enviamos esa info al GameManager del ultimo palo y posicion del disco para luego conectar con script DragAndDrop del disco seleccionado para que sepa el palo y la posición donde se ha dejado
            _myGameManagerHanoi.SetPaloYPosicionUltimoDiscoSeleccionado(this.gameObject, huecoAnterior);
        }
        
        //Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        //cuando se suelte el disco en una zona se debe saber la info de:
        //en qué palo está el disco
        //en que posicion del palo está el disco
        //poner la propiedad del disco 
        //así ya podemos poner si se quita este disco(OnDrag) el valor de libre de la pos a true
        ultimaPosicionSeleccionadaUltimoDisco = _myGameManagerHanoi.GetPosicionUltimoDiscoSeleccionado();
        //_myGameManagerHanoi.MismaPosicionDiscoMismoPalo();
        //en la posicion donde esté cada disco avisa al hueco que coincida con esta posicion
        //GameObject hueco = _myGameManagerHanoi.BuscandoHuecoConPosicion(this.gameObject.transform.position);
        //if(hueco!=null)
        //{
        //    hueco.GetComponent<Libre>().SetHuecoLibre(false);
        //    hueco.GetComponent<Libre>().SetNombreDiscoActual(this.gameObject.name);
        //}
       


    }

    public bool GetLimitesSuperados()
    {
        return limitessuperados;
    }

    public void SetLimitesSuperados(bool set)
    {
        limitessuperados = set;
    }

    public void OnEndDragManual()
    {
       
        //Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        //cuando se suelte el disco en una zona se debe saber la info de:
        //en qué palo está el disco
        //en que posicion del palo está el disco
        //poner la propiedad del disco 
        //así ya podemos poner si se quita este disco(OnDrag) el valor de libre de la pos a true
        ultimaPosicionSeleccionadaUltimoDisco = _myGameManagerHanoi.GetPosicionUltimoDiscoSeleccionado();


    }

    //si se detecta que choca contra un disco
    private void OnTriggerEnter2D(Collider2D other)
    {
        discoEncimaDeOtro = true;
        //deteccion disco sobre disco
    }

    //si se detecta que ya no choca contra un disco
    private void OnTriggerExit2D(Collider2D other)
    {
        discoEncimaDeOtro = false;
        //deteccion disco sobre disco
    }

    //se llamará a esta funcion cuando se apriete el ratón
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    //hacer drop
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop);
        

    }

    //metodo que devuelve la ultimaPosSeleccionada para así acceder a su script Libre y cambiarlo a false ya que si recyhaza el disco vuelve a este
    public GameObject ReturnPosSeleccionada()
    {
        return ultimaPosicionSeleccionadaUltimoDisco;
    }
}

