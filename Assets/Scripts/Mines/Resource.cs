using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeResource
{
    basicOre1,
    basicOre2,
    basicOre3,
    mediumOre1,
    mediumOre2,
    mediumOre3,
    advancedOre1,
    advancedOre2,
    advancedOre3
}
public class Resource
{
    public typeResource type;
    public float amount;
    public float totalAmount;
    public Machine machine;

    //Constructores
    public Resource(typeResource type, float amount)
    {
        this.type = type;
        this.amount = amount;
        this.totalAmount = this.amount;
    }
    public Resource(typeResource type)
    {
        this.type = type;
        //this.amount = 1000;
        //this.totalAmount = this.amount;

        switch (type)
        {
            case typeResource.basicOre1:
                this.amount = 1000;
                this.totalAmount = this.amount;
                break;
            case typeResource.mediumOre1:
                this.amount = 1000;
                this.totalAmount = this.amount;
                break;
            case typeResource.advancedOre1:
                this.amount = 1000;
                this.totalAmount = this.amount;
                break;
            default:
                Debug.Log("No existe este recurso");
                break;
        }
    }

    // metodos propios
    public Resource GetResource()
    {
        if (this.machine != null)
        {
            return this.machine.GetResource(this);
        }
        else
        {
            return null;
        }

    }

    public void SetMachine(Machine machine)
    {
        this.machine = machine;
    }

    //metodos estaticos
    public static List<Resource> SortList(List<Resource> resourcesToSort)
    {
        List<Resource> sorted = new List<Resource>();
        foreach (var itemToSort in resourcesToSort.ToArray())
        {
            if (sorted.Count > 0)
            {
                foreach (var sortedItem in sorted.ToArray())
                {
                    if (itemToSort.type == sortedItem.type)
                    {
                        sortedItem.amount += itemToSort.amount;
                        sortedItem.totalAmount += itemToSort.amount;
                    }
                    else
                    {
                        if (sortedItem == sorted[sorted.Count - 1])
                        {
                            sorted.Add(itemToSort);
                        }
                    }
                }
            }
            else
            {
                sorted.Add(itemToSort);
            }
        }
        return sorted;
    }
}
