using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public Node node;
    public Outline outline;
    public GameObject basic, medium, advansed;
    private MineManager manager;

    void Start()
    {
        manager = GameObject.Find("Mine Manager").GetComponent<MineManager>();
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
        ActivityControl();
    }

    private void ActivityControl()
    {
        switch (node.status)
        {
            case StatusNode.Active:
                outline.OutlineColor = Color.cyan;
                break;
            case StatusNode.Inactive:
                outline.OutlineColor = Color.yellow;
                break;
            case StatusNode.Blocked:
                outline.OutlineColor = Color.red;
                break;
            case StatusNode.Working:
                outline.OutlineColor = Color.green;
                break;
            case StatusNode.Empty:
                outline.OutlineColor = Color.gray;
                break;
        }
    }

    public List<Resource> GetResources()
    {
        List<Resource> extractedResources = node.GetResources();
        return extractedResources;
    }

    private void OnCollisionStay(Collision other)
    {
        manager.NewPos(this);
    }

    void OnMouseEnter()
    {
        // resalto la mina
        if (manager.ui.infoMine.alpha == 0)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }

    }
    void OnMouseExit()
    {
        // dejo de resaltar la mina
        outline.enabled = false;
    }
    void OnMouseDown()
    {
        //si el menu no se enceuntra desplegado y no estoy conectando minas
        if (manager.ui.infoMine.alpha == 0 && !manager.IsConecting())
        {
            // le pido al managger que muestre la info de la mina
            manager.ShowMine(this);
        }
        // si estoy conectando minas conecto esa mina
        if (manager.IsConecting())
        {
            manager.ConectMines(manager.ui.GetMine(), this);
        }
    }
}
