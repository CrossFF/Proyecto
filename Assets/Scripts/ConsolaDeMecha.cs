using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ConsolaDeMecha : MonoBehaviour, IInteractable
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
        GameObject.Find("Mecha Manager").GetComponent<MechaManager>().ShowMenu();
        // centro la atencion del jugador en el mecha
    }
    
    public void Salir()
    {
        //oculto el menu de mechas
        GameObject.Find("Mecha Manager").GetComponent<MechaManager>().HideMenu();
        // regreso la atencion del jugador al personaje
    }
}
