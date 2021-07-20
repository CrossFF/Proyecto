using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BasicMineInfo : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Mine mine;
    public MineUI ui;
    public bool selected = false;
    public Text nameMine;
    public Image estadoMina;
    public Image recursosDisponibles;

    void Update()
    {
    RefreshInfo();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (transform.parent.parent.parent.name)
        {
            case "Panel de Minas Activas":
                selected = true;
                ui.SeeMine(mine);
                break;
            case "Panel de Minas conectadas":
                selected = true;
                ui.SeeMine(mine);
                break;
            case "Panel de Minas Inactivas":
                ui.ConectMines(mine);
                break;
            default:
                break;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // resalto la mina que representa
        mine.outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // desmarco el nodo
        if (!selected)
        {
            mine.outline.enabled = false;
        }
    }

    public void SetInfo(Mine theMine, MineUI theUI)
    {
        mine = theMine;
        ui = theUI;
        nameMine.text = mine.node.name;
        if (mine.node.active && !mine.node.blocked)
        {
            var sprite = Resources.Load<Sprite>("Prototype/Active");
            estadoMina.sprite = sprite;
        }
        if (!mine.node.active)
        {
            var sprite = Resources.Load<Sprite>("Prototype/Inactive");
            estadoMina.sprite = sprite;
        }
        if (mine.node.active && mine.node.blocked)
        {
            var sprite = Resources.Load<Sprite>("Prototype/Blocked");
            estadoMina.sprite = sprite;
        }

        recursosDisponibles.fillAmount = Resource.CalculateAmoutOfResource(mine);
    }

    private void RefreshInfo()
    {
        if (mine.node.active && !mine.node.blocked)
        {
            var sprite = Resources.Load<Sprite>("Prototype/Active");
            estadoMina.sprite = sprite;
        }
        if (!mine.node.active)
        {
            var sprite = Resources.Load<Sprite>("Prototype/Inactive");
            estadoMina.sprite = sprite;
        }
        if (mine.node.active && mine.node.blocked)
        {
            var sprite = Resources.Load<Sprite>("Prototype/Blocked");
            estadoMina.sprite = sprite;
        }

        recursosDisponibles.fillAmount = Resource.CalculateAmoutOfResource(mine);
    }
}
