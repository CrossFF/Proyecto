using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMineUI : MonoBehaviour
{
    [Header("Referencias")]
    private UIActions _uiActions;
    public MineManager manager;
    public SonidoManager sonidoManager;
    public CanvasGroup allUI,
    infoMine,
    minaBloqueada,
    minaInactiva,
    minaVacia;
    public Text conectionsText,
    dispoTuneladoras,
    dispoTuneladoras2,
    dispoDronesLimpieza,
    stausText;
    public Button derrumbeButton;// boton de eliminar derrumbes
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
                Vacia();
                break;
            case StatusNode.Inactiva:
                Inactiva();
                break;
            case StatusNode.Bloqueada:
                Bloqueada();
                break;
        }
    }

    private void Activa()
    {
        // muestro la interfaz
        _uiActions.OnOffCanvasGroup(infoMine, true);
        //seteo la info basica de la mina
        conectionsText.text = "Esta mina conecta con " + _mine.node.trails.Count + " minas";
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
    }

    private void Inactiva()
    {
        // muestro interfaz
        _uiActions.OnOffCanvasGroup(minaInactiva, true);
        // seteo la info de las tuneladoras
        float num = manager.GetAmount(BlueprintName.Tuneladora);
        dispoTuneladoras.text = num + "/1";
    }

    private void Vacia()
    {
        // mina vacia
        _uiActions.OnOffCanvasGroup(minaVacia, true);
        // seteo la info de los recursos necesarios
        float num = manager.GetAmount(BlueprintName.Tuneladora);
        dispoTuneladoras2.text = num + "/1";
    }

    private void Bloqueada()
    {
        // muestro la interfaz
        _uiActions.OnOffCanvasGroup(minaBloqueada, true);
        // muestro cuantos drones hay disponibles
        float num = manager.GetAmount(BlueprintName.Dron_Limpiador);
        dispoDronesLimpieza.text = num + "/1";
        // si la cantidad de drones no es suficiente no se puede desbloquear
        if (num > 0)
        {
            derrumbeButton.interactable = true;
        }
        else
        {
            derrumbeButton.interactable = false;
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
                _resources[i].nombreRecurso.text = new UIActions().CleanString(_mine.node.resources[i].type.ToString());
                var sprite1 = Resources.Load<Sprite>("Ores/" + _mine.node.resources[i].type);
                // imagen de fondo
                _resources[i].imageRecurso.sprite = sprite1;
                // imagen de relleno
                _resources[i].fillImage.sprite = sprite1;
                // info de la maquina
                if (_mine.node.resources[i].machine != null)
                {
                    _resources[i].nombreMaquina.text = new UIActions().CleanString(_mine.node.resources[i].machine.name.ToString());
                    var sprite2 = Resources.Load<Sprite>("Iconos/" + _mine.node.resources[i].machine.name);
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
        _uiActions.OnOffCanvasGroup(infoMine, false);
        _uiActions.OnOffCanvasGroup(minaBloqueada, false);
        _uiActions.OnOffCanvasGroup(minaInactiva, false);
        _uiActions.OnOffCanvasGroup(minaVacia, false);
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

    public void ResetearInventario()
    {
        foreach (var item in _invetory)
        {
            Destroy(item.gameObject);
        }
        _invetory.Clear();
        InstanciarInventario();
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
                var sprite = Resources.Load<Sprite>("Iconos/" + _invetory[i].machine.name);
                _invetory[i].imagenObjeto.sprite = sprite;
                _invetory[i].nombreItem.text = new UIActions().CleanString(machines[i].name.ToString());
            }
        }
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
        // verifico si hay tuneladoras para usarse
        if (manager.GetAmount(BlueprintName.Tuneladora) > 0)
        {
            if (manager.DispoConect())
            {
                // sonido de confirmacion
                StartCoroutine(TiempoDeSonido(EventoSonoroUI.Confirmacion));
                // despues de la corrutina sigue el proceso normal
                HideMine();
                manager.StartConecting(_mine);
            }
        }
        else
        {
            // sonido de error
            sonidoManager.PlayUISound(EventoSonoroUI.Error);
        }
    }

    // para que no suene un sonido en la nada
    public IEnumerator TiempoDeSonido(EventoSonoroUI evento)
    {
        sonidoManager.PlayUISound(evento);
        yield return new WaitForSeconds(0.2f);
    }

    public void DesbloquearMina()
    {
        if (manager.GetAmount(BlueprintName.Dron_Limpiador) > 0)
        {
            // sonido de confirmacion
            StartCoroutine(TiempoDeSonido(EventoSonoroUI.Confirmacion));
            // desbloqueo la mina
            manager.DesbloquearMina(_mine);
        }
        else
        {
            // sonido de error
            sonidoManager.PlayUISound(EventoSonoroUI.Error);
        }
    }

    public void NuevosRecursos()
    {
        // verifico si se puede usar una tuneladora
        if (manager.inventory.GetAmount("Tuneladora") >= 1)
        {
            manager.NuevaMina(_mine);
            // sonido de confirmacion
            StartCoroutine(TiempoDeSonido(EventoSonoroUI.Confirmacion));
        }
        else
        {
            // no se puede generar nueva mina
            // sonido de error
            sonidoManager.PlayUISound(EventoSonoroUI.Error);
        }
    }

    public Mine GetMine()
    {
        return _mine;
    }
}
