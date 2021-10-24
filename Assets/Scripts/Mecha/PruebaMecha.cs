using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PruebaMecha : MonoBehaviour
{
    public float ataque, defensa, calor, frio;

    [Header("Referencias")]
    public CanvasGroup canvasGroup;
    public Text tAtaque, tDefensa, tCalor, tFrio;
    public MechaManager manager;
    public Animator animator;

    void Awake()
    {
        new UIActions().OnOffCanvasGroup(canvasGroup, false);
    }

    public void RealizarPrueba()
    {
        new UIActions().OnOffCanvasGroup(canvasGroup, true);
        animator.SetTrigger("activar");
        tAtaque.text = manager.GetAtaque() >= ataque ? "Exito" : "Fallo";
        tDefensa.text = manager.GetDefensa() >= defensa ? "Exito" : "Fallo";
        tCalor.text = manager.GetCalor() >= calor ? "Exito" : "Fallo";
        tFrio.text = manager.GetFrio() >= frio ? "Exito" : "Fallo";
    }

    public void Continuar()
    {
        new UIActions().OnOffCanvasGroup(canvasGroup, false);
    }
}
