using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineUI : MonoBehaviour
{
    private List<GameObject> _activeMines;
    private List<GameObject> _resourcesInfo;
    private List<GameObject> _concetedMines;
    private List<GameObject> _inactiveMines;
    public GameObject prefabInfoNode, prefabInfoResource;
    public CanvasGroup allUI, activeNodePanel, resourcePanel, conectedNodePanel, inactiveNodePanel;
    public Transform active, resource, conected, inactive;
    public MineManager manager;
    public Mine mineSelected;

    void Awake()
    {
        _activeMines = new List<GameObject>();
        _resourcesInfo = new List<GameObject>();
        _concetedMines = new List<GameObject>();
        _inactiveMines = new List<GameObject>();
        HideMenu();
    }

    void Update()
    {

    }

    public void ShowMenu(List<Mine> activeMines)
    {
        allUI.alpha = 1f;
        allUI.interactable = true;
        allUI.blocksRaycasts = true;
        ShowActivesMines(activeMines);
    }

    private void ShowMenu()
    {
        HideMenu();
        allUI.alpha = 1f;
        allUI.interactable = true;
        allUI.blocksRaycasts = true;
        ShowActivesMines(manager.GetActiveMines());
    }

    public void HideMenu()
    {
        ClearLists("active");
        ClearLists("conected");
        ClearLists("inactive");
        ClearLists("resource");
        allUI.alpha = 0f;
        allUI.interactable = false;
        allUI.blocksRaycasts = false;
        activeNodePanel.alpha = 0f;
        resourcePanel.alpha = 0f;
        conectedNodePanel.alpha = 0f;
        inactiveNodePanel.alpha = 0f;
    }

    public void SeeMine(Mine mine)
    {
        mineSelected = mine;
        ShowResources(mine);
        ShowConectedMines(mine);
    }

    public void ConectMines()
    {
        ShowInactiveMines();
    }

    public void ConectMines(Mine mine)
    {
        // le pido al al manager que conecte las dos minas
        manager.ConectMines(mineSelected, mine);
        ShowMenu();
    }

    public void ShowMine(Mine mine)
    {
        mineSelected = mine;
        ShowActivesMines(manager.GetActiveMines());
        SeeMine(mine);
    }

    private void ClearLists(string type)
    {
        switch (type)
        {
            case "active":
                if (_activeMines.Count != 0)
                {
                    foreach (var item in _activeMines)
                    {
                        Destroy(item);
                    }
                }
                _activeMines.Clear();
                break;
            case "resource":
                if (_resourcesInfo.Count != 0)
                {
                    foreach (var item in _resourcesInfo)
                    {
                        Destroy(item);
                    }
                }
                _resourcesInfo.Clear();
                break;
            case "conected":
                if (_concetedMines.Count != 0)
                {
                    foreach (var item in _concetedMines)
                    {
                        Destroy(item);
                    }
                }
                _concetedMines.Clear();
                break;
            case "inactive":
                if (_inactiveMines.Count != 0)
                {
                    foreach (var item in _inactiveMines)
                    {
                        Destroy(item);
                    }
                }
                _inactiveMines.Clear();
                break;
            default:
                break;
        }
    }

    private void ShowActivesMines(List<Mine> activeMines)
    {
        ClearLists("active");
        activeNodePanel.alpha = 1f;
        _activeMines = InstantiatePrefab(activeMines, active);
    }

    private void ShowResources(Mine mine)
    {
        ClearLists("resource");
        resourcePanel.alpha = 1f;
        foreach (var item in mine.node.resources)
        {
            GameObject temp = Instantiate(prefabInfoResource, resource);
            _resourcesInfo.Add(temp);
            temp.GetComponent<ResourceInfo>().SetInfo(item, this);
        }
    }
    private void ShowConectedMines(Mine mine)
    {
        ClearLists("conected");
        conectedNodePanel.alpha = 1f;
        _concetedMines = InstantiatePrefab(mine.node.trails, conected);
    }

    private void ShowInactiveMines()
    {
        ClearLists("inactive");
        // le pido al manager las minas inactivas
        List<Mine> inactiveMines = manager.GetInactiveMines();
        inactiveNodePanel.alpha = 1f;
        _inactiveMines = InstantiatePrefab(inactiveMines, inactive);
    }

    private List<GameObject> InstantiatePrefab(List<Mine> mines, Transform parent)
    {
        List<GameObject> goTemps = new List<GameObject>();
        if (mines.Count != 0)
        {
            foreach (var item in mines)
            {
                GameObject temp = Instantiate(prefabInfoNode, parent);
                goTemps.Add(temp);
                temp.GetComponent<BasicMineInfo>().SetInfo(item, this);
            }
        }
        return goTemps;
    }
}
