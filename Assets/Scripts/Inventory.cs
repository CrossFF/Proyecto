using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<Resource> resourcesList;
    List<Machine> machineList;
    List<SystemMecha> systemList;
    List<PartMecha> partsList;

    void Awake()
    {
        resourcesList = new List<Resource>();
        machineList = new List<Machine>();
        partsList = new List<PartMecha>();
        systemList = new List<SystemMecha>();
        Machine primerMaquina = new Machine(MachineName.Dron_Excavador);
        Store(primerMaquina);
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
        machineList.Add(machine);
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
        foreach (var item in resourcesList.ToArray())
        {
            if (item.type.ToString() == thing)
            {
                item.amount -= amount;
            }
        }
    }

    public void UseMachine(Machine machine)
    {
        List<Machine> machinesTemp = machineList;
        for (int i = 0; i < machineList.Count; i++)
        {
            if (machineList[i] == machine)
            {
                machinesTemp.Remove(machine);
            }
        }
        machineList = machinesTemp;
    }

    public List<Machine> GetMachines()
    {
        return machineList;
    }

}
