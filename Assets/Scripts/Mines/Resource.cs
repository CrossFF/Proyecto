using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TypeResource
{
    BasicOre1,
    BasicOre2,
    BasicOre3,
    MediumOre1,
    MediumOre2,
    MediumOre3,
    AdvancedOre1,
    AdvancedOre2,
    AdvancedOre3
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
            case TypeResource.BasicOre1:
                this.amount = 1000;
                break;
            case TypeResource.BasicOre2:
                this.amount = 1000;
                break;
            case TypeResource.BasicOre3:
                this.amount = 1000;
                break;
            case TypeResource.MediumOre1:
                this.amount = 1000;
                break;
            case TypeResource.MediumOre2:
                this.amount = 1000;
                break;
            case TypeResource.MediumOre3:
                this.amount = 1000;
                break;
            case TypeResource.AdvancedOre1:
                this.amount = 1000;
                break;
            case TypeResource.AdvancedOre2:
                this.amount = 1000;
                break;
            case TypeResource.AdvancedOre3:
                this.amount = 1000;
                break;
            default:
                Debug.Log("No existe este recurso");
                break;
        }
        this.totalAmount = this.amount;
    }
    public Resource(List<Resource> list)
    {
        this.type = list[0].type;
        foreach (var item in list)
        {
            this.amount += item.amount;
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
        foreach (var item in list.ToArray())
        {
            if (item.type.ToString() == type)
            {
                theList.Add(item);
                list.Remove(item);
            }
        }
        return theList;
    }
}
