using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    public CharacterController characterController;
    public Animator animator;
    private IInteractable interactuable; // objeto interactuable
    public CinemachineVirtualCamera camPJ;// camara del personaje
    [SerializeField] private CameraManager cameraManager;// manager de las camaras

    [Header("Test Options")]
    public bool debugMode = false;

    void Start()
    {
        cameraManager.ChangePriority(camPJ);
    }

    void Update()
    {
        Interactuar();
        Movement();
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 fixedSpeed = new Vector3(x, 0f, z);
        fixedSpeed = fixedSpeed * _speed * Time.deltaTime;
        // si estoy enfocando al personaje
        if (camPJ.Priority > 0)
        {
            // muevo el personaje
            characterController.Move(fixedSpeed);
            //caminata
            if (fixedSpeed != Vector3.zero)
            {
                animator.SetFloat("speed", 1);
                // hago que mire en la direccion que va a caminar
                float fixedRotation = _rotationSpeed * Time.deltaTime;
                animator.transform.forward = Vector3.Slerp(animator.transform.forward, fixedSpeed, fixedRotation);
            }
            else
            {
                animator.SetFloat("speed", 0);
            }
        }
        else
        {
            // la atencion no esta en el personaje
            animator.SetFloat("speed", 0);
        }
    }

    private void Interactuar()
    {
        // interactuar
        if (interactuable != null)
        {
            if (Input.GetButtonDown("Interactuar") && camPJ.Priority > 0)
            {
                interactuable.Interact();
            }
            // salir de la seccion actual
            if (Input.GetButtonDown("Salir") && camPJ.Priority == 0)
            {
                SalirInteractuable();
            }
        }

        if (debugMode)
        {
            // +1000 de todos los minerales
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GetComponent<Inventory>().Cheat();
            }
        }
    }

    public void SalirInteractuable()
    {
        cameraManager.ChangePriority(camPJ);
        interactuable.Salir();
        interactuable = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        interactuable = other.GetComponent<IInteractable>();
        if (interactuable != null)
        {
            interactuable.Resaltar();
        }
    }

    void OnTriggerStay(Collider other)
    {
        // verifco si no hay algo con lo que interactuar
        if(interactuable == null)
        {
            interactuable = other.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactuable = other.GetComponent<IInteractable>();
        if (interactuable != null)
        {
            interactuable.Desmarcar();
            interactuable = null;
        }
    }

    public void ControlSpeed(bool movement)
    {
        if (movement)
        {
            _speed = 4;
            _rotationSpeed = 5;
        }
        else
        {
            _speed = 0;
            _rotationSpeed = 0;
        }
    }
}
