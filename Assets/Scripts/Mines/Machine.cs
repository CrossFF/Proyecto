using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MachineName
{
    Dron_Excavador,
    Excavadora,
    Excavadora_Avanzada,
    Tuneladora,
    Dron_Limpiador
}

public enum MachineFunction
{
    Extraer,
    Conectar,
    Limpiar
}

public class Machine
{
    public MachineName name;
    public MachineFunction function;
    public string description;
    public int amountPerCycle;

    //Constructores
    public Machine()
    {
        this.name = MachineName.Excavadora;
        this.description = "Extrae 10 de mineral";
        this.amountPerCycle = 10;
    }

    public Machine(MachineName name)
    {
        // nombre de la maquina
        this.name = name;
        switch (name)
        {
            case MachineName.Dron_Excavador:
                this.function = MachineFunction.Extraer;
                this.amountPerCycle = 5;
                this.description = "Extrae 5 de mineral";
                break;
            case MachineName.Excavadora:
                this.function = MachineFunction.Extraer;
                this.amountPerCycle = 110;
                this.description = "Extrae 10 de mineral";
                break;
            case MachineName.Tuneladora:
                this.function = MachineFunction.Conectar;
                this.amountPerCycle = 0;
                this.description = "Permite conectar minas";
                break;
        }
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
        return extractedResource;
    }

    // metodos estaticos
    public static MachineName GetName(string text)
    {
        MachineName name = MachineName.Dron_Excavador;
        if (MachineName.Dron_Excavador.ToString() == text) name = MachineName.Dron_Excavador;
        if (MachineName.Excavadora.ToString() == text) name = MachineName.Excavadora;
        if (MachineName.Tuneladora.ToString() == text) name = MachineName.Tuneladora;
        return name;
    }
}
