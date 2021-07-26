using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeOfNode
{
    Basic,
    Medium,
    Advanced
}
public class Node
{
    public string name;
    public List<Resource> resources;
    public List<Mine> trails;
    public bool active;
    public bool blocked;

    // Constructores
    public Node(typeOfNode type)
    {
        this.active = false;
        this.blocked = false;

        switch (type)
        {
            case typeOfNode.Basic:
                List<Resource> temp1 = new List<Resource>();
                for (int i = 0; i < 3; i++)
                {
                    temp1.Add(new Resource(typeResource.basicOre1));
                }
                this.resources = temp1;
                break;
            case typeOfNode.Medium:
                List<Resource> temp2 = new List<Resource>();
                for (int i = 0; i < 3; i++)
                {
                    temp2.Add(new Resource(typeResource.mediumOre1));
                }
                this.resources = temp2;
                break;
            case typeOfNode.Advanced:
                List<Resource> temp3 = new List<Resource>();
                for (int i = 0; i < 3; i++)
                {
                    temp3.Add(new Resource(typeResource.advancedOre1));
                }
                this.resources = temp3;
                break;
            default:
                Debug.Log("No eciste este nodo");
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
            if(temp != null)
            {
                extractedResources.Add(temp);
            }
        }
        return extractedResources;
    }

    // metodos estaticos 
    public static List<Mine> GetActiveNodes(List<Mine> mines)
    {
        List<Mine> activeMines = new List<Mine>();
        foreach (var item in mines)
        {
            if (item.node.active)
            {
                activeMines.Add(item);
            }
        }
        return activeMines;
    }

    public static List<Mine> GetInactiveNodes(List<Mine> mines)
    {
        List<Mine> inactiveMines = new List<Mine>();
        foreach (var item in mines)
        {
            if (!item.node.active)
            {
                inactiveMines.Add(item);
            }
        }
        return inactiveMines;
    }
}
