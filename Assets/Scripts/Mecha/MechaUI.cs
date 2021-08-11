using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechaUI : MonoBehaviour
{
    public CanvasGroup allUI;
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

    public void ShowMenu()
    {
        allUI.alpha = 1;
        allUI.interactable = true;
        allUI.blocksRaycasts = true;
        ClearInventory();
        InstantiateInventory();
    }

    public void HideMenu()
    {
        allUI.alpha = 0;
        allUI.interactable = false;
        allUI.blocksRaycasts = false;
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
}
