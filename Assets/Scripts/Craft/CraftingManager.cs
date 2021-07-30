using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public Inventory inventory;
    public List<GameObject> prefabBlueprints;
    public CanvasGroup allMenu, machineMenu, mechaMenu, systemsMenu;
    public Transform machineContent, mechaContent, systemsContent;
    public Button machineButton, mechaButton, systemButton;
    private List<GameObject> _temps;

    void Awake()
    {
        _temps = new List<GameObject>();
        HideMenu();
    }

    public void HideMenu()
    {
        allMenu.alpha = 0;
        machineMenu.alpha = 0;
        mechaMenu.alpha = 0;
        systemsMenu.alpha = 0;
        allMenu.interactable = false;
        machineMenu.interactable = false;
        mechaMenu.interactable = false;
        systemsMenu.interactable = false;
        allMenu.blocksRaycasts = false;
        ClearList();
    }

    public void ShowMenu()
    {
        allMenu.alpha = 1;
        allMenu.interactable = true;
        allMenu.blocksRaycasts = true;
        InstantiateBlueprints();
        ShowMachineMenu();
    }

    public void ShowMachineMenu()
    {
        machineMenu.alpha = 1;
        mechaMenu.alpha = 0;
        systemsMenu.alpha = 0;
        machineMenu.interactable = true;
        mechaMenu.interactable = false;
        systemsMenu.interactable = false;
        machineButton.interactable = false;
        mechaButton.interactable = true;
        systemButton.interactable = true;
    }

    public void ShowMechaMenu()
    {
        machineMenu.alpha = 0;
        mechaMenu.alpha = 1;
        systemsMenu.alpha = 0;
        machineMenu.interactable = false;
        mechaMenu.interactable = true;
        systemsMenu.interactable = false;
        machineButton.interactable = true;
        mechaButton.interactable = false;
        systemButton.interactable = true;
    }

    public void ShowSystemsMenu()
    {
        machineMenu.alpha = 0;
        mechaMenu.alpha = 0;
        systemsMenu.alpha = 1;
        machineMenu.interactable = false;
        mechaMenu.interactable = false;
        systemsMenu.interactable = true;
        machineButton.interactable = true;
        mechaButton.interactable = true;
        systemButton.interactable = false;
    }

    private void InstantiateBlueprints()
    {
        List<GameObject> machines = new List<GameObject>();
        List<GameObject> parts = new List<GameObject>();
        List<GameObject> systems = new List<GameObject>();
        foreach (var item in prefabBlueprints)
        {
            BlueprintGameObject temp = item.GetComponent<BlueprintGameObject>();
            temp.manager = this;
            switch (temp.blueprintType)
            {
                case BlueprintType.Machine:
                    machines.Add(item);
                    break;
                case BlueprintType.Part:
                    parts.Add(item);
                    break;
                case BlueprintType.System:
                    systems.Add(item);
                    break;
            }
        }
        InstantiateBlueprints(machines, machineContent);
        InstantiateBlueprints(parts, mechaContent);
        InstantiateBlueprints(systems, systemsContent);
    }

    private void InstantiateBlueprints(List<GameObject> blueprints, Transform parent)
    {
        foreach (var item in blueprints)
        {
            _temps.Add(Instantiate(item, parent));
        }
    }

    private void ClearList()
    {
        if (_temps.Count > 0)
        {
            foreach (var item in _temps)
            {
                Destroy(item);
            }
            _temps.Clear();
        }
    }

    public void Craft(BlueprintGameObject blueprint)
    {
        //gasto los recursos necesarios para el crafteo
        for (int i = 0; i < blueprint.blueprintIngredients.Count; i++)
        {
            inventory.UseResource(blueprint.blueprintIngredients[i].ToString(), blueprint.ingredientsAmount[i]);
        }
        // creo el objeto necesario y lo guardo en el inventario
        switch (blueprint.blueprintType)
        {
            case BlueprintType.Machine:
                MachineName name = Machine.GetName(blueprint.blueprintType.ToString());
                Machine machine = new Machine(name);
                inventory.Store(machine);
                break;
            case BlueprintType.Part:

                break;
            case BlueprintType.System:

                break;
        }
    }
}
