using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMineUI : MonoBehaviour
{
    [Header("Referencias")]
    public MineManager manager;
    public CanvasGroup allUI;
    public CanvasGroup infoMine;
    public Text nameText;
    public Text statusText;
    public Text conectionsText;
    public Button conectionButton;
    public GameObject prefabResource; // info basica del recurso
    public Transform parentResource;
    public GameObject prefabInventory; // prefab del objeto en inventario
    public Transform parentInventory;

    // variables privadas
    private Mine _mine;
    private List<DispoResource> _resources;
    private List<InventoryObject> _invetory;

    void Awake()
    {
        _resources = new List<DispoResource>();
        _invetory = new List<InventoryObject>();
        HideMenu();
    }

    void Update()
    {
        SetInfoResources();
    }

    public void ShowMenu()
    {
        allUI.alpha = 1f;
        allUI.blocksRaycasts = true;
        allUI.interactable = true;
        HideMine();
    }

    public void HideMenu()
    {
        allUI.alpha = 0f;
        allUI.blocksRaycasts = false;
        allUI.interactable = false;
        HideMine();
    }

    public void ShowMine(Mine mine)
    {
        _mine = mine;
        infoMine.alpha = 1f;
        infoMine.interactable = true;
        infoMine.blocksRaycasts = true;
        //seteo la info basica de la mina
        nameText.text = _mine.node.name;
        string text = "Mina: " + _mine.node.status;
        statusText.text = text;
        conectionsText.text = "Esta mina conecta con: " + _mine.node.trails.Count + " minas";
        // insatancio los recursos
        foreach (var item in _mine.node.resources)
        {
            _resources.Add(Instantiate(prefabResource, parentResource).GetComponent<DispoResource>());
        }
        // instancio el inventario
        InstanciarInventario();
        //seteo la info de los recursos
        SetInfoResources();
        // limito acciones
        // si la mina no esat activa o trabajando no se puede usar el boton de conectar minas
        if (_mine.node.status == StatusNode.Inactive || _mine.node.status == StatusNode.Blocked || _mine.node.status == StatusNode.Empty)
        {
            conectionButton.interactable = false;
        }
        else
        {
            conectionButton.interactable = true;
        }
    }

    private void SetInfoResources()
    {
        if (_resources.Count != 0)
        {
            for (int i = 0; i < _resources.Count; i++)
            {
                // info del recurso
                float fillAmount = _mine.node.resources[i].amount / _mine.node.resources[i].totalAmount;
                _resources[i].fillImage.fillAmount = fillAmount;
                _resources[i].nombreRecurso.text = _mine.node.resources[i].type.ToString();
                var sprite1 = Resources.Load<Sprite>("Prototype/" + _mine.node.resources[i].type);
                _resources[i].imageRecurso.sprite = sprite1;
                // info de la maquina
                if (_mine.node.resources[i].machine != null)
                {
                    _resources[i].nombreMaquina.text = _mine.node.resources[i].machine.name.ToString();
                    var sprite2 = Resources.Load<Sprite>("Prototype/" + _mine.node.resources[i].machine.name);
                    _resources[i].imageMachine.sprite = sprite2;
                }
                else
                {
                    _resources[i].nombreMaquina.text = "Empty";
                }
            }
        }
    }

    public void HideMine()
    {
        infoMine.alpha = 0f;
        infoMine.interactable = false;
        infoMine.blocksRaycasts = false;
        foreach (var item in _resources)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in _invetory)
        {
            Destroy(item.gameObject);
        }
        _resources.Clear();
        _invetory.Clear();
    }

    private void InstanciarInventario()
    {
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        List<Machine> machines = manager.GetMachines();
        if (machines.Count != 0)
        {
            foreach (var item in machines)
            {
                if (item.function == MachineFunction.Extraer)
                {
                    _invetory.Add(Instantiate(prefabInventory, parentInventory).GetComponent<InventoryObject>());
                }
            }
            // seteo la info
            for (int i = 0; i < _invetory.Count; i++)
            {
                _invetory[i].canvas = canvas;
                _invetory[i].parent = parentInventory;
                _invetory[i].machine = machines[i];
                _invetory[i].nombreItem.text = machines[i].name.ToString();
            }
        }
    }

    public void InstallMachine(Machine machine, DispoResource resource)
    {
        int index = 0;
        for (int i = 0; i < _resources.Count; i++)
        {
            if (_resources[i] == resource)
            {
                index = i;
            }
        }
        manager.InstallMachine(machine, _mine, index);
    }

    public void ConectMines()
    {
        HideMine();
        manager.StartConecting();
    }

    public Mine GetMine()
    {
        return _mine;
    }
}
