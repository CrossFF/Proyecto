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
    public CanvasGroup allMenu, machineMenu, mechaMenu, systemsMenu, craftPanel, upgradePanel;
    public Transform machineContent, mechaContent, systemsContent;
    public Button machineButton, mechaButton, systemButton;
    private List<GameObject> _blueprintsGameObjetcs;

    [Header("Confirmacion de Upgrade")]
    public Text textNewBlueprints;
    public GameObject prefabMaterial;
    public Transform parentMaterial;
    private List<IngredientGameObject> _nextLevelMaterials;
    public Button confirmarButton;

    void Awake()
    {
        _uiActions = new UIActions();
        _blueprintsGameObjetcs = new List<GameObject>();
        _nextLevelMaterials = new List<IngredientGameObject>();
        HideMenu();
    }

    public void HideMenu()
    {
        _uiActions.OnOffCanvasGroup(allMenu, false);
    }

    public void ShowMenu()
    {
        _uiActions.OnOffCanvasGroup(allMenu, true);
        _uiActions.OnOffCanvasGroup(craftPanel, true);
        _uiActions.OnOffCanvasGroup(upgradePanel, false);
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
        for (int i = 0; i < _consoleLevel + 1; i++)
        {
            InstantiateBlueprints(prefabsPerLevel[i].prefabsBlueprints);
        }
    }

    public void MenuDeConfirmacion()
    {
        // muestro el menu de confirmacion y oculto el de craft
        _uiActions.OnOffCanvasGroup(craftPanel, false);
        _uiActions.OnOffCanvasGroup(upgradePanel, true);
        // instacio el nivel disponible
        if (_consoleLevel + 1 < prefabsPerLevel.Count)
        {
            //variable de siguiente nivel
            int nextLevel = _consoleLevel + 1;
            // materiales
            // borro los elementos que estuvieran en los materiales
            foreach (var item in _nextLevelMaterials)
            {
                Destroy(item.gameObject);
            }
            _nextLevelMaterials.Clear();
            // index para evitar errores
            int index = 0; 
            foreach (var ingrediente in prefabsPerLevel[nextLevel].materials)
            {
                // instancio un material
                IngredientGameObject tempMaterial;
                tempMaterial = Instantiate(prefabMaterial, parentMaterial).GetComponent<IngredientGameObject>();
                // imagen del material
                var sprite = Resources.Load<Sprite>("Ores/" + ingrediente);
                tempMaterial.resourceImage.sprite = sprite;
                // cantidades del material
                float actualTemp = inventory.GetAmount(ingrediente);
                float necesarioTemp = prefabsPerLevel[nextLevel].amounts[index];
                tempMaterial.amountText.text = actualTemp + "/" + necesarioTemp;
                // agrego el material a la lista
                _nextLevelMaterials.Add(tempMaterial);
                index++;
            }  
            // blueprints
            // borro lo que tuviera escrio de antes;
            textNewBlueprints.text = "";
            foreach (var item in prefabsPerLevel[nextLevel].prefabsBlueprints)
            {
                textNewBlueprints.text += _uiActions.CleanString(item.GetComponent<BlueprintGameObject>().blueprintName.ToString()) + "\n";
            }
            // verifico si los recursos disponibles son suficientes para la actualizacion
            int num = 0;
            for (int i = 0; i < prefabsPerLevel[nextLevel].materials.Count; i++)
            {
                float actual = inventory.GetAmount(prefabsPerLevel[nextLevel].materials[i]);
                if(actual >= prefabsPerLevel[nextLevel].amounts[i])
                    num++;
            }
            confirmarButton.interactable = num == prefabsPerLevel[nextLevel].materials.Count ? true : false;   
        }
        else
        {
            // no es posible seguir actualizando
            // desabilito boton de confirmacion
            confirmarButton.interactable = false;
        }
    }

    public void CancelarMenuDeConfirmacion()
    {
        // muestro el menu de craft y oculto el de confirmacion
        _uiActions.OnOffCanvasGroup(craftPanel, true);
        _uiActions.OnOffCanvasGroup(upgradePanel, false);
    }

    // subir de nivel la mesa de crafteo para obtener mas planos
    public void Upgrade()
    {
        // cierro menu de confirmacion mustro menu de craft
        _uiActions.OnOffCanvasGroup(upgradePanel, false);
        _uiActions.OnOffCanvasGroup(craftPanel, true);
        //subo de nivel la consola
        _consoleLevel++;
        // uso los recursos necesarios
        int index = 0;
        foreach (var item in prefabsPerLevel[_consoleLevel].materials)
        {
            inventory.UseResource(item,prefabsPerLevel[_consoleLevel].amounts[index]);
            index++;
        }
        // actualizo los blueprints
        LevelControl();
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
