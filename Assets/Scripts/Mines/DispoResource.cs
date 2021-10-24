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

    //para las animaciones
    public Animator animator;

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
                animator.SetTrigger("Equipable");
                ui.InstallMachine(machine, this);
            }
            else
            {
                //informo que la maquina no es instalable en esta mina
                // animacion de no instalable
                animator.SetTrigger("No Equipable");
            }
        }
    }
}
