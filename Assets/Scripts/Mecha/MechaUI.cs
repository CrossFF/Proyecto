using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class MechaUI : MonoBehaviour
{
    private UIActions _uiActions;
    public CanvasGroup allUI,
    inventarioParte,
    sistemas;
    public MechaManager manager;
    public CameraManager cameraManager;
    public CinemachineVirtualCamera camMecha;

    //informacionParte;
    public GameObject prefabPartInventory;
    public Transform parentInventory;
    private List<InventoryPart> _partsInventory;

    // info sistemas
    public GameObject prefabSystemInventory;
    public Transform parentInventorySystems;
    private List<InventorySystem> _systemsInventory;

    void Awake()
    {
        _uiActions = new UIActions();
        _partsInventory = new List<InventoryPart>();
        _systemsInventory = new List<InventorySystem>();
        ClearInventory();
        HideMenu();
    }

    public void ShowMenu()
    {
        _uiActions.OnOffCanvasGroup(allUI, true);
        _uiActions.OnOffCanvasGroup(inventarioParte, true);
        _uiActions.OnOffCanvasGroup(sistemas, false);
        ClearInventory();
        InstantiateInventory();
    }

    public void HideMenu()
    {
        _uiActions.OnOffCanvasGroup(allUI, false);
        _uiActions.OnOffCanvasGroup(inventarioParte, false);
        _uiActions.OnOffCanvasGroup(sistemas, false);
    }

    private void ClearInventory()
    {
        foreach (var item in _partsInventory)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in _systemsInventory)
        {
            Destroy(item.gameObject);
        }
        _partsInventory.Clear();
        _systemsInventory.Clear();
    }

    public void InstantiateInventory()
    {
        List<PartMecha> parts = manager.inventory.GetParts();
        foreach (var item in parts)
        {
            //instancio las partes en el inventario
            _partsInventory.Add(Instantiate(prefabPartInventory, parentInventory).GetComponent<InventoryPart>());
        }
        //seteo la info
        for (int i = 0; i < parts.Count; i++)
        {
            _partsInventory[i].canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            _partsInventory[i].parent = parentInventory;
            _partsInventory[i].part = parts[i];
            _partsInventory[i].nombreParte.text = new UIActions().CleanString(parts[i].name.ToString());
            var sprite = Resources.Load<Sprite>("Iconos/Mecha/" + parts[i].name);
            _partsInventory[i].imagenParte.sprite = sprite;
        }
    }

    public void ShowPart(PartMecha part)
    {
        _uiActions.OnOffCanvasGroup(inventarioParte, false);
        _uiActions.OnOffCanvasGroup(sistemas, true);
        ClearInventory();
        InstantiateSystems();
    }

    public void InstantiateSystems()
    {
        List<SystemMecha> systems = manager.inventory.GetSystems();
        foreach (var item in systems)
        {
            // instancio el sistema en el inventario
            _systemsInventory.Add(Instantiate(prefabSystemInventory, parentInventorySystems).GetComponent<InventorySystem>());
        }
        // seteo la info
        for (int i = 0; i < systems.Count; i++)
        {
            _systemsInventory[i].canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            _systemsInventory[i].parent = parentInventorySystems;
            _systemsInventory[i].system = systems[i];
            _systemsInventory[i].nombreSistema.text = new UIActions().CleanString(systems[i].name.ToString());
            var sprite = Resources.Load<Sprite>("La direccion");
            _systemsInventory[i].imagenSistema.sprite = sprite;
        }
    }

    public void ReturnPartMenu()
    {
        cameraManager.ChangePriority(camMecha);
        ShowMenu();
    }
}
