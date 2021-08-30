using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Interactuable : MonoBehaviour, IInteractable
{
    // managers
    private CameraManager cameraManager; //camaras
    private MineManager mineManager;// minas
    private CraftingManager craftingManager;// crafteo
    private MechaManager mechaManager;// mecha
    public CinemachineVirtualCamera cam; // camara de la seccion que muestra
    public Seccion seccion; // seccion que muestra
    public Outline outline; // para resaltar interactuable

    void Awake()
    {
        cameraManager = GameObject.Find("Cameras Manager").GetComponent<CameraManager>();
        mineManager = GameObject.Find("Mine Manager").GetComponent<MineManager>();
        mechaManager = GameObject.Find("Mecha Manager").GetComponent<MechaManager>();
        craftingManager = GameObject.Find("Menu de Crafteo").GetComponent<CraftingManager>();
    }
    
    public void Interact()
    {
        cameraManager.ChangePriority(cam);
        switch (seccion)
        {
            case Seccion.Minas:
                mineManager.ShowMenu();
                break;
            case Seccion.Crafteo:
                craftingManager.ShowMenu();
                break;
            case Seccion.Mecha:
                mechaManager.ShowMenu();
                break;
        }
    }

    public void Salir()
    {
        switch (seccion)
        {
            case Seccion.Minas:
                mineManager.HideMenu();
                break;
            case Seccion.Crafteo:
                craftingManager.HideMenu();
                break;
            case Seccion.Mecha:
                mechaManager.HideMenu();
                break;
        }
    }

    public void Resaltar()
    {
        outline.enabled = true;
    }

    public void Desmarcar()
    {
        outline.enabled = false;
    }


}
