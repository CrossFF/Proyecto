using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MechaManager : MonoBehaviour
{
    public Material oculto;// materiales para las partes que estan colocadas en el meca
    public List<PartGameObject> parts; // partes del mecha
    public Inventory inventory;
    public MechaUI ui;
    [SerializeField] private CameraManager _cameraManager;

    // informacion del mecha
    [Header("Mecha")]
    [SerializeField] private float _energiaTotal = 0;
    [SerializeField] private float _energiaUtilizada = 0;
    [SerializeField] private float _ataque = 0;
    [SerializeField] private float _defensa = 0;
    [SerializeField] private float _proteccionCalor = 0;
    [SerializeField] private float _proteccionFrio = 0;

    void Start()
    {
        CheckParts();
    }

    void Update()
    {
        CheckMechaValues();
    }

    public void ShowMenu()
    {
        ui.ShowMenu();
    }

    public void ShowSystemMenu()
    {
        ui.ShowSystemMenu();
    }

    public void HideMenu()
    {
        ui.HideMenu();
    }

    public void ShowPart(PartMecha part, CinemachineVirtualCamera cam)
    {
        ui.ShowPart(part);
        _cameraManager.ChangePriority(cam);
    }

    private void CheckParts()
    {
        // verifico si la parte efectivamente tiene algo equipado
        foreach (var item in parts)
        {
            if (item.Equiped())
            {
                item.AsignMaterial(null);
            }
            else
            {
                item.AsignMaterial(oculto);    
            }
            //muestro los sistemas
            item.ShowSystems();
        }
    }

    // funcion para calcular los distintos valores de funcionamiento del mecha
    private void CheckMechaValues()
    {
        // varaibles temporales para asignar los valores correspondientes
        float ataque = 0, defensa = 0, energia = 0, proteccionCalor = 0, proteccionFrio = 0, energiaUtilizada = 0;
        //recorro el array de partes
        foreach (var item in parts)
        {
            // verifico si tiene partes equipadas
            if (item.Equiped())
            {
                PartMecha partTemp = item.GetPart();
                // coloco los valores basicos que aportan dicha parte
                ataque += partTemp.atack;
                defensa += partTemp.defense;
                // verifico si las partes equipadas tienen sistemas
                foreach (var system in partTemp.systems)
                {
                    // dependiendo del sistema cambio los valores totales del mecha
                    switch (system.function)
                    {
                        case SystemFunction.Ataque:
                            ataque += CheckSystem(system);
                            break;
                        case SystemFunction.Calor:
                            proteccionCalor += CheckSystem(system);
                            break;
                        case SystemFunction.Defensa:
                            defensa += CheckSystem(system);
                            break;
                        case SystemFunction.Energia:
                            energia += system.valueEffect;
                            break;
                        case SystemFunction.Frio:
                            proteccionFrio += CheckSystem(system);
                            break;
                    }

                    // cambio el valor de eneregia actual
                    energiaUtilizada += system.energyAsigned;
                }
            }
        }

        // actualizo los valores de cada estadistica
        _ataque = ataque;
        _defensa = defensa;
        _energiaTotal = energia;
        _proteccionCalor = proteccionCalor;
        _proteccionFrio = proteccionFrio;
        _energiaUtilizada = energiaUtilizada;
    }

    // chequeo si el sistema funciona
    private float CheckSystem(SystemMecha system)
    {
        if (system.Working())
        {
            // si lo hace devuelvo el valor de su funcion
            return system.valueEffect;
        }
        else
        {
            // sino devuelvo 0
            return 0f;
        }
    }

    public void UsePart(PartMecha part)
    {
        inventory.UsePart(part);
        CheckParts();
        HideMenu();
        ShowMenu();
    }

    public void UseSystem(SystemMecha system)
    {
        inventory.UseSystem(system);
        CheckParts();
    }

    public void Store(PartMecha part)
    {
        inventory.Store(part);
    }

    // devuelve una parte segun un index
    public PartMecha GetPart(int index)
    {
        return parts[index].GetPart();
    }

    // devuelve valores del meka
    public float GetAtaque()
    {
        return _ataque;
    }

    public float GetDefensa()
    {
        return _defensa;
    }

    public float GetCalor()
    {
        return _proteccionCalor;
    }

    public float GetFrio()
    {
        return _proteccionFrio;
    }

    public float GetEnergiaTotal()
    {
        return _energiaTotal;
    }

    public float GetEnergiaAsignada()
    {
        return _energiaUtilizada;
    }
}
