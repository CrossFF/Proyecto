using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public CharacterController characterController;
    private bool interactuar = false;
    private GameObject interactuable;
    public CinemachineVirtualCamera playerCam, mineCam, mechaCam, craftingCam;
    public MineManager mineManager;
    public MechaUI mechaManager;
    public CraftingManager craftManager;

    void Start()
    {
        SetPrority("player");
    }


    void Update()
    {
        Movement();
        Interactuar();
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 fixedSpeed = new Vector3(x, 0f, z);
        fixedSpeed = fixedSpeed * speed * Time.deltaTime;
        characterController.Move(fixedSpeed);
    }

    private void Interactuar()
    {
        if (interactuar)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                switch (interactuable.name)
                {
                    case "Consola de Mecha":
                        SetPrority("mecha");
                        mechaManager.ShowMenu();
                        break;
                    case "Consola de Minas":
                        SetPrority("mine");
                        mineManager.ActivateMineUI();
                        break;
                    case "Consola de Crafting":
                        SetPrority("craft");
                        craftManager.ShowMenu();
                        break;
                    default:
                        break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPrority("player");
            mineManager.HideMineUI();
            mechaManager.HideMenu();
            craftManager.HideMenu();
        }
    }

    private void SetPrority(string priority)
    {
        switch (priority)
        {
            case "player":
                playerCam.Priority = 10;
                mineCam.Priority = 0;
                mechaCam.Priority = 0;
                craftingCam.Priority = 0;
                break;
            case "mine":
                playerCam.Priority = 0;
                mineCam.Priority = 10;
                mechaCam.Priority = 0;
                craftingCam.Priority = 0;
                break;
            case "mecha":
                playerCam.Priority = 0;
                mineCam.Priority = 0;
                mechaCam.Priority = 10;
                craftingCam.Priority = 0;
                break;
            case "craft":
                playerCam.Priority = 0;
                mineCam.Priority = 0;
                mechaCam.Priority = 0;
                craftingCam.Priority = 10;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Consola de Mecha")
        {
            interactuar = true;
            interactuable = other.gameObject;
        }
        if (other.name == "Consola de Minas")
        {
            interactuar = true;
            interactuable = other.gameObject;
        }
        if (other.name == "Consola de Crafting")
        {
            interactuar = true;
            interactuable = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Consola de Mecha")
        {
            interactuar = false;
            interactuable = null;
        }
        if (other.name == "Consola de Minas")
        {
            interactuar = false;
            interactuable = null;
        }
        if (other.name == "Consola de Crafting")
        {
            interactuar = true;
            interactuable = other.gameObject;
        }
    }
}
