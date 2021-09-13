using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    private Dictionary<TypeResource, float> _resourceDictionary;
    private List<Machine> _machineList;
    private List<SystemMecha> _systemList;
    private List<PartMecha> _partsList;

    void Awake()
    {
        // inicializo el diccionario
        _resourceDictionary = new Dictionary<TypeResource, float>();
        foreach (string name in Enum.GetNames(typeof(TypeResource)))
        {
            _resourceDictionary.Add(Resource.GetType(name), 0f);
        }
        _machineList = new List<Machine>();
        _partsList = new List<PartMecha>();
        _systemList = new List<SystemMecha>();
        Machine primerMaquina = new Machine(MachineName.Dron_Excavador);
        Store(primerMaquina);
    }

    // Guardar recursos
    public void Store(List<Resource> theList)
    {
        //agrego los recursos extraidos al inventario
        foreach (var item in theList)
        {
            _resourceDictionary[item.type] += item.amount;
        }
    }

    // guardar maquina
    public void Store(Machine machine)
    {
        _machineList.Add(machine);
    }

    // guardar parte
    public void Store(PartMecha part)
    {
        _partsList.Add(part);
    }

    // guardar sistema
    public void Store(SystemMecha system)
    {
        _systemList.Add(system);
    }

    public float GetAmount(string thing)
    {
        float amount = 0;
        // maquinas
        foreach (var item in _machineList)
        {
            if (item.name.ToString() == thing) amount++;
        }
        // partes
        foreach (var item in _partsList)
        {
            if (item.name.ToString() == thing) amount++;
        }
        // sistemas
        foreach (var item in _systemList)
        {
            if (item.name.ToString() == thing) amount++;
        }
        return amount;
    }

    // Consigue la cantidad de un recurso
    public float GetAmount(TypeResource thing)
    {
        float amount = 0;
        if(_resourceDictionary.ContainsKey(thing))
        {
            amount += _resourceDictionary[thing];
        }
        return amount;
    }

    public void UseResource(TypeResource type, float amount)
    {
        if(_resourceDictionary.ContainsKey(type))
        {
            _resourceDictionary[type] -= amount;
        }
    }

    public void UseMachine(Machine machine)
    {
        _machineList.Remove(machine);
    }

    public void UseMachine(MachineName machine)
    {
        Machine machineTemp = null;
        foreach (var item in _machineList)
        {
            if (item.name == machine)
            {
                machineTemp = item;
                break;
            }
        }
        UseMachine(machineTemp);
    }

    public void UsePart(PartMecha part)
    {
        _partsList.Remove(part);
    }

    public void UseSystem(SystemMecha system)
    {
        _systemList.Remove(system);
    }

    public List<Machine> GetMachines()
    {
        return _machineList;
    }

    public List<Machine> GetMachines(MachineFunction function)
    {
        List<Machine> machines = new List<Machine>();
        foreach (var item in _machineList)
        {
            if (item.function == function)
                machines.Add(item);
        }
        return machines;
    }

    public List<PartMecha> GetParts()
    {
        return _partsList;
    }

    public List<SystemMecha> GetSystems()
    {
        return _systemList;
    }

}
