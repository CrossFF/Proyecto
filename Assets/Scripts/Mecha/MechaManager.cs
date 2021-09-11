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
            }
            else
            {
                item.AsignMaterial(oculto);
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

    public void Store(PartMecha part)
    {
        inventory.Store(part);
    }


}
