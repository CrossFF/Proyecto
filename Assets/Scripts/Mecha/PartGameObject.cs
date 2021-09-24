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
    [SerializeField] private Material[] _texturas;
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
        // si me pasan un material lo asigno
        if (material != null)
        {
            foreach (var item in _mallas)
            {
                item.material = material;
            }
        }
        else
        {
            // sino asigno una textura dependiendo de la parte que este equipada
            if (Equiped())
            {
                switch (_part.tier)
                {
                    case PartTier.Basico:
                        material = _texturas[0];
                        break;
                    case PartTier.Industrial:
                        material = _texturas[1];
                        break;
                    case PartTier.Militar:
                        material = _texturas[2];
                        break;
                }
                AsignMaterial(material);
            }
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
        if (Equiped())
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
