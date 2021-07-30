using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<Resource> resourcesList;
    List<Machine> machineList;
    List<SystemMecha> systemList;
    List<PartMecha> partsList;

    void Start()
    {
        resourcesList = new List<Resource>();
        machineList = new List<Machine>();
        partsList = new List<PartMecha>();
        systemList = new List<SystemMecha>();
    }

    // Guardar recursos
    public void Store(List<Resource> theList)
    {
        if (resourcesList.Count == 0)
        {
            resourcesList = Resource.SortList(theList);
        }
        else
        {
            resourcesList = Resource.SortList(resourcesList, theList);
        }
    }

    // guardar maquina
    public void Store(Machine machine)
    {
        Debug.Log("Maquina :" + machine.name);
        machineList.Add(machine);
        // muestro
        foreach (var item in machineList)
        {
            Debug.Log(item.name.ToString());
        }
    }

    public float GetAmount(string thing)
    {
        float amount = 0;
        foreach (var item in machineList)
        {
            if (item.name.ToString() == thing) amount++;
        }
        foreach (var item in resourcesList)
        {
            if (item.type.ToString() == thing) amount += item.amount;
        }
        /*
        foreach (var item in partsList)
        {
            if (item.type.ToString() == thing) amount++;
        }
        foreach (var item in systemList)
        {
            if (item.type.ToString() == thing) amount++;
        }
        */
        return amount;
    }

    public void UseResource(string thing, float amount)
    {
        Debug.Log("Recurso: " + amount);
        foreach (var item in resourcesList.ToArray())
        {
            if (item.type.ToString() == thing)
            {
                item.amount -= amount;
            }
        }
    }

}
