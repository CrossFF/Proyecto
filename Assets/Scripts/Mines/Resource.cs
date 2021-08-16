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
        this.type = list[0].type;
        foreach (var item in list)
        {
            this.amount += item.amount;
            this.totalAmount += item.amount;
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
    // ordeno la lista de recursos
    public static List<Resource> SortList(List<Resource> list)
    {
        List<Resource> theList = new List<Resource>();
        // por cada tipo de recurso genero una lista
        foreach (var item in Enum.GetNames(typeof(TypeResource)))
        {
            List<Resource> listTemp = GetResourcesType(list, item);
            // si esa lista tiene almenos un elemneto comnvierto esa lista en un solo recurso
            if (listTemp.Count > 0)
            {
                Resource temp = new Resource(listTemp);
                // agrego ese recurso a la lista
                theList.Add(temp);
            }
        }
        return theList;
    }
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

    
}
