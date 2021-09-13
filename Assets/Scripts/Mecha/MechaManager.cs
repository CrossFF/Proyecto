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
    [SerializeField] private CameraManager cameraManager;

    void Start()
    {
        CheckParts();
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
        cameraManager.ChangePriority(cam);
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
