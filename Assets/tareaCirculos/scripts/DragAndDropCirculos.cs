using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropCirculos : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler  
{
    private RectTransform rectTransform;
    private Image image;

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.color = new Color32(255, 255, 255, 170);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;

        //transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.color = new Color(255, 255, 255, 170);
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
