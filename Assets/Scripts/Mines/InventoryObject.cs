using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler,IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    //parametros
    public Machine machine;
    // referencias
    public CanvasGroup canvasGroup;
    public Text nombreItem, existenciasItem;
    public Image imagenObjeto;
    public RectTransform rectTransform;
    public Transform parent;
    public Canvas canvas;


    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        transform.parent = canvas.transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        transform.parent = parent;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.8f;
    }
}
