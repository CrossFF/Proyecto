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

    private float _sliderMaxValue = 0;

    // le seteo un sistema
    public void SetSystem(SystemMecha system)
    {
        _sistema = system;
        SetInfo();
    }

    private void SetInfo()
    {
        textNombreSistema.text = new UIActions().CleanString(_sistema.name.ToString());
        // seteo si el slider es interactuable
        if (_sistema.name == SystemName.Bateria)
        {
            sliderEnergiaAplicada.interactable = false;
        }
        else
        {
            sliderEnergiaAplicada.interactable = true; 
        }

        // seteo el valor del slider segun si el sistema tiene energia o no
        if(_sistema.Working())
        {
            sliderEnergiaAplicada.value = 1f;
        }
        else
        {
            sliderEnergiaAplicada.value = _sistema.energyAsigned / _sistema.GetEnergyToWork();
        }
    }

    void Update()
    {
        float energiaActual = manager.GetEnergiaTotal() - manager.GetEnergiaAsignada();
        float fillAmount = energiaActual / manager.GetEnergiaTotal();
        float sliderMaxValue = 0f;
        imagenBarraEnergia.fillAmount = fillAmount;
        textEnergiaDisponible.text = energiaActual.ToString();
        if (_sistema != null)
        {
            sliderMaxValue = energiaActual + _sistema.energyAsigned / _sistema.GetEnergyToWork();
            if (_sistema.name != SystemName.Bateria)
            {
                sliderEnergiaAplicada.value = Mathf.Clamp(sliderEnergiaAplicada.value, 0f, sliderMaxValue);
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
