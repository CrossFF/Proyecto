using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemMecha
{
    public SystemName name;
    public SystemFunction function;
    public List<PartPosition> positions;
    private float energyToWork { get; }
    public float energyAsigned = 0;
    public float valueEffect;

    public SystemMecha(SystemName name)
    {
        this.name = name;
        this.positions = new List<PartPosition>();
        switch (name)
        {
            case SystemName.Ametralladora:
                //funcion
                this.function = SystemFunction.Ataque;
                // posicion equipable
                this.positions.Add(PartPosition.Cabina);
                this.positions.Add(PartPosition.Brazo_Derecho);
                this.positions.Add(PartPosition.Brazo_Izquierdo);
                // energia y valor
                this.energyToWork = 10f;
                this.valueEffect = 10f;
                break;
            case SystemName.Cañon:
                //funcion
                this.function = SystemFunction.Ataque;
                // posicion equipable
                this.positions.Add(PartPosition.Cabina);
                this.positions.Add(PartPosition.Brazo_Derecho);
                this.positions.Add(PartPosition.Brazo_Izquierdo);
                // energia y valor
                this.energyToWork = 10f;
                this.valueEffect = 10f;
                break;
            case SystemName.Espada:
                //funcion
                this.function = SystemFunction.Ataque;
                // posicion equipable

                this.positions.Add(PartPosition.Brazo_Derecho);
                this.positions.Add(PartPosition.Brazo_Izquierdo);
                // energia y valor
                this.energyToWork = 10f;
                this.valueEffect = 10f;
                break;
            case SystemName.Proteccion_Calor:
                //funcion
                this.function = SystemFunction.Calor;
                // posicion equipable
                this.positions.Add(PartPosition.Cabina);
                this.positions.Add(PartPosition.Brazo_Derecho);
                this.positions.Add(PartPosition.Brazo_Izquierdo);
                this.positions.Add(PartPosition.Pierna_Derecha);
                this.positions.Add(PartPosition.Pierna_Izquierda);
                // energia y valor
                this.energyToWork = 10f;
                this.valueEffect = 10f;
                break;
            case SystemName.Proteccion_Frio:
                //funcion
                this.function = SystemFunction.Frio;
                // posicion equipable
                this.positions.Add(PartPosition.Cabina);
                this.positions.Add(PartPosition.Brazo_Derecho);
                this.positions.Add(PartPosition.Brazo_Izquierdo);
                this.positions.Add(PartPosition.Pierna_Derecha);
                this.positions.Add(PartPosition.Pierna_Izquierda);
                // energia y valor
                this.energyToWork = 10f;
                this.valueEffect = 10f;
                break;
            case SystemName.Bateria:
                //funcion
                this.function = SystemFunction.Energia;
                // posicion equipable
                this.positions.Add(PartPosition.Cabina);
                this.positions.Add(PartPosition.Brazo_Derecho);
                this.positions.Add(PartPosition.Brazo_Izquierdo);
                this.positions.Add(PartPosition.Pierna_Derecha);
                this.positions.Add(PartPosition.Pierna_Izquierda);
                // energia y valor
                this.energyToWork = 0f;
                this.valueEffect = 10f;
                break;
        }
    }

    // metodos propios
    public bool Working()
    {
        if (this.energyToWork <= this.energyAsigned)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // metodos estaticos
    public static SystemName GetName(BlueprintName blueprint)
    {
        SystemName name = SystemName.Ametralladora;
        switch (blueprint)
        {
            case BlueprintName.Proteccion_Calor:
                name = SystemName.Proteccion_Calor;
                break;
            case BlueprintName.Ametralladora:
                name = SystemName.Ametralladora;
                break;
            case BlueprintName.Cañon:
                name = SystemName.Cañon;
                break;
            case BlueprintName.Proteccion_Frio:
                name = SystemName.Proteccion_Frio;
                break;
            case BlueprintName.Bateria:
                name = SystemName.Bateria;
                break;
            case BlueprintName.Espada:
                name = SystemName.Espada;
                break;
        }
        return name;
    }
}



public enum SystemName
{
    Ametralladora,
    Cañon,
    Espada,
    Proteccion_Calor,
    Proteccion_Frio,
    Bateria
}

public enum SystemFunction
{
    Ataque,
    Defensa,
    Calor,
    Frio,
    Energia
}
