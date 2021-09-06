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

    // metodos estaticos
    public static TypeResource GetType(string name)
    {
        TypeResource type = TypeResource.BasicOre1;
        switch (name)
        {
            case "BasicOre1":
                type = TypeResource.BasicOre1;
                break;
            case "BasicOre2":
                type = TypeResource.BasicOre2;
                break;
            case "BasicOre3":
                type = TypeResource.BasicOre3;
                break;
            case "MediumOre1":
                type = TypeResource.MediumOre1;
                break;
            case "MediumOre2":
                type = TypeResource.MediumOre2;
                break;
            case "MediumOre3":
                type = TypeResource.MediumOre3;
                break;
            case "AdvancedOre1":
                type = TypeResource.AdvancedOre1;
                break;
            case "AdvancedOre2":
                type = TypeResource.AdvancedOre2;
                break;
            case "AdvancedOre3":
                type = TypeResource.AdvancedOre3;
                break;
        }
        return type;
    }
}
