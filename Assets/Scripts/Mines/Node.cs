using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeOfNode
{
    basic,
    medium,
    advanced
}
public class Node
{
    public string name { set; get; }
    public List<Resource> resources { set; get; }
    public float _totalResources { get; set; }
    public List<Mine> trails { set; get; }
    public bool active { set; get; }
    public bool blocked { set; get; }

    // Constructores
    public Node(typeOfNode type)
    {
        this.active = false;
        this.blocked = false;

        switch (type)
        {
            case typeOfNode.basic:
                List<Resource> temp1 = new List<Resource>();
                for (int i = 0; i < 3; i++)
                {
                    temp1.Add(new Resource(typeResource.basicOre1));
                }
                this.resources = temp1;
                break;
            case typeOfNode.medium:
                List<Resource> temp2 = new List<Resource>();
                for (int i = 0; i < 3; i++)
                {
                    temp2.Add(new Resource(typeResource.mediumOre1));
                }
                this.resources = temp2;
                break;
            case typeOfNode.advanced:
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

        foreach (var item in this.resources)
        {
            this._totalResources += item.amount;
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
