using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SystemUIFuncionamiento : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public SystemMecha system;

    public void OnPointerDown(PointerEventData eventData)
    {
        //si hay un sistema asignado
        if(system != null)
        {
            // activo el menu de gestion de energia
            GameObject.Find("Panel_Funcionamiento").GetComponent<UIFuncionamiento>().GestionarEnergia(system);  
        }
        
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
