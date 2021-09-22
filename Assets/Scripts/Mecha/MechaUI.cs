using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class MechaUI : MonoBehaviour
{
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
        _partsInventory = new List<InventoryPart>();
        _systemsInventory = new List<InventorySystem>();
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
        SetVisibilidad(sistemas, false);
        ClearInventory();
        InstantiateInventory();
    }

    public void HideMenu()
    {
        SetVisibilidad(allUI, false);
        SetVisibilidad(inventarioParte, false);
        SetVisibilidad(sistemas, false);
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
            _partsInventory[i].nombreParte.text = parts[i].name.ToString();
            var sprite = Resources.Load<Sprite>("La direccion");
            _partsInventory[i].imagenParte.sprite = sprite;
        }
    }

    public void ShowPart(PartMecha part)
    {
        SetVisibilidad(inventarioParte, false);
        SetVisibilidad(sistemas, true);
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
            _systemsInventory[i].nombreSistema.text = systems[i].name.ToString();
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
