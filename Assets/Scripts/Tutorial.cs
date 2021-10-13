using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public string textToShow;
    public Text text;
    public CanvasGroup panelTutorial;
    public BoxCollider col;

    void OnTriggerEnter(Collider other)
    {
        new UIActions().OnOffCanvasGroup(panelTutorial, true);
        text.text = textToShow;
    }

    void OnTriggerExit(Collider other)
    {
        col.enabled = false;
    }

    public void CerrarPanel()
    {
        new UIActions().OnOffCanvasGroup(panelTutorial, false);
        text.text = "";    
    }  
}
