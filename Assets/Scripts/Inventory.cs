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

    public void StoreResources(List<Resource> theList)
    {
        if (resourcesList.Count == 0)
        {
            resourcesList = Resource.SortList(theList);
        }
        else
        {
            resourcesList = Resource.SortList(resourcesList, theList);
        }

        foreach (var item in resourcesList)
        {
            Debug.Log(item.type + "/" + item.amount);
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
}
