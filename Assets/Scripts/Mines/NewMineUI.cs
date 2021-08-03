using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMineUI : MonoBehaviour
{
    [Header("Referencias")]
    public CanvasGroup allUI;
    public CanvasGroup infoMine;
    public Text nameText;
    public Text statusText;
    public Text conectionsText;
    public GameObject prefabResource; // info basica del recurso
    public Transform parentResource;
    private Mine _mine;
    private List<DispoResource> _resources;

    void Awake()
    {
        _resources = new List<DispoResource>();
    }

    public void ShowMenu()
    {
        allUI.alpha = 1f;
        allUI.blocksRaycasts = true;
        allUI.interactable = true;
        HideMine();
    }

    public void HideMenu()
    {
        allUI.alpha = 0f;
        allUI.blocksRaycasts = false;
        allUI.interactable = false;
        HideMine();
    }

    public void ShowMine(Mine mine)
    {
        _mine = mine;
        infoMine.alpha = 1f;
        infoMine.interactable = true;
        infoMine.blocksRaycasts = true;
        //seteo la info basica de la mina
        nameText.text = _mine.node.name;
        string text = "Mina: " + _mine.node.status;
        statusText.text = text;
        conectionsText.text = "Esta mina conecta con: " + _mine.node.trails.Count + " minas";
        // insatancio los recursos
        foreach (var item in _mine.node.resources)
        {
            _resources.Add(Instantiate(prefabResource, parentResource).GetComponent<DispoResource>());
        }
        //seteo la info de los recursos
        for (int i = 0; i < _resources.Count; i++)
        {
            // info del recurso
            float fillAmount = _mine.node.resources[i].amount / _mine.node.resources[i].totalAmount;
            _resources[i].fillImage.fillAmount = fillAmount;
            _resources[i].nombreRecurso.text = _mine.node.resources[i].type.ToString();
            var sprite1 = Resources.Load<Sprite>("Prototype/" + _mine.node.resources[i].type);
            _resources[i].imageRecurso.sprite = sprite1;
            // info de la maquina
            if (_mine.node.resources[i].machine != null)
            {
                _resources[i].nombreMaquina.text = _mine.node.resources[i].machine.name.ToString();
                var sprite2 = Resources.Load<Sprite>("Prototype/" + _mine.node.resources[i].machine.name);
                _resources[i].imageMachine.sprite = sprite2;
            }
            else
            {
                _resources[i].nombreMaquina.text = "Empty";
            }

        }
    }

    public void HideMine()
    {
        infoMine.alpha = 0f;
        infoMine.interactable = false;
        infoMine.blocksRaycasts = false;
        foreach (var item in _resources)
        {
            Destroy(item.gameObject);
        }
        _resources.Clear();
    }
}
