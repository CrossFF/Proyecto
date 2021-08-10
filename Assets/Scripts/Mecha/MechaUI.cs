using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechaUI : MonoBehaviour
{
    public CanvasGroup allUI;
    
    void Awake()
    {
        HideMenu();
    }

    public void ShowMenu()
    {
        allUI.alpha = 1;
        allUI.interactable = true;
        allUI.blocksRaycasts = true;
    }

    public void HideMenu()
    {
        allUI.alpha = 0;
        allUI.interactable = false;
        allUI.blocksRaycasts = false;
    }
}
