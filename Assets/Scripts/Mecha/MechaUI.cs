using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechaUI : MonoBehaviour
{
    public CanvasGroup allUI,
    inventarioParte,
    informacionParte;
    public MechaManager manager;
    public GameObject prefabPartInventory;
    public Transform parentInventory;
    private List<InventoryPart> _partsInventory;

    void Awake()
    {
        _partsInventory = new List<InventoryPart>();
        ClearInventory();
        HideMenu();
    }

    private void SetVisibilidad(CanvasGroup canvasGroup, bool estado)
    {
        canvasGroup.alpha = estado ? 1f : 0f;
        canvasGroup.interactable = estado;
        canvasGroup.blocksRaycasts = estado;
    }

    public void ShowMenu()
    {
        SetVisibilidad(allUI, true);
        SetVisibilidad(inventarioParte, true);
        SetVisibilidad(informacionParte, false);
        ClearInventory();
        InstantiateInventory();
    }

    public void HideMenu()
    {
        SetVisibilidad(allUI, false);
        SetVisibilidad(inventarioParte, false);
        SetVisibilidad(informacionParte, false);
    }

    private void ClearInventory()
    {
        foreach (var item in _partsInventory)
        {
            Destroy(item.gameObject);
        }
        _partsInventory.Clear();
    }

    public void InstantiateInventory()
    {
        List<PartMecha> parts = manager.inventory.GetParts();
        foreach (var item in parts)
        {
            _partsInventory.Add(Instantiate(prefabPartInventory, parentInventory).GetComponent<InventoryPart>());
        }
        //seteo la infop
        for (int i = 0; i < parts.Count; i++)
        {
            _partsInventory[i].canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            _partsInventory[i].parent = parentInventory;
            _partsInventory[i].part = parts[i];
            _partsInventory[i].nombreParte.text = parts[i].name.ToString();
            var sprite = Resources.Load<Sprite>("La direccion");
            _partsInventory[i].imagenParte.sprite = sprite;
        }
    }

    public void ShowPart(PartMecha part)
    {
        SetVisibilidad(inventarioParte, false);
        SetVisibilidad(informacionParte, true);
    }
}
