using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ConsolaDeCrafting : MonoBehaviour, IInteractable
{
    [SerializeField]private Outline outline;
    [SerializeField]private CinemachineVirtualCamera vCam;
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
        //sonido
        GameObject.Find("Manager de Sonido").GetComponent<SonidoManager>().PlayUISound(EventoSonoroUI.AbrirConsola);     
    }

    public void Salir()
    {
        // oculto el menu
        GameObject.Find("Menu de Crafteo").GetComponent<CraftingManager>().HideMenu();
        //sonido de desactivacion
        GameObject.Find("Manager de Sonido").GetComponent<SonidoManager>().PlayUISound(EventoSonoroUI.CerrarConsola);
    }
}
