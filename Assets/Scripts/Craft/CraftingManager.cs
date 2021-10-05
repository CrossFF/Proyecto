using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    private UIActions _uiActions;
    public Inventory inventory;
    private int _consoleLevel = 0;
    [SerializeField] private List<BlueprintsForLevel> prefabsPerLevel;
    public CanvasGroup allMenu, machineMenu, mechaMenu, systemsMenu;
    public Transform machineContent, mechaContent, systemsContent;
    public Button machineButton, mechaButton, systemButton;
    private List<GameObject> _blueprintsGameObjetcs;
    void Awake()
    {
        _uiActions = new UIActions();
        _blueprintsGameObjetcs = new List<GameObject>();
        HideMenu();
    }

    public void HideMenu()
    {
        _uiActions.OnOffCanvasGroup(allMenu, false);
    }

    public void ShowMenu()
    {
        _uiActions.OnOffCanvasGroup(allMenu, true);
        //Control de instancia
        LevelControl();
        ShowMachineMenu();
    }

    public void ShowMachineMenu()
    {
        _uiActions.OnOffCanvasGroup(machineMenu, true);
        _uiActions.OnOffCanvasGroup(mechaMenu, false);
        _uiActions.OnOffCanvasGroup(systemsMenu, false);
    }

    public void ShowMechaMenu()
    {
        _uiActions.OnOffCanvasGroup(machineMenu, false);
        _uiActions.OnOffCanvasGroup(mechaMenu, true);
        _uiActions.OnOffCanvasGroup(systemsMenu, false);
    }

    public void ShowSystemsMenu()
    {
        _uiActions.OnOffCanvasGroup(machineMenu, false);
        _uiActions.OnOffCanvasGroup(mechaMenu, false);
        _uiActions.OnOffCanvasGroup(systemsMenu, true);
    }

    //verifico el nivel de la consola e instancio los planos que corresponda
    private void LevelControl()
    {
        //limpio la lista de planos instaciados por las dudas
        ClearList();
        //instancio los planos correspondientes de cada nivel
        for (int i = 0; i < _consoleLevel+1; i++)
        {
            InstantiateBlueprints(prefabsPerLevel[i].prefabsBlueprints);       
        }
    }

    // subir de nivel la mesa de crafteo para obtener mas planos
    public void Upgrade()
    {
        if(_consoleLevel+1 < prefabsPerLevel.Count)
        {
            //subo de nivel la consola
            _consoleLevel++;
            LevelControl();
        }
        else
        {
            //informo de que no puedo subir de nivel la consola
            print("No se puede subir de nivel la consola");
        }
    }

    private void InstantiateBlueprints(List<GameObject> prefabs)
    {
        List<GameObject> machines = new List<GameObject>();
        List<GameObject> parts = new List<GameObject>();
        List<GameObject> systems = new List<GameObject>();
        foreach (var item in prefabs)
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
            _blueprintsGameObjetcs.Add(Instantiate(item, parent));
        }
    }

    private void ClearList()
    {
        if (_blueprintsGameObjetcs.Count > 0)
        {
            foreach (var item in _blueprintsGameObjetcs)
            {
                Destroy(item);
            }
            _blueprintsGameObjetcs.Clear();
        }
    }

    public void Craft(BlueprintGameObject blueprint)
    {
        //gasto los recursos necesarios para el crafteo
        for (int i = 0; i < blueprint.blueprintIngredients.Count; i++)
        {
            inventory.UseResource(blueprint.blueprintIngredients[i], blueprint.ingredientsAmount[i]);
        }
        // creo el objeto necesario y lo guardo en el inventario
        switch (blueprint.blueprintType)
        {
            case BlueprintType.Machine:
                Machine machine = new Machine(Machine.GetName(blueprint.blueprintName));
                inventory.Store(machine);
                break;
            case BlueprintType.Part:
                PartMecha part = new PartMecha(PartMecha.GetName(blueprint.blueprintName));
                inventory.Store(part);
                break;
            case BlueprintType.System:
                SystemMecha system = new SystemMecha(SystemMecha.GetName(blueprint.blueprintName));
                inventory.Store(system);
                break;
        }
    }
}
