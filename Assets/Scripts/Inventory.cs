using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<Resource> resourcesList;
    List<Machine> machineList;

    void Start()
    {
        resourcesList = new List<Resource>();
    }

    public void StoreResources(List<Resource> theList)
    {
        if (resourcesList.Count <= 0)
        {
            resourcesList = Resource.SortList(theList);
        }
        else
        {
            resourcesList = Resource.SortList(resourcesList, theList);
        }

        foreach (var item in resourcesList)
        {
            Debug.Log(item.name + "/" + item.amount);
        }
    }
}
