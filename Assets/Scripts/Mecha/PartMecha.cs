using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartMecha
{
    public PartName name;
    public PartTier tier;
    public PartPosition position;
    public float atack;
    public float defense;
    public List<SystemMecha> systems;
    private int systemCapacity;

    //Constructores
    public PartMecha(PartName name)
    {
        this.name = name;
        this.systems = new List<SystemMecha>();
        switch (name)
        {
            // partes basicas
            case PartName.Cabina_Basico:
                this.tier = PartTier.Basico;
                this.position = PartPosition.Cabina;
                this.atack = 0f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Brazo_Derecho_Basico:
                this.tier = PartTier.Basico;
                this.position = PartPosition.Brazo_Derecho;
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Brazo_Izquierdo_Basico:
                this.tier = PartTier.Basico;
                this.position = PartPosition.Brazo_Izquierdo;
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Pierna_Derecha_Basico:
                this.tier = PartTier.Basico;
                this.position = PartPosition.Pierna_Derecha;
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Pierna_Izquierda_Basico:
                this.tier = PartTier.Basico;
                this.position = PartPosition.Pierna_Izquierda;
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            // partes industriales
            case PartName.Cabina_Industrial:
                this.tier = PartTier.Industrial;
                this.position = PartPosition.Cabina;
                this.atack = 0f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Brazo_Derecho_Industrial:
                this.tier = PartTier.Industrial;
                this.position = PartPosition.Brazo_Derecho;
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Brazo_Izquierdo_Industrial:
                this.tier = PartTier.Industrial;
                this.position = PartPosition.Brazo_Izquierdo;
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Pierna_Izquierda_Industrial:
                this.tier = PartTier.Industrial;
                this.position = PartPosition.Pierna_Izquierda;
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Pierna_Derecha_Industrial:
                this.tier = PartTier.Industrial;
                this.position = PartPosition.Pierna_Derecha;
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            // partes militares
            case PartName.Cabina_Militar:
                this.tier = PartTier.Militar;
                this.position = PartPosition.Cabina;
                this.atack = 0f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Brazo_Derecho_Militar:
                this.tier = PartTier.Militar;
                this.position = PartPosition.Brazo_Derecho;
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Brazo_Izquierdo_Militar:
                this.tier = PartTier.Militar;
                this.position = PartPosition.Brazo_Izquierdo;
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Pierna_Izquierda_Militar:
                this.tier = PartTier.Militar;
                this.position = PartPosition.Pierna_Izquierda;
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Pierna_Derecha_Militar:
                this.tier = PartTier.Militar;
                this.position = PartPosition.Pierna_Derecha;
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
        }
    }

    public bool CheckSystemCapacity()
    {
        if (this.systems.Count < systemCapacity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // funciones estaticas
    public static PartName GetName(BlueprintName name)
    {
        PartName thePart = PartName.Cabina_Basico;
        switch (name)
        {
            case BlueprintName.Cabina_Basico:
                thePart = PartName.Cabina_Basico;
                break;
            case BlueprintName.Brazo_Derecho_Basico:
                thePart = PartName.Brazo_Derecho_Basico;
                break;
            case BlueprintName.Brazo_Izquierdo_Basico:
                thePart = PartName.Brazo_Izquierdo_Basico;
                break;
            case BlueprintName.Pierna_Derecha_Basico:
                thePart = PartName.Pierna_Derecha_Basico;
                break;
            case BlueprintName.Pierna_Izquierda_Basico:
                thePart = PartName.Pierna_Izquierda_Basico;
                break;
            case BlueprintName.Cabina_Industrial:
                thePart = PartName.Cabina_Industrial;
                break;
            case BlueprintName.Brazo_Derecho_Industrial:
                thePart = PartName.Brazo_Derecho_Industrial;
                break;
            case BlueprintName.Brazo_Izquierdo_Industrial:
                thePart = PartName.Brazo_Izquierdo_Industrial;
                break;
            case BlueprintName.Pierna_Izquierda_Industrial:
                thePart = PartName.Pierna_Izquierda_Industrial;
                break;
            case BlueprintName.Pierna_Derecha_Industrial:
                thePart = PartName.Pierna_Derecha_Industrial;
                break;
            case BlueprintName.Cabina_Militar:
                thePart = PartName.Cabina_Militar;
                break;
            case BlueprintName.Brazo_Derecho_Militar:
                thePart = PartName.Brazo_Derecho_Militar;
                break;
            case BlueprintName.Brazo_Izquierdo_Militar:
                thePart = PartName.Brazo_Izquierdo_Militar;
                break;
            case BlueprintName.Pierna_Izquierda_Militar:
                thePart = PartName.Pierna_Izquierda_Militar;
                break;
            case BlueprintName.Pierna_Derecha_Militar:
                thePart = PartName.Pierna_Derecha_Militar;
                break;
        }
        return thePart;
    }
}


