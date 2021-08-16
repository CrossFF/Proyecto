using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TypeResource
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
    public TypeResource type;
    public float amount;
    public float totalAmount;
    public Machine machine;

    //Constructores
    public Resource(TypeResource type, float amount)
    {
        this.type = type;
        this.amount = amount;
        this.totalAmount = this.amount;
    }
    public Resource(TypeResource type)
    {
        this.type = type;
        //this.amount = 1000;
        //this.totalAmount = this.amount;

        switch (type)
        {
            case TypeResource.basicOre1:
                this.amount = 1000;
                this.totalAmount = this.amount;
                break;
            case TypeResource.mediumOre1:
                this.amount = 1000;
                this.totalAmount = this.amount;
                break;
            case TypeResource.advancedOre1:
                this.amount = 1000;
                this.totalAmount = this.amount;
                break;
            default:
                Debug.Log("No existe este recurso");
                break;
        }
    }
    public Resource(List<Resource> list)
    {
        Resource resource = new Resource(list[0].type, 0);
        foreach (var item in list)
        {
            resource.amount += item.amount;
            resource.totalAmount += item.amount;
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
    // devuelvo una lista del recurso pedido
    public static List<Resource> GetResourcesType(List<Resource> list, string type)
    {
        List<Resource> theList = new List<Resource>();
        foreach (var item in list)
        {
            if (item.type.ToString() == type) theList.Add(item);
        }
        return theList;
    }

    public static List<Resource> SortList(List<Resource> list)
    {
        List<Resource> theList = new List<Resource>();
        foreach (var item in Enum.GetNames(typeof(TypeResource)))
        {
            List<Resource> listTemp = GetResourcesType(list, item);
            if (listTemp.Count > 0)
            {
                Resource temp = new Resource(listTemp);
                theList.Add(temp);
            }
        }
        return theList;
    }
}
