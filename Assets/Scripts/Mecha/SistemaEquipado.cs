using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaEquipado : MonoBehaviour
{
    public SistemaInfo info;
    public SistemaInfo[] listaInfo;
    private int index = 0;
    public Text nombre;
    public Image imagen;
    public Slider energia;
    public ParteMechaInfo parteCorrespondiente;
    public float energiaActual;

    void Start()
    {
        CambiarSistema();
        energia.value = 0;
    }

    void Update()
    {
        // controlo que la energia no supere la energia disponible
        if(parteCorrespondiente.conEnergia)
        {
            energiaActual = energia.value * info.energiaMaxima;
        }
        else
        {
            if(energia.value > energiaActual/info.energiaMaxima)
            {
                energia.value = energiaActual/info.energiaMaxima;
            }
            else
            {
                energiaActual = energia.value * info.energiaMaxima;
            }
        }
        
    }

    public void CambiarSistema()
    {
        energiaActual = 0f;
        energia.value = 0f;
        index++;
        if (index > listaInfo.Length - 1)
        {
            index = 0;
        }
        info = listaInfo[index];
        SetInfo();
    }

    private void SetInfo()
    {
        nombre.text = info.nombreSistema;
        imagen.color = info.color;
    }

    public int SetAtaque(int stat)
    {
        if (info.nombreSistema == "Ataque")
        {
            stat += GetEfecto();
        }
        return stat;
    }

    public int SetDefensa(int stat)
    {
        if (info.nombreSistema == "Defensa")
        {
            stat += GetEfecto();
        }
        return stat;
    }

    public int SetCalor(int stat)
    {
        if (info.nombreSistema == "Calor")
        {
            stat += GetEfecto();
        }
        return stat;
    }

    public int SetFrio(int stat)
    {
        if (info.nombreSistema == "Frio")
        {
            stat += GetEfecto();
        }
        return stat;
    }

    public int SetO2(int stat)
    {
        if (info.nombreSistema == "O2")
        {
            stat += GetEfecto();
        }
        return stat;
    }

    public int SetComida(int stat)
    {
        if (info.nombreSistema == "Comida")
        {
            stat += GetEfecto();
        }
        return stat;
    }

    private int GetEfecto()
    {
        int efecto = 0;  
        efecto = Mathf.FloorToInt(energia.value * info.valorEfecto);
        return efecto;
    }
}
