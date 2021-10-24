using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SystemUIFuncionamiento : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public SystemMecha system;

    public void OnPointerDown(PointerEventData eventData)
    {
        // activo el menu de gestion de energia
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // resalto el cuadrado
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // desresalto el cuadro
    }
}
