using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechaUI : MonoBehaviour
{
    public CanvasGroup allUI, 
    mecha,
    sistemasEquipados1,
    sistemasEquipados2,
    sistemasEquipados3,
    sistemasEquipados4,
    sistemasEquipados5;

    public MechaInfo mechaGameObject;
    public ParteMechaInfo laParte;

    void Awake()
    {
        HideMenu();
    }

    public void ShowMenu()
    {
        allUI.alpha = 1f;
        allUI.interactable = true;
        allUI.blocksRaycasts = true;
        ShowInfoMecha();
    }

    public void HideMenu()
    {
        allUI.alpha = 0f;
        allUI.interactable = false;
        allUI.blocksRaycasts = false;
        mecha.alpha = 0f;
        sistemasEquipados1.alpha = 0f;
        sistemasEquipados2.alpha = 0f;
        sistemasEquipados3.alpha = 0f;
        sistemasEquipados4.alpha = 0f;
        sistemasEquipados5.alpha = 0f;
        sistemasEquipados1.blocksRaycasts = false;
        sistemasEquipados2.blocksRaycasts = false;
        sistemasEquipados3.blocksRaycasts = false;
        sistemasEquipados4.blocksRaycasts = false;
        sistemasEquipados5.blocksRaycasts = false;
    }

    private void ShowInfoMecha()
    {
        mecha.alpha = 1f;
    }

    public void MostrarSistemas(ParteMechaInfo parte)
    {
        if(laParte != null)
        {
            laParte.referenciaGameObjetc.enabled = false;
        }
        laParte = parte;
        HideMenu();
        ShowMenu();
        parte.canvas.alpha = 1f;
        parte.canvas.blocksRaycasts = true;
    }
}
