using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionDeEnergia : MonoBehaviour
{
    private SystemMecha _sistema; // sistema que esta modificando
    public Text textNombreSistema;
    public Text textEnergiaDisponible;
    public Image imagenBarraEnergia;
    public Slider sliderEnergiaAplicada;
    public MechaManager manager;

    // le seteo un sistema
    public void SetSystem(SystemMecha system)
    {
        _sistema = system;
        SetInfo();
    }

    private void SetInfo()
    {
        textNombreSistema.text = new UIActions().CleanString(_sistema.name.ToString());
        // seteo el slider
        if(_sistema.name == SystemName.Bateria)
        {
            sliderEnergiaAplicada.interactable = false;
            sliderEnergiaAplicada.value = 1f;
        }
        else
        {
            sliderEnergiaAplicada.value = _sistema.energyAsigned / _sistema.GetEnergyToWork();
            sliderEnergiaAplicada.interactable = true;
        }
    }

    void Update()
    {
        float energiaActual = manager.GetEnergiaTotal() - manager.GetEnergiaAsignada();
        float fillAmount = energiaActual / manager.GetEnergiaTotal();
        imagenBarraEnergia.fillAmount = fillAmount;
        textEnergiaDisponible.text = energiaActual.ToString();
        if(_sistema != null)
        {
            if(_sistema.name != SystemName.Bateria)
            {
                _sistema.energyAsigned = sliderEnergiaAplicada.value * _sistema.GetEnergyToWork();
            }
        }
    }

    public void GuardarCambios()
    {
        transform.parent.GetComponent<UIFuncionamiento>().OcultarPanelDeEnergia();
        _sistema = null;
    }
}
