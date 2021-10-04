using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMineUI : MonoBehaviour
{
    [Header("Referencias")]
    private UIActions _uiActions;
    public MineManager manager;
    public CanvasGroup allUI,
    infoMine,
    minaBloqueada;
    public Text conectionsText,
    dispoTuneladoras,
    dispoDronesLimpieza,
    stausText;
    public Button conectionButton,// boton de crear conecciones
    derrumbeButton;// boton de eliminar derrumbes
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
        _uiActions = new UIActions();
        _resources = new List<DispoResource>();
        _invetory = new List<InventoryObject>();
        HideMenu();
    }

    void Update()
    {
        SetInfoResources();
    }

    // muestro el menu de la mas minas
    public void ShowMenu()
    {
        _uiActions.OnOffCanvasGroup(allUI, true);
        HideMine();
    }

    public void HideMenu()
    {
        _uiActions.OnOffCanvasGroup(allUI, false);
        HideMine();
    }

    // muestro la interfaz de la mina
    public void ShowMine(Mine mine)
    {
        // guardo la mina
        _mine = mine;
        // dependiendo el estado de la mina muestro distintos menus
        switch (mine.node.status)
        {
            case StatusNode.Lista_para_trabajar:
                Activa();
                break;
            case StatusNode.Activa:
                Activa();
                break;
            case StatusNode.Sin_recursos:
                break;
            case StatusNode.Inactiva:
                break;
            case StatusNode.Bloqueada:
                break;
        }

        // si la mina no esta bloqueada no se puede usar el boton de eliminar derrumbe
        if (manager.GetAmount(BlueprintName.Dron_Limpiador) > 0)
        {
            if (_mine.node.status == StatusNode.Bloqueada)
            {
                derrumbeButton.interactable = true;
            }
            else
            {
                derrumbeButton.interactable = false;
            }
        }
        else
        {
            derrumbeButton.interactable = false;
        }
    }

    private void Activa()
    {
        // muestro la interfaz
        _uiActions.OnOffCanvasGroup(infoMine,true);
        //seteo la info basica de la mina
        conectionsText.text = "Esta mina conecta con: " + _mine.node.trails.Count + " minas";
        stausText.text = _mine.node.status.ToString();
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
        if (manager.GetAmount(BlueprintName.Tuneladora) > 0)
        {
            if (_mine.node.status == StatusNode.Lista_para_trabajar || _mine.node.status == StatusNode.Activa || _mine.node.status == StatusNode.Sin_recursos)
            {
                conectionButton.interactable = true;
            }
            else
            {
                conectionButton.interactable = false;
            }
        }
        else
        {
            conectionButton.interactable = false;
        }
    }

    private void Inactiva()
    {

    }

    private void Vacia()
    {

    }

    private void Bloqueada()
    {
        // muestro la interfaz
        _uiActions.OnOffCanvasGroup(minaBloqueada, true);
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
                var sprite1 = Resources.Load<Sprite>("Ores/" + _mine.node.resources[i].type);
                // imagen de fondo
                _resources[i].imageRecurso.sprite = sprite1;
                // imagen de relleno
                _resources[i].fillImage.sprite = sprite1;
                // info de la maquina
                if (_mine.node.resources[i].machine != null)
                {
                    _resources[i].nombreMaquina.text = _mine.node.resources[i].machine.name.ToString();
                    var sprite2 = Resources.Load<Sprite>("Prototype/" + _mine.node.resources[i].machine.name);
                    _resources[i].imageMachine.color = Color.white;
                    _resources[i].imageMachine.sprite = sprite2;
                }
                else
                {
                    _resources[i].nombreMaquina.text = "Empty";
                    _resources[i].imageMachine.color = new Color32(1, 1, 1, 0);
                }
            }
        }
    }

    // oculto cualquier interfaz relacionada con las minas
    public void HideMine()
    {
        // oculto interfaz
        _uiActions.OnOffCanvasGroup(infoMine,false);
        _uiActions.OnOffCanvasGroup(minaBloqueada, false);
        // borro elemntos instanciados
        foreach (var item in _resources)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in _invetory)
        {
            Destroy(item.gameObject);
        }
        //Limpio listas
        _resources.Clear();
        _invetory.Clear();
    }

    private void InstanciarInventario()
    {
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        List<Machine> machines = manager.GetMachines(MachineFunction.Extraer);
        if (machines.Count != 0)
        {
            foreach (var item in machines)
            {
                _invetory.Add(Instantiate(prefabInventory, parentInventory).GetComponent<InventoryObject>());
            }
            // seteo la info
            for (int i = 0; i < _invetory.Count; i++)
            {
                _invetory[i].canvas = canvas;
                _invetory[i].parent = parentInventory;
                _invetory[i].machine = machines[i];
                // imagen de la maquina
                var sprite = Resources.Load<Sprite>("Prototype/" + _invetory[i].machine.name);
                _invetory[i].imagenObjeto.sprite = sprite;
                _invetory[i].nombreItem.text = machines[i].name.ToString();
            }
        }
        // seteo la info de las tuneladoras
        float num1 = manager.GetAmount(BlueprintName.Tuneladora);
        dispoTuneladoras.text = "Tuneladoras:\n" + num1;
        // seteo la info de los drones de limpeza
        float num2 = manager.GetAmount(BlueprintName.Dron_Limpiador);
        dispoDronesLimpieza.text = "Drones de Limpeza:\n" + num2;
    }

    public void InstallMachine(Machine machine, DispoResource resource)
    {
        manager.InstallMachine(machine, _mine, GetIndexResource(resource));
    }

    public bool Instalable(Machine machine)
    {
        return manager.Instalable(machine, _mine);
    }

    public int GetIndexResource(DispoResource resource)
    {
        int index = 0;
        for (int i = 0; i < _resources.Count; i++)
        {
            if (_resources[i] == resource)
            {
                index = i;
            }
        }
        return index;
    }

    public void ConectMines()
    {
        if (manager.DispoConect())
        {
            HideMine();
            manager.StartConecting(_mine);
        }
    }

    public void DesbloquearMina()
    {
        manager.DesbloquearMina(_mine);
    }

    public Mine GetMine()
    {
        return _mine;
    }
}
