using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SolucionPocoEleganteParaUnBug : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool interactuable = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        interactuable = GetComponent<CanvasGroup>().alpha == 1 ? true : false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        interactuable = false;
    }
}
