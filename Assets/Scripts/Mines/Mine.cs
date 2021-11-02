using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public Node node;
    public Outline outline;
    public GameObject basic, medium, advansed;
    private MineManager manager;
    public Light spot;
    public ParticleSystem particulas;

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
        // verifico si la mina esta vacia
        float num = 0;
        foreach (var item in node.resources)
        {
            num += item.amount;
        }
        if (num == 0)
        {
            node.status = StatusNode.Sin_recursos;
        }

        // activo cosas segun su estado
        switch (node.status)
        {
            case StatusNode.Lista_para_trabajar:
                outline.OutlineColor = Color.green;
                spot.enabled = true;
                spot.color = Color.white;
                particulas.Stop();
                break;
            case StatusNode.Inactiva:
                outline.OutlineColor = Color.yellow;
                spot.enabled = false;
                particulas.Stop();
                break;
            case StatusNode.Bloqueada:
                outline.OutlineColor = Color.red;
                spot.color = Color.red;
                particulas.Stop();
                break;
            case StatusNode.Activa:
                outline.OutlineColor = Color.green;
                spot.enabled = true;
                spot.color = Color.white;
                if (!particulas.isPlaying)
                    particulas.Play();
                break;
            case StatusNode.Sin_recursos:
                outline.OutlineColor = Color.gray;
                spot.enabled = true;
                particulas.Stop();
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
            // desactivo el outline
            outline.enabled = false;
        }
        // si estoy conectando minas conecto esa mina
        if (manager.IsConecting())
        {
            manager.ConectMines(manager.ui.GetMine(), this);
        }
    }
}
