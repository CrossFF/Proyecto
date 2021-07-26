using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public Inventory inventory;
    public GameObject prefabBlueprint;
    public CanvasGroup allMenu, machineMenu, mechaMenu, systemsMenu;

    void Awake()
    {
        HideMenu();
    }

    private void HideMenu()
    {
        allMenu.alpha = 0;
        machineMenu.alpha = 0;
        mechaMenu.alpha = 0;
        systemsMenu.alpha = 0;
        allMenu.interactable = false;
        machineMenu.interactable = false;
        mechaMenu.interactable = false;
        systemsMenu.interactable = false;
    }

    public void ShowMenu()
    {
        allMenu.alpha = 1;
        allMenu.interactable = true;
    }

    public void ShowMachineMenu()
    {
        machineMenu.alpha = 1;
        mechaMenu.alpha = 0;
        systemsMenu.alpha = 0;
        machineMenu.interactable = true;
        mechaMenu.interactable = false;
        systemsMenu.interactable = false;    
    }

    public void ShowMechaMenu()
    {
        machineMenu.alpha = 0;
        mechaMenu.alpha = 1;
        systemsMenu.alpha = 0;
        machineMenu.interactable = false;
        mechaMenu.interactable = true;
        systemsMenu.interactable = false;       
    }

    public void ShowSystemsMenu()
    {
        machineMenu.alpha = 0;
        mechaMenu.alpha = 0;
        systemsMenu.alpha = 1;
        machineMenu.interactable = false;
        mechaMenu.interactable = false;
        systemsMenu.interactable = true;      
    }
}
