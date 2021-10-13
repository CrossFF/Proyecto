using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TypeResource
{
    Cobriun_Blando,
    Alusteno,
    Zar_Opaco,
    Bibrio,
    Terusteno,
    Zar_Puro,
    Infinium,
    Markurio,
    Zar_Refinado
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
            case TypeResource.Cobriun_Blando:
                this.amount = 500;
                break;
            case TypeResource.Alusteno:
                this.amount = 500;
                break;
            case TypeResource.Zar_Opaco:
                this.amount = 500;
                break;
            case TypeResource.Bibrio:
                this.amount = 300;
                break;
            case TypeResource.Terusteno:
                this.amount = 300;
                break;
            case TypeResource.Zar_Puro:
                this.amount = 300;
                break;
            case TypeResource.Infinium:
                this.amount = 100;
                break;
            case TypeResource.Markurio:
                this.amount = 100;
                break;
            case TypeResource.Zar_Refinado:
                this.amount = 100;
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
        TypeResource type = TypeResource.Cobriun_Blando;
        switch (name)
        {
            case "Cobriun_Blando":
                type = TypeResource.Cobriun_Blando;
                break;
            case "Alusteno":
                type = TypeResource.Alusteno;
                break;
            case "Zar_Opaco":
                type = TypeResource.Zar_Opaco;
                break;
            case "Bibrio":
                type = TypeResource.Bibrio;
                break;
            case "Terusteno":
                type = TypeResource.Terusteno;
                break;
            case "Zar_Puro":
                type = TypeResource.Zar_Puro;
                break;
            case "Infinium":
                type = TypeResource.Infinium;
                break;
            case "Markurio":
                type = TypeResource.Markurio;
                break;
            case "Zar_Refinado":
                type = TypeResource.Zar_Refinado;
                break;
        }
        return type;
    }
}
