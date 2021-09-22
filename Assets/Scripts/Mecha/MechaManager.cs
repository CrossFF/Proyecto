using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MechaManager : MonoBehaviour
{
    public Material visto, oculto;// materiales para las partes que estan colocadas en el meca
    public List<PartGameObject> parts; // partes del mecha
    public Inventory inventory;
    public MechaUI ui;
    [SerializeField] private CameraManager _cameraManager;

    // informacion del mecha
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
                item.AsignMaterial(visto);
                // verifico si tiene sistemas equipados y los muestro
                PartMecha part = item.GetPart();
                if (part.systems.Count != 0)
                {
                    item.HideSystems();
                    for (int i = 0; i < part.systems.Count; i++)
                    {
                        item.ShowSystem(i);
                    }
                }
                else
                {
                    item.HideSystems();
                }
            }
            else
            {
                item.AsignMaterial(oculto);
                item.HideSystems();
            }
        }
    }

    // funcion para calcular los distintos valores de funcionamiento del mecha
    private void CheckMechaValues()
    {
        // varaibles temporales para asignar los valores correspondientes
        float ataque = 0, defensa = 0, energia = 0, proteccionCalor = 0, proteccionFrio = 0;
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
                }
            }
        }
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


}
