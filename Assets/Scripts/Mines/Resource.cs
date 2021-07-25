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
    public typeResource type { set; get; }
    public int amount { set; get; }
    public int totalAmount { set; get; }
    public Machine machine { set; get; }

    //Constructores
    public Resource(typeResource type, int amount)
    {
        this.type = type;
        this.amount = amount;
        this.totalAmount = this.amount;
    }
    public Resource(typeResource type)
    {
        this.type = type;
        this.amount = 1000;
        this.totalAmount = this.amount;
        /*
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
        }*/
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

    //metodos estaticos
    public static float CalculateAmountOfResource(Resource resource)
    {
        float result = resource.amount / resource.totalAmount;
        return result;
    }

    public static float CalculateAmoutOfResource(Mine mine)
    {
        float result = 0f;
        int totalResources = 0;
        foreach (var item in mine.node.resources)
        {
            result += item.amount;
            totalResources += item.totalAmount;
        }
        result = result / totalResources;
        return result;
    }

    public static List<Resource> SortList(List<Resource> resourcesToSort)
    {
        List<Resource> sorted = new List<Resource>();
        foreach (var item in resourcesToSort)
        {
            if (sorted.Count > 0)
            {
                foreach (var item2 in sorted)
                {
                    if (item.type == item2.type)
                    {
                        item2.amount += item.amount;
                        item2.totalAmount += item.amount;
                        break;
                    }
                    else
                    {
                        if (item2 == sorted[sorted.Count - 1])
                        {
                            sorted.Add(item);
                        }
                    }
                }
            }
            else
            {
                sorted.Add(item);
            }
        }
        return sorted;
    }

    public static List<Resource> SortList(List<Resource> actualResource, List<Resource> resourcesToSort)
    {
        foreach (var item in actualResource)
        {
            foreach (var item2 in resourcesToSort)
            {
                if (item.type == item2.type)
                {
                    item.amount += item2.amount;
                    item.totalAmount += item2.amount;
                }
                else
                {
                    if (item == actualResource[actualResource.Count - 1])
                    {
                        actualResource.Add(item2);
                    }
                }
            }
        }
        return actualResource;
    }
}
