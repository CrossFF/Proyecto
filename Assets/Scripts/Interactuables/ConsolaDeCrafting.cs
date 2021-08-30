using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ConsolaDeCrafting : MonoBehaviour, IInteractable
{
    [SerializeField]private Outline outline;
    [SerializeField]private CinemachineVirtualCamera vCam;
    [SerializeField]private Vector3 pos;
    public void Desmarcar()
    {
        outline.enabled = false;
    }

    public void Resaltar()
    {
        outline.enabled = true;
    }

    public void Interact()
    {
        //cambio proridad de camaras
        GameObject.Find("Cameras Manager").GetComponent<CameraManager>().ChangePriority(vCam);
        //activo el menu de crafteo
        GameObject.Find("Menu de Crafteo").GetComponent<CraftingManager>().ShowMenu();
        //le doy una posicion fija al jugador
        var player = GameObject.Find("Player").GetComponent<Transform>();
        player.position = pos;
    }

    public void Salir()
    {
        // oculto el menu
        GameObject.Find("Menu de Crafteo").GetComponent<CraftingManager>().HideMenu();
    }
}
