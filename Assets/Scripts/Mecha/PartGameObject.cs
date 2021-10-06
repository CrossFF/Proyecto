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
    [SerializeField] private List<SystemGameObject> _sistemasGameObject;
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
    public void ShowSystems()
    {
        //oculto los sistemas
        HideSystems();
        //verifico que haya sistemas equipados para mostrar
        if (Equiped())
        {
            if (_part.systems.Count != 0)
            {
                //recorro los sistemas equipables y los comparo con los equipados
                for (int i = 0; i < _sistemasGameObject.Count; i++)
                {
                    foreach (var system in _part.systems)
                    {
                        if (_sistemasGameObject[i].nombre == system.name)
                            _sistemasGameObject[i].malla.enabled = true;
                    }
                }
            }
        }
    }

    // oculto todos los sistemas
    public void HideSystems()
    {
        foreach (var item in _sistemasGameObject)
        {
            item.malla.enabled = false;
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
        return _part.CheckSystemCapacity();
    }

    public bool CheckSystemCompatibility(SystemMecha system)
    {
        return _part.CheckSystemCompatibility(system);
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
