using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    [Header("Parametros")]
    public int amountOfMines;// cantidad de minas a instanciar
    public float timeCycle;// tiempo entre ciclo de recoleccion

    [Header("Referencias")]
    public GameObject prefabMine;// prefab de la mina
    //public MineUI ui;// referencia a la UI de las minas
    public NewMineUI ui;// referencia a la UI de las minas
    public Inventory pj;// referencia al inventario del jugador
    public GameObject prefabTrail;// prefab del Line Renderer que hace de camino entre mina y mina

    // variables privadas
    private List<Mine> _mines;// refrencia a las minas instanciadas   
    private float _cronometro;// coronometro para control de ciclo   
    private bool conectingMines = false; // para saber si estoy conectando minas

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
        _mines[0].node.status = StatusNode.Active;
        // seteo el cronometro
        _cronometro = timeCycle;
    }

    void Update()
    {
        ResourceControl();
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
            List<Mine> activeMines = Node.GetTypeNodes(_mines, StatusNode.Working);
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

    private void StoreResources(List<Resource> resources)
    {
        pj.Store(resources);
    }

    public void ConectMines(Mine startMine, Mine endMine)
    {
        
        // verifico que el inicio y el final no sean el mismo
        if (startMine != endMine)
        {
            //agrego la mina al camino de la primera
            startMine.node.trails.Add(endMine);
            //activo la mina a conectar
            endMine.node.status = StatusNode.Active;
            //instancio una linea
            GameObject temp = Instantiate(prefabTrail, Vector3.zero, Quaternion.identity);
            LineRenderer line = temp.GetComponent<LineRenderer>();
            line.SetPosition(0, startMine.transform.position);
            line.SetPosition(1, endMine.transform.position);
            //termino la accion
            conectingMines = false;
            //ShowMine(endMine);
        }
    }

    public void ActivateMineUI()
    {
        ui.ShowMenu();
    }

    public void HideMineUI()
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
        List<Machine> machines = pj.GetMachines();
        return machines;
    }

    public void InstallMachine(Machine machine, Mine mine, int indexResource)
    {
        // si el nodo esta activo
        if (mine.node.status == StatusNode.Active || mine.node.status == StatusNode.Working)
        {
            // si el recurso ya tiene alguna maquina
            if (mine.node.resources[indexResource].machine == null)
            {
                // instalo la maquina en el recurso correspondiente
                mine.node.SetMachine(machine, indexResource);
                // elimino la maquina del inventario
                pj.UseMachine(machine);
                // si la mina no tenia el esatdo de trabajando le cambio el estado
                if (mine.node.status == StatusNode.Active) mine.node.status = StatusNode.Working;
            }
        }
        // refresco la UI de la mina
        ui.HideMine();
        ui.ShowMine(mine);
    }

    public bool IsConecting()
    {
        return conectingMines;
    }

    public void StartConecting()
    {
        conectingMines = true;
    }
}
