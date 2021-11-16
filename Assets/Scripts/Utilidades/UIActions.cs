using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIActions
{
    // da un estado a un canvas group
    public void OnOffCanvasGroup(CanvasGroup canvasGroup, bool active)
    {
        canvasGroup.alpha = active ? 1f : 0f;
        canvasGroup.interactable = active;
        canvasGroup.blocksRaycasts = active;
    }

    // transforma un texto con _ en un texto con espacios
    public string CleanString(string toClean)
    {
        string text = "";
        foreach (var item in toClean)
        {
            if(item.ToString() == "_")
            {
                text += " ";
            }
            else
            {
                text += item.ToString();
            }
        }
        return text;
    }

    // devuelve si el mouse esta sobre un elemneto de la UI
    public bool MouseInUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
