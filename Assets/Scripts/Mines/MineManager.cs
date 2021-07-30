using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    private List<Mine> _mines;
    public int amountOfMines;
    public GameObject prefabMine;
    public MineUI ui;

    //recoleccion de recursos
    public float time;
    private float cronometro;
    public Inventory pj;

    //caminos entre nodos
    public GameObject prefabTrail;

    void Start()
    {
        _mines = new List<Mine>();

        int basicMines = (50 * amountOfMines) / 100;
        int medumMines = (30 * amountOfMines) / 100;
        int advancedMines = (20 * amountOfMines) / 100;

        InstantiateMine(basicMines, TypeOfNode.Basic);
        InstantiateMine(medumMines, TypeOfNode.Medium);
        InstantiateMine(advancedMines, TypeOfNode.Advanced);

        _mines[0].node.active = true;

        // cronometro
        cronometro = time;
    }

    void Update()
    {
        ResourceControl();
    }

    private void InstantiateMine(int amount, TypeOfNode type)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(0f, 10f), 0f, Random.Range(0f, 10f));
            GameObject temp  = Instantiate(prefabMine, pos, Quaternion.identity);
            Mine tempMine = temp.GetComponent<Mine>();
            string name = type + "_" + i;
            tempMine.node = new Node(type,name);   
            _mines.Add(tempMine);
        }
    }

    private void ResourceControl()
    {
        cronometro -= Time.deltaTime;
        if (cronometro <= 0f)
        {
            //extraigo recursos de la minas activas
            List<Mine> activeMines = Node.GetActiveNodes(_mines);
            List<Resource> extractedResources = new List<Resource>();
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
            cronometro = time;
        }
    }

    private void StoreResources(List<Resource> resources)
    {
        pj.Store(resources);
    }

    public void ConectMines(Mine startMine, Mine endMine)
    {
        //agrego la mina al camino de la primera
        startMine.node.trails.Add(endMine);
        //activo la mina a conectar
        endMine.node.active = true;
        //instancio una linea
        GameObject temp = Instantiate(prefabTrail, Vector3.zero, Quaternion.identity);
        LineRenderer line = temp.GetComponent<LineRenderer>();
        line.SetPosition(0, startMine.transform.position);
        line.SetPosition(1, endMine.transform.position);
    }

    public void ActivateMineUI()
    {
        List<Mine> activeMines = Node.GetActiveNodes(_mines);
        ui.ShowMenu(activeMines);
    }

    public void HideMineUI()
    {
        ui.HideMenu();
    }

    public List<Mine> GetInactiveMines()
    {
        List<Mine> inactiveMines = Node.GetInactiveNodes(_mines);
        return inactiveMines;
    }

    public List<Mine> GetActiveMines()
    {
        List<Mine> activeMines = Node.GetActiveNodes(_mines);
        return activeMines;
    }

    public void NewPos(Mine mine)
    {
        Vector3 pos = new Vector3(Random.Range(0f, 10f), 0f, Random.Range(0f, 10f));
        mine.transform.position = pos;
    }
}
