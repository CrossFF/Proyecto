using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class PartGameObject : MonoBehaviour
{
    [Header("Parametros")]
    [SerializeField] private PartPosition position;

    [Header("Referencias")]
    [SerializeField] private List<Renderer> _mallas;
    [SerializeField] private List<Renderer> _mallasSistemas;
    [SerializeField] private Outline _outline;
    [SerializeField] private MechaManager manager;
    [SerializeField] private CinemachineVirtualCamera cam;
    private PartMecha _part = null;


    // verifico si la parte tiene una parte equipada
    public bool Equiped()
    {
        if (_part != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // asigno el material apropiado
    public void AsignMaterial(Material material)
    {
        foreach (var item in _mallas)
        {
            item.material = material;
        }
    }

    // muestro los systemas
    public void ShowSystem(int index)
    {
        _mallasSistemas[index].enabled = true;
    }
    // oculto todos los sistemas
    public void HideSystems()
    {
        foreach (var item in _mallasSistemas)
        {
            item.enabled = false;
        }
    }

    // verifico si se puede equipar la parte
    public bool Equipable(PartMecha part)
    {
        if (part.position == position)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckSystemCapacity()
    {
        if(Equiped())
        {
            return _part.CheckSystemCapacity();
        }
        else
        {
            // informo que es una accion incorrecta
            return false;
        }
    }

    // equipo la parte
    public void SetPart(PartMecha part)
    {
        if (_part != null)
        {
            // cambio la parte equipada por la nueva
            manager.Store(_part);
            _part = part;
            manager.UsePart(part);
        }
        else
        {
            // instalo la parte
            _part = part;
            manager.UsePart(part);
        }
        manager.ShowMenu();
    }

    public void SetSystem(SystemMecha system)
    {
        _part.systems.Add(system);
        manager.UseSystem(system);
        manager.ShowPart(_part, cam);
    }

    public PartMecha GetPart()
    {
        return _part;
    }

    void OnMouseEnter()
    {
        _outline.enabled = true;
    }

    void OnMouseExit()
    {
        _outline.enabled = false;
    }

    void OnMouseDown()
    {
        if (Equiped())
            manager.ShowPart(_part, cam);
    }
}
