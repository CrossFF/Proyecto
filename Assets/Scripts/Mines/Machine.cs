using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine
{
    public string name { set; get; }
    public string function { set; get; }
    public int amountPerCycle { set; get; }

    //Constructores
    public Machine()
    {
        this.name = "Excavadora";
        this.function = "Extrae 10 de mineral";
        this.amountPerCycle = 10;
    }

    // metodos propios
    public Resource GetResource(Resource resource)
    {
        Resource extractedResource;
        if (resource.amount >= this.amountPerCycle)
        {
            extractedResource = new Resource(resource.name, this.amountPerCycle);
            resource.amount -= this.amountPerCycle;
        }
        else
        {
            if (resource.amount > 0f)
            {
                extractedResource = new Resource(resource.name, resource.amount);
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
