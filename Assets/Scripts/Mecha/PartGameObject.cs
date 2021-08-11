using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PartGameObject : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private List<Renderer> _mallas;
    [SerializeField] private Outline _outline;
    [SerializeField] private MechaManager manager;
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

    void OnMouseEnter()
    {
        _outline.enabled = true;
    }

    void OnMouseExit()
    {
        _outline.enabled = false;
    }
}
