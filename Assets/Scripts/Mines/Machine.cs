using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Name
{
    Excavadora,
    Dron,
    Tuneladora
}

public class Machine
{
    public Name name;
    public string function;
    public int amountPerCycle;

    //Constructores
    public Machine()
    {
        this.name = Name.Excavadora;
        this.function = "Extrae 10 de mineral";
        this.amountPerCycle = 10;
    }

    // metodos propios
    public Resource GetResource(Resource resource)
    {
        Resource extractedResource;
        if (resource.amount >= this.amountPerCycle)
        {
            extractedResource = new Resource(resource.type, this.amountPerCycle);
            resource.amount -= this.amountPerCycle;
        }
        else
        {
            if (resource.amount > 0f)
            {
                extractedResource = new Resource(resource.type, resource.amount);
                resource.amount -= resource.amount;
            }
            else
            {
                return null;
            }
        }
        Debug.Log(resource.amount);
        return extractedResource;
    }
}
