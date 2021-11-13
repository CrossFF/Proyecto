using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SystemUIFuncionamiento : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public SystemMecha system;

    public void OnPointerDown(PointerEventData eventData)
    {
        //si hay un sistema asignado
        if (system != null)
        {
            // activo el menu de gestion de energia
            GameObject.Find("Panel_Funcionamiento").GetComponent<UIFuncionamiento>().GestionarEnergia(system);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (system != null)
            // resalto el cuadrado
            GetComponent<Image>().color = Color.gray;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (system != null)
            // desresalto el cuadro
            GetComponent<Image>().color = Color.white;
    }
}
