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

        // no dejo al personaje moverse
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.ControlSpeed(false);

        // desactivo el collider para que no apareza de nuevo
        col.enabled = false;
    }

    public void CerrarPanel()
    {
        new UIActions().OnOffCanvasGroup(panelTutorial, false);
        text.text = "";    
        // le permito moverse al personaje
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.ControlSpeed(true);
    }  
}
