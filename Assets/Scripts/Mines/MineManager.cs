using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    [Header("Parametros")]
    public int amountOfMines;// cantidad de minas a instanciar
    public float timeCycle;// tiempo entre ciclo de recoleccion
    public float timeMin, timeMax;// tiempo minimo y maximo entre bloqueo y bloqueo de mina

    [Header("Referencias")]
    public GameObject prefabMine;// prefab de la mina
    //public MineUI ui;// referencia a la UI de las minas
    public NewMineUI ui;// referencia a la UI de las minas
    public Inventory inventory;// referencia al inventario del jugador
    public GameObject prefabTrail;// prefab del Line Renderer que hace de camino entre mina y mina

    // variables privadas
    private List<Mine> _mines;// refrencia a las minas instanciadas   
    private float _cronometro;// coronometro para control de ciclo
    private float _cronometro2;// cronometro de bloqueo de minas

    // conexion de minas
    private bool conectingMines = false; // para saber si estoy conectando minas
    private LineRenderer line; // line renderer de los caminos

    // referencia a la consola
    [SerializeField] private ConsolaDeMinas _consola;

    void Start()
    {
        _mines = new List<Mine>();
        //calculo la cantidad de cada tipo de mina que va a existir
        int basicMines = (50 * amountOfMines) / 100;
        int medumMines = (30 * amountOfMines) / 100;
        int advancedMines = (20 * amountOfMines) / 100;
        // instancio las minas segun su tipo
        InstantiateMine(basicMines, TypeOfNode.Basic);
        InstantiateMine(medumMines, TypeOfNode.Medium);
        InstantiateMine(advancedMines, TypeOfNode.Advanced);
        // activo la primera mina
        _mines[0].node.status = StatusNode.Lista_para_trabajar;
        // seteo el cronometro
        _cronometro = timeCycle;
    }

    void Update()
    {
        ResourceControl();
        BloqueoDeMinas();
    }

    private void InstantiateMine(int amount, TypeOfNode type)
    {
        // por cada objeto a instanciar:
        for (int i = 0; i < amount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(0f, 10f), 0f, Random.Range(0f, 10f));
            GameObject temp = Instantiate(prefabMine, pos, Quaternion.identity);
            Mine tempMine = temp.GetComponent<Mine>();
            string name = type + "_" + i;
            tempMine.node = new Node(type, name);
            _mines.Add(tempMine);
        }
    }

    private void ResourceControl()
    {
        _cronometro -= Time.deltaTime;
        if (_cronometro <= 0f)
        {
            //extraigo recursos de la minas que estan trabajando
            List<Mine> activeMines = Node.GetTypeNodes(_mines, StatusNode.Activa);
            List<Resource> extractedResources = new List<Resource>();
            // si la cantidad de minas activas es distinta de 0
            if (activeMines.Count != 0)
            {
                foreach (var item in activeMines)
                {
                    List<Resource> temp = item.GetResources();
                    if (temp.Count != 0)
                    {
                        foreach (var itemTemp in temp)
                        {
                            extractedResources.Add(itemTemp);
                        }
                    }
                }
            }
            //guardo los recursos el inventario
            StoreResources(extractedResources);
            // reseteo cronometro
            _cronometro = timeCycle;
        }
    }

    private void BloqueoDeMinas()
    {
        _cronometro2 -= Time.deltaTime;
        if (_cronometro2 <= 0)
        {
            // selecciono una mina random que este trabajando
            List<Mine> mines = Node.GetTypeNodes(_mines, StatusNode.Activa);
            if (mines.Count != 0)
            {
                int num = Random.Range(0, mines.Count);
                mines[num].node.status = StatusNode.Bloqueada;
                // informo al jugador que hay minas bloqueadas
                _consola.Error(); // cambio el material de la consola
            }
            _cronometro2 = Random.Range(timeMin, timeMax);
        }
    }

    private void StoreResources(List<Resource> resources)
    {
        inventory.Store(resources);
    }

    public bool DispoConect()
    {
        bool dispo = false;
        foreach (var item in _mines)
        {
            if (item.node.status == StatusNode.Inactiva) dispo = true;
        }
        return dispo;
    }

    public bool IsConecting()
    {
        return conectingMines;
    }

    public void StartConecting(Mine startMine)
    {
        conectingMines = true;
        // instancio un camino donde el origen sea la mina origen y el destino sea la posicion del mouse
        line = Instantiate(prefabTrail, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        line.SetPosition(0, startMine.transform.position);
    }

    public void ConectMines(Mine startMine, Mine endMine)
    {
        // verifico que el inicio y el final no sean el mismo y que la mina final no este activa
        if (startMine != endMine && endMine.node.status == StatusNode.Inactiva)
        {
            //uso una tuneladora
            inventory.UseMachine(MachineName.Tuneladora);
            //agrego la mina al camino de la primera
            startMine.node.trails.Add(endMine);
            //activo la mina a conectar
            endMine.node.status = StatusNode.Lista_para_trabajar;
            //termino de conectar la linea antes instanciada
            line.GetComponent<FollowMouse>().enabled = false;
            line.SetPosition(1, endMine.transform.position);
            line = null;
            //termino la accion
            conectingMines = false;
        }
    }

    public void ShowMenu()
    {
        ui.ShowMenu();
    }

    public void HideMenu()
    {
        ui.HideMenu();
    }

    public void NewPos(Mine mine)
    {
        Vector3 pos = new Vector3(Random.Range(0f, 10f), 0f, Random.Range(0f, 10f));
        mine.transform.position = pos;
    }

    public void ShowMine(Mine mine)
    {
        ui.ShowMine(mine);
    }

    public List<Machine> GetMachines()
    {
        return inventory.GetMachines();
    }

    public List<Machine> GetMachines(MachineFunction function)
    {
        return inventory.GetMachines(function);
    }

    public void InstallMachine(Machine machine, Mine mine, int indexResource)
    {
        // si el nodo esta activo
        if (mine.node.status == StatusNode.Lista_para_trabajar || mine.node.status == StatusNode.Activa)
        {
            // si el recurso ya tiene alguna maquina
            if (mine.node.resources[indexResource].machine == null)
            {
                // instalo la maquina en el recurso correspondiente
                mine.node.SetMachine(machine, indexResource);
                // elimino la maquina del inventario
                inventory.UseMachine(machine);
                // si la mina no tenia el esatdo de trabajando le cambio el estado
                if (mine.node.status == StatusNode.Lista_para_trabajar) mine.node.status = StatusNode.Activa;
            }
        }
        // refresco la UI de la mina
        ui.ResetearInventario();
    }

    public bool Instalable(Machine machine, Mine mine)
    {
        return Node.Instalable(machine, mine.node);
    }

    public float GetAmount(BlueprintName name)
    {
        return inventory.GetAmount(name.ToString());
    }

    public void DesbloquearMina(Mine mine)
    {
        inventory.UseMachine(MachineName.Dron_Limpiador);
        // re activo el nodo
        mine.node.status = StatusNode.Activa;
        ui.HideMine();
        ui.ShowMine(mine);
        // verifico si hay mas minas bloqueadas
        if (Node.GetAmountOfType(_mines, StatusNode.Bloqueada) == 0)
            _consola.Funcionado();
    }

    public void NuevaMina(Mine mine)
    {
        // gasto una tuneladora
        inventory.UseMachine(MachineName.Tuneladora);
        // genero nuevos recursos para la mina
        mine.node.NewResources();
        // re activo el nodo
        mine.node.status = StatusNode.Lista_para_trabajar;
        ui.HideMine();
        ui.ShowMine(mine);
    }

    public bool SomeMenuActive()
    {
        return ui.SomeMenuActive();
    }
}
