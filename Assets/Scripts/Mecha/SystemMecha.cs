using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemMecha
{
    public SystemName name;
    public SystemFunction function;
    public float energryToWork;
    public float valueEffect;

    public SystemMecha(SystemName name)
    {
        this.name = name;
        switch (name)
        {
            case SystemName.Ataque:
                this.function = SystemFunction.Ataque;
                this.energryToWork = 10f;
                this.valueEffect = 10f;
                break;
            case SystemName.Defensa:
                this.function = SystemFunction.Defensa;
                this.energryToWork = 10f;
                this.valueEffect = 10f;
                break;
            case SystemName.Proteccion_Calor:
                this.function = SystemFunction.Calor;
                this.energryToWork = 10f;
                this.valueEffect = 10f;
                break;
            case SystemName.Proteccion_Frio:
                this.function = SystemFunction.Frio;
                this.energryToWork = 10f;
                this.valueEffect = 10f;
                break;
            case SystemName.Bateria:
                this.function = SystemFunction.Energia;
                this.energryToWork = 10f;
                this.valueEffect = 10f;
                break;
        }
    }

    // metodos estaticos
    public static SystemName GetName(BlueprintName blueprint)
    {
        SystemName name = SystemName.Ataque;
        switch (blueprint)
        {
            case BlueprintName.Proteccion_Calor:
                name = SystemName.Proteccion_Calor;
            break;
            case BlueprintName.Ataque:
                name = SystemName.Ataque;
            break;
            case BlueprintName.Defensa:
                name = SystemName.Defensa;
            break;
            case BlueprintName.Proteccion_Frio:
                name = SystemName.Proteccion_Frio;
            break;
            case BlueprintName.Bateria:
                name = SystemName.Bateria;
            break;
        }
        return name;
    }
}



public enum SystemName
{
    Ataque,
    Defensa,
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
