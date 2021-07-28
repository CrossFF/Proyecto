using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public Node node;
    public Outline outline;
    public GameObject basic, medium, advansed;

    void Start()
    {
        node.trails = new List<Mine>();
        outline.enabled = false;
        switch (node.type)
        {
            case TypeOfNode.Basic:
                basic.SetActive(true);
                medium.SetActive(false);
                advansed.SetActive(false);
                break;
            case TypeOfNode.Medium:
                basic.SetActive(false);
                medium.SetActive(true);
                advansed.SetActive(false);
                break;
            case TypeOfNode.Advanced:
                basic.SetActive(false);
                medium.SetActive(false);
                advansed.SetActive(true);
                break;
        }
    }

    void Update()
    {
        /*
        if (node.active && !node.blocked)
        {

        }
        if (node.active && node.blocked)
        {

        }
        if (!node.active)
        {

        }*/
    }

    public List<Resource> GetResources()
    {
        List<Resource> extractedResources = node.GetResources();
        return extractedResources;
    }
}
