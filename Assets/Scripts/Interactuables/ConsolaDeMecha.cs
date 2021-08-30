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
        BoxCollider collider = GetComponent<BoxCollider>();
        //cambio proridad de camaras
        GameObject.Find("Cameras Manager").GetComponent<CameraManager>().ChangePriority(vCam);
        //activo el menu de crafteo
        GameObject.Find("Mecha Manager").GetComponent<MechaManager>().ShowMenu();
        // concentro la atencion del jugador en el mecha
        outline.enabled = false;
        collider.enabled = false;
    }
    
    public void Salir()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        //oculto el menu de mechas
        GameObject.Find("Mecha Manager").GetComponent<MechaManager>().HideMenu();
        // regreso la atencion del jugador al personaje
        collider.enabled = true;
    }
}
