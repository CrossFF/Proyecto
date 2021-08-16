using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfNode
{
    Basic,
    Medium,
    Advanced
}

public enum StatusNode
{
    Active,
    Inactive,
    Blocked,
    Working,
    Empty
}

public class Node
{
    public TypeOfNode type;
    public string name;
    public List<Resource> resources;
    public List<Mine> trails;
    public StatusNode status;

    // Constructores
    public Node(TypeOfNode type, string name)
    {
        this.type = type;
        this.name = name;
        this.status = StatusNode.Inactive;

        switch (type)
        {
            case TypeOfNode.Basic:
                List<Resource> temp1 = new List<Resource>();
                for (int i = 0; i < 3; i++)
                {
                    temp1.Add(new Resource(TypeResource.basicOre1));
                }
                this.resources = temp1;
                break;
            case TypeOfNode.Medium:
                List<Resource> temp2 = new List<Resource>();
                for (int i = 0; i < 3; i++)
                {
                    temp2.Add(new Resource(TypeResource.mediumOre1));
                }
                this.resources = temp2;
                break;
            case TypeOfNode.Advanced:
                List<Resource> temp3 = new List<Resource>();
                for (int i = 0; i < 3; i++)
                {
                    temp3.Add(new Resource(TypeResource.advancedOre1));
                }
                this.resources = temp3;
                break;
        }
    }

    // metodos propios
    public List<Resource> GetResources()
    {
        List<Resource> extractedResources = new List<Resource>();
        foreach (var item in this.resources)
        {
            Resource temp = item.GetResource();
            if (temp != null)
            {
                extractedResources.Add(temp);
            }
        }
        return extractedResources;
    }

    public void SetMachine(Machine machine, int indexResource)
    {
        this.resources[indexResource].SetMachine(machine);
    }

    // metodos estaticos 
    public static List<Mine> GetTypeNodes(List<Mine> mines, StatusNode type)
    {
        List<Mine> theMines = new List<Mine>();
        foreach (var item in mines)
        {
            if (item.node.status == type)
            {
                theMines.Add(item);
            }
        }
        return theMines;
    }
}
