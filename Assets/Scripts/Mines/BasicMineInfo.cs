using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BasicMineInfo : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Mine mine;
    public MineUI ui;
    public Text nameMine;
    public Image estadoMina;
    // resaltar mina seleccionada
    public Image fondo;
    public bool selected = false;
    public Color colorSelected, colorNoSelected;

    // para la disponibilidad de los recursos
    private List<GameObject> _dispo;
    public GameObject prefabDispoResource;
    public Transform parentDispo;

    void Update()
    {
        RefreshInfo();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       /* switch (transform.parent.parent.parent.name)
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
*/
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
        _dispo = new List<GameObject>();
        mine = theMine;
        ui = theUI;
        nameMine.text = mine.node.name;
        /*if (mine.node.active && !mine.node.blocked)
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
        }*/

        SetDispo();
    }

    private void RefreshInfo()
    {
        if(mine == ui.mineSelected)
        {
            selected = true;
        }
        else
        {
            selected = false;
        }
        
        if(selected)
        {
            fondo.color = colorSelected;
        }
        else
        {
            fondo.color = colorNoSelected;
        }

        /*if (mine.node.active && !mine.node.blocked)
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
        }*/

        SetDispo();
    }

    private void SetDispo()
    {
        List<DispoResource> dispo = new List<DispoResource>();
        if (_dispo.Count == 0)
        {
            foreach (var resource in mine.node.resources)
            {
                GameObject temp = Instantiate(prefabDispoResource, parentDispo);
                _dispo.Add(temp);
            }
        }
        foreach (var item in _dispo)
        {
            dispo.Add(item.GetComponent<DispoResource>());
        }
        for (int i = 0; i < dispo.Count; i++)
        {
            float num = mine.node.resources[i].amount / mine.node.resources[i].totalAmount;
            dispo[i].fillImage.fillAmount = num;
        }
    }
}
