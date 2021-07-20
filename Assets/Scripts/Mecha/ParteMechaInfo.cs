using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ParteMechaInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public MechaUI ui;

    public CanvasGroup canvas;

    public Text nombre,
    atk,
    def,
    sistema1Text,
    sistema2Text,
    sistema3Text;

    public Image energiaUtilizada;

    public SistemaEquipado sistema1,
    sistema2,
    sistema3;
    public string nombreParte;

    public int ataqueBase,
    defensaBase,
    energiaMaxima,
    energiActual;
    public bool conEnergia = true;
    public Outline referenciaGameObjetc;

    void Start()
    {
        nombre.text = nombreParte;
        atk.text = "ATK " + ataqueBase.ToString();
        def.text = "DEF " + defensaBase.ToString();
        sistema1Text.text = sistema1.info.nombreSistema;
        sistema2Text.text = sistema2.info.nombreSistema;
        sistema3Text.text = sistema3.info.nombreSistema;
        energiActual = energiaMaxima;
    }

    void Update()
    {
        energiaUtilizada.fillAmount = energiActual / energiaMaxima;
        ControlDeEnergia();
        SetInfo();
    }

    private void SetInfo()
    {
        sistema1Text.text = sistema1.info.nombreSistema;
        sistema2Text.text = sistema2.info.nombreSistema;
        sistema3Text.text = sistema3.info.nombreSistema;
    }

    private void ControlDeEnergia()
    {
        float sumaDeEnergias = sistema1.energiaActual + sistema2.energiaActual + sistema3.energiaActual;
        sumaDeEnergias = sumaDeEnergias / energiaMaxima;
        energiaUtilizada.fillAmount = 1 - sumaDeEnergias;
        if (energiaUtilizada.fillAmount <= 0)
        {
            conEnergia = false;
        }
        else
        {
            conEnergia = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        referenciaGameObjetc.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ui.laParte != this)
        {
            referenciaGameObjetc.enabled = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ui.MostrarSistemas(this);
    }

    public int SetAtaque(int stat)
    {
        stat += ataqueBase;
        stat = sistema1.SetAtaque(stat);
        stat = sistema2.SetAtaque(stat);
        stat = sistema3.SetAtaque(stat);
        return stat;
    }

    public int SetDefensa(int stat)
    {
        stat += defensaBase;
        stat = sistema1.SetDefensa(stat);
        stat = sistema2.SetDefensa(stat);
        stat = sistema3.SetDefensa(stat);
        return stat;
    }

    public int SetCalor(int stat)
    {
        stat = sistema1.SetCalor(stat);
        stat = sistema2.SetCalor(stat);
        stat = sistema3.SetCalor(stat);
        return stat;
    }

    public int SetFrio(int stat)
    {
        stat = sistema1.SetFrio(stat);
        stat = sistema2.SetFrio(stat);
        stat = sistema3.SetFrio(stat);
        return stat;
    }

    public int SetO2(int stat)
    {
        stat = sistema1.SetO2(stat);
        stat = sistema2.SetO2(stat);
        stat = sistema3.SetO2(stat);
        return stat;
    }

    public int SetComida(int stat)
    {
        stat = sistema1.SetComida(stat);
        stat = sistema2.SetComida(stat);
        stat = sistema3.SetComida(stat);
        return stat;
    }
}
