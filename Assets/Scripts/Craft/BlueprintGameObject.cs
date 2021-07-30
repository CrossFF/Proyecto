using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintGameObject : MonoBehaviour
{
    [Header("Parametros")]
    public BlueprintName blueprintName;
    public BlueprintType blueprintType;
    public List<BlueprintIngredients> blueprintIngredients;
    public List<int> ingredientsAmount;
    private bool _crafteable;

    [Header("Referencias")]
    public CraftingManager manager;
    public GameObject prefabIngredientCraft;
    public Text nameText, amountText;
    public Image craftImage;
    public Button fabricateButton;
    public Transform ingredientsParent;
    private List<IngredientGameObject> _ingredients;

    void Start()
    {
        _ingredients = new List<IngredientGameObject>();
        SetInfo();
    }

    void Update()
    {
        CraftControl();
        SetInfo();
    }

    private void CraftControl()
    {
        // verifico si hay suficiente de ese ingrediente en el inventario
        int num = 0;
        for (int i = 0; i < blueprintIngredients.Count; i++)
        {
            float actual = manager.inventory.GetAmount(blueprintIngredients[i].ToString());
            if (ingredientsAmount[i] <= actual) num++;
        }
        _crafteable = num == blueprintIngredients.Count ? true : false;
        //activo y descativo boton
        fabricateButton.interactable = _crafteable ? true : false;
    }

    private void SetInfo()
    {
        //instanciar ingredientes
        if (_ingredients.Count == 0)
        {
            foreach (var item in blueprintIngredients)
            {
                GameObject temp = Instantiate(prefabIngredientCraft, ingredientsParent);
                _ingredients.Add(temp.GetComponent<IngredientGameObject>());
            }
        }
        // seteo la info basica del craft
        //nombre
        nameText.text = blueprintName.ToString();
        //existencias
        float actualAmount = manager.inventory.GetAmount(blueprintName.ToString());
        amountText.text = "Tienes: " + actualAmount;
        //imagen
        var sprite = Resources.Load<Sprite>("");
        craftImage.sprite = sprite;
        //seteo la info de los ingredientes
        for (int i = 0; i < _ingredients.Count; i++)
        {
            //imagen
            var spriteTemp = Resources.Load<Sprite>("Prototype/" + blueprintIngredients[i]);
            _ingredients[i].resourceImage.sprite = spriteTemp;
            //cantidad necesaria y en el inventario
            float necesario = ingredientsAmount[i];
            float actual = manager.inventory.GetAmount(blueprintIngredients[i].ToString());
            _ingredients[i].amountText.text = actual + "/" + necesario;
        }
    }

    public void Craft()
    {
        //
        manager.Craft(this);
    }
}
