using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMineUI : MonoBehaviour
{
    [Header("Referencias")]
    public CanvasGroup allUI;
    public CanvasGroup infoMine;
    public Text nameText;
    public Text statusText;
    public Text conectionsText;

    public void ShowMenu()
    {
        allUI.alpha = 1f;
        allUI.blocksRaycasts = true;
        allUI.interactable = true;
        infoMine.alpha = 0f;
        infoMine.interactable = false;
        infoMine.blocksRaycasts = false;
    }

    public void HideMenu()
    {
        allUI.alpha = 0f;
        allUI.blocksRaycasts = false;
        allUI.interactable = false;
        infoMine.alpha = 0f;
        infoMine.interactable = false;
        infoMine.blocksRaycasts = false;
    }

    public void ShowMine(Mine mine)
    {
        infoMine.alpha = 1f;
        infoMine.interactable = true;
        infoMine.blocksRaycasts = true;
    }

    public void HideMine()
    {
        infoMine.alpha = 0f;
        infoMine.interactable = false;
        infoMine.blocksRaycasts = false;
    }
}
