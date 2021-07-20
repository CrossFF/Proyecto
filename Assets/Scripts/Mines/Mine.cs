using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public Node node;
    public Color active, blocked, inactive;
    public Renderer render;
    public Outline outline;
    
    void Start()
    {
        node.trails = new List<Mine>();
    }

    void Update()
    {
        if(node.active && !node.blocked)
        {
            render.material.color = active;
        }
        if(node.active && node.blocked)
        {
            render.material.color = blocked;
        }
        if(!node.active)
        {
            render.material.color = inactive;
        }
    }

    public List<Resource> GetResources()
    {
        List<Resource> extractedResources = node.GetResources();
        return extractedResources;    
    }
}
