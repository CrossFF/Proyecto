using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Resource> _resourcesList;
    private List<Machine> _machineList;
    private List<SystemMecha> _systemList;
    private List<PartMecha> _partsList;

    void Awake()
    {
        _resourcesList = new List<Resource>();
        _machineList = new List<Machine>();
        _partsList = new List<PartMecha>();
        _systemList = new List<SystemMecha>();
        Machine primerMaquina = new Machine(MachineName.Dron_Excavador);
        Store(primerMaquina);
    }

    // Guardar recursos
    public void Store(List<Resource> theList)
    {
        foreach (var item in theList)
        {
            _resourcesList.Add(item);
        }
        _resourcesList = Resource.SortList(_resourcesList);
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

    public float GetAmount(string thing)
    {
        float amount = 0;
        foreach (var item in _machineList)
        {
            if (item.name.ToString() == thing) amount++;
        }
        foreach (var item in _resourcesList)
        {
            if (item.type.ToString() == thing) amount += item.amount;
        }

        foreach (var item in _partsList)
        {
            if (item.name.ToString() == thing) amount++;
        }
        /*
        foreach (var item in systemList)
        {
            if (item.type.ToString() == thing) amount++;
        }*/

        return amount;
    }

    public void UseResource(string thing, float amount)
    {
        foreach (var item in _resourcesList.ToArray())
        {
            if (item.type.ToString() == thing)
            {
                item.amount -= amount;
            }
        }
    }

    public void UseMachine(Machine machine)
    {
        List<Machine> machinesTemp = _machineList;
        for (int i = 0; i < _machineList.Count; i++)
        {
            if (_machineList[i] == machine)
            {
                machinesTemp.Remove(machine);
            }
        }
        _machineList = machinesTemp;
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
        List<PartMecha> partTemp = _partsList;
        for (int i = 0; i < _partsList.Count; i++)
        {
            if (_partsList[i] == part)
            {
                partTemp.Remove(part);
            }
        }
        _partsList = partTemp;
    }

    public List<Machine> GetMachines()
    {
        return _machineList;
    }

    public List<PartMecha> GetParts()
    {
        return _partsList;
    }

}
