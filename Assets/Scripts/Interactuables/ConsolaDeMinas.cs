using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ConsolaDeMinas : MonoBehaviour, IInteractable
{
    [SerializeField] private Outline _outline;
    [SerializeField] private CinemachineVirtualCamera _vCam;
    [SerializeField] private Material _mFuncionando;
    [SerializeField] private Material _mError;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private AudioSource _audioAlarma;
    public void Desmarcar()
    {
        _outline.enabled = false;
    }

    public void Resaltar()
    {
        _outline.enabled = true;
    }

    public void Interact()
    {
        //cambio proridad de camaras
        GameObject.Find("Cameras Manager").GetComponent<CameraManager>().ChangePriority(_vCam);
        //activo el menu de crafteo
        GameObject.Find("Mine Manager").GetComponent<MineManager>().ShowMenu();
        // sonido off
    }

    public void Salir()
    {
        // oculto el menu
        GameObject.Find("Mine Manager").GetComponent<MineManager>().HideMenu();
        // sonido on si es necesario
    }

    public void Funcionado()
    {
        // aplico mataerial
        _renderer.material = _mFuncionando;
        // sonido off
        _audioAlarma.Stop();
    }

    public void Error()
    {
        // aplico material
        _renderer.material = _mError;
        // alarma
        _audioAlarma.Play();
    }
}
