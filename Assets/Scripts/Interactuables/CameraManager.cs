using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    // lista de camaras
    public List<CinemachineVirtualCamera> cameras;

    // cambio de prioridad
    public void ChangePriority(CinemachineVirtualCamera camera)
    {
        foreach (var item in cameras)
        {
            if(item == camera)
            {
                item.Priority = 10;
            }
            else
            {
                item.Priority = 0;
            }
        }
    }
}

//inrterfaz de los interactuables
public interface IInteractable 
{
    void Interact();
    void Salir();
    void Resaltar();
    void Desmarcar();    
}
