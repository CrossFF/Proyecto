using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceInfo : MonoBehaviour
{
    public MineUI ui;
    public Resource resource;
    public Text resourceName;
    public Image resourceImage;
    public Image machineImage;
    public Image disponibilidad;

    void Update()
    {
        RefreshInfo();
    }

    public void SetInfo(Resource theResource, MineUI theUI)
    {
        ui = theUI;
        resource = theResource;
        var sprite = Resources.Load<Sprite>("Prototype/" + resource.type);
        resourceImage.sprite = sprite;
        resourceName.text = resource.type.ToString();
        RefreshInfo();
    }

    public void RefreshInfo()
    {
        disponibilidad.fillAmount = Resource.CalculateAmountOfResource(resource);
        //disponibilidad.fillAmount = resource.amount / resource.totalAmount;
        if (resource.machine != null)
        {
            var sprite = Resources.Load<Sprite>("Prototype/" + resource.machine.name);
            machineImage.sprite = sprite;
        }
    }

    public void SetMachine()
    {
        Machine machine = new Machine();
        if (resource.machine == null)
        {
            resource.machine = machine;
        }
        RefreshInfo();
    }
}
