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

    // verifico si se puede equipar la parte
    public bool Equippable(PartMecha part)
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
