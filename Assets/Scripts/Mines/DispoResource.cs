using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DispoResource : MonoBehaviour, IDropHandler
{
    public Image fillImage;
    public Text nombreRecurso;
    public Text nombreMaquina;
    public Image imageRecurso, imageMachine;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            // instalo maquina
            GameObject.Find("Menu de minas").GetComponent<NewMineUI>().InstallMachine(eventData.pointerDrag.GetComponent<InventoryObject>().machine, this);
        }
    }
}
