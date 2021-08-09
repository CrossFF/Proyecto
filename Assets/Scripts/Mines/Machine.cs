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
                this.amountPerCycle = 10;
                this.description = "Extrae 10 de mineral";
                break;
            case MachineName.Excavadora_Avanzada:
                this.function = MachineFunction.Extraer;
                this.amountPerCycle = 20;
                this.description = "Extrae 20 de mineral";
                break;
            case MachineName.Tuneladora:
                this.function = MachineFunction.Conectar;
                this.amountPerCycle = 0;
                this.description = "Permite conectar minas";
                break;
            case MachineName.Dron_Limpiador:
                this.function = MachineFunction.Limpiar;
                this.amountPerCycle = 0;
                this.description = "Permite desbloquear una mina bloqueada";
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
    public static MachineName GetName(BlueprintName text)
    {
        MachineName name;
        switch (text)
        {
            case BlueprintName.Dron_Excavador:
                name = MachineName.Dron_Excavador;
                break;
            case BlueprintName.Excavadora:
                name = MachineName.Excavadora;
                break;
            case BlueprintName.Excavadora_Avanzada:
                name = MachineName.Excavadora_Avanzada;
                break;
            case BlueprintName.Tuneladora:
                name = MachineName.Tuneladora;
                break;
            case BlueprintName.Dron_Limpiador:
                name = MachineName.Dron_Limpiador;
                break;
            default:
                name = MachineName.Dron_Excavador;
                break;
        }
        return name;
    }
}
