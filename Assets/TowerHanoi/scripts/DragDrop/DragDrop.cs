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
    private GameObject ultimaPosicionSeleccionadaUltimoDisco = null;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        _myGameManagerHanoi = GameManagerHanoi.GetInstance();
    }


    //BEGIN DRAG
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }
    //hacer drag
    public void OnDrag(PointerEventData eventData)
    {
        //miramos si ese disco se ha movido a una posicion antes de hacero ahora
        if(ultimaPosicionSeleccionadaUltimoDisco != null)
        {
            //si es el caso, como se ha cogido el disco, ponemos esta posicion a true otra vez
            ultimaPosicionSeleccionadaUltimoDisco.GetComponent<Libre>().SetHuecoLibre(true);
        }
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        //cuando se coja el disco ponemos habilitamos todos los palos
        _myGameManagerHanoi.HabilitarPalos();

    }

    //END DRAG
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        //cuando se suelte el disco en una zona se debe saber la info de:
        //en qu� palo est� el disco
        //en que posicion del palo est� el disco
        //poner la propiedad del disco 
        //as� ya podemos poner si se quita este disco(OnDrag) el valor de libre de la pos a true
        ultimaPosicionSeleccionadaUltimoDisco = _myGameManagerHanoi.GetPosicionUltimoDiscoSeleccionado();
    }

    //se llamar� a esta funcion cuando se apriete el rat�n
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    //hacer drop
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop);
        

    }
}

