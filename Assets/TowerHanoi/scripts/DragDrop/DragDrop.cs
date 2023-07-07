using System.Collections;
using System.Collections.Generic;
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
        ultimaPos = transform.position;
        //quitamos nombre de disco en anterior hueco ya que ya no está
        ultimaPosicionSeleccionadaUltimoDisco.GetComponent<Libre>().SetNombreDiscoActual("");
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }
    //hacer drag
    public void OnDrag(PointerEventData eventData)
    {
        
        //miramos si ese disco se ha movido a una posicion antes de hacero ahora
        if (ultimaPosicionSeleccionadaUltimoDisco != null)
        {
            //ultimaPos = transform.position;
            //si es el caso, como se ha cogido el disco, ponemos esta posicion a true otra vez
            ultimaPosicionSeleccionadaUltimoDisco.GetComponent<Libre>().SetHuecoLibre(true);
            
        }

        Cursor.lockState = CursorLockMode.Confined;
        //Debug.Log("OnDrag");
        if(eventData.delta.x>= -250 && eventData.delta.x <= 260 && eventData.delta.y >= -158 && eventData.delta.y <= 93 && rectTransform.anchoredPosition.x >= -250 && rectTransform.anchoredPosition.x <= 260 && rectTransform.anchoredPosition.y >= -158 && rectTransform.anchoredPosition.y <= 93)
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

    //END DRAG
    public void OnEndDrag(PointerEventData eventData)
    {
        if(limitessuperados) transform.position = ultimaPos;
        limitessuperados = false;
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

