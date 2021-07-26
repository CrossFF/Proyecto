using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintGameObject : MonoBehaviour
{
    //public Blueprint info;
    public CraftingManager manager;
    public GameObject prefabIngredientCraft;
    public Text nameText, amountText;
    public Image craftImage;
    public Transform ingredientsParent;

    void Update()
    {
        SetInfo();
    }

    public void SetInfo(CraftingManager manager)
    {
        this.manager = manager;
    }

    private void SetInfo()
    {
        
    }

    public void Craft()
    {

    }
}
