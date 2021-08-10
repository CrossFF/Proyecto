using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartName
{
    Cabina,
    Brazo_Derecho,
    Brazo_Izquierdo,
    Pierna_Izquierda,
    Pierna_Derecha
}

public class PartMecha
{
    public PartName name;
    public float atack;
    public float defense;
    public List<SystemMecha> systems;
    public int systemCapacity;

    //Constructores
    public PartMecha(PartName name)
    {
        this.name = name;
        switch (name)
        {
            case PartName.Cabina:
                this.atack = 0f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Brazo_Derecho:
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Brazo_Izquierdo:
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Pierna_Derecha:
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Pierna_Izquierda:
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
        }
    }

    // funciones estaticas
    public static PartName GetName(BlueprintName name)
    {
        PartName thePart = PartName.Cabina;
        switch (name)
        {
            case BlueprintName.Cabina:
                thePart = PartName.Cabina;
                break;
            case BlueprintName.Brazo_Derecho:
                thePart = PartName.Brazo_Derecho;
                break;
            case BlueprintName.Brazo_Izquierdo:
                thePart = PartName.Brazo_Izquierdo;
                break;
            case BlueprintName.Pierna_Derecha:
                thePart = PartName.Pierna_Derecha;
                break;
            case BlueprintName.Pierna_Izquierda:
                thePart = PartName.Pierna_Izquierda;
                break;
        }
        return thePart;
    }
}


