using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public string name { set; get; }
    public List<Resource> resources { set; get; }
    public float _totalResources { get; set; }
    public List<Mine> trails { set; get; }
    public bool active { set; get; }
    public bool blocked { set; get; }

    // Constructores
    public Node(string type)
    {
        this.active = false;
        this.blocked = false;

        switch (type)
        {
            case "Basic":
                List<Resource> temp1 = new List<Resource>();
                Resource rB1 = new Resource("Basic");
                Resource rB2 = new Resource("Basic");
                Resource rB3 = new Resource("Basic");
                temp1.Add(rB1);
                temp1.Add(rB2);
                temp1.Add(rB3);
                this.resources = temp1;
                break;
            case "Medium":
                List<Resource> temp2 = new List<Resource>();
                Resource rM1 = new Resource("Medium");
                Resource rM2 = new Resource("Medium");
                Resource rM3 = new Resource("Medium");
                temp2.Add(rM1);
                temp2.Add(rM2);
                temp2.Add(rM3);
                this.resources = temp2;
                break;
            case "Advanced":
                List<Resource> temp3 = new List<Resource>();
                Resource rA1 = new Resource("Advanced");
                Resource rA2 = new Resource("Advanced");
                Resource rA3 = new Resource("Advanced");
                temp3.Add(rA1);
                temp3.Add(rA2);
                temp3.Add(rA3);
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
