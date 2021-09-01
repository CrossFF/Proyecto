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
        NewMineUI ui = GameObject.Find("Menu de minas").GetComponent<NewMineUI>();
        Machine machine = eventData.pointerDrag.GetComponent<InventoryObject>().machine;
        //si se dropeo una maquina
        if (machine != null)
        {
            // si la maquina es instalable
            if (ui.Instalable(machine))
            {
                // instalo maquina
                ui.InstallMachine(machine, this);
            }
            else
            {
                //informo que la maquina no es instalable en esta mina
            }
        }
    }
}
