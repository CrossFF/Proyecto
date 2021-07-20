using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechaInfo : MonoBehaviour
{
    public Text nombreText,
    calorText,
    frioText,
    defText,
    atkText,
    o2Text,
    comidaText;

    public string nombreMecha;
    public List<ParteMechaInfo> partes;
    public int DEFcalor = 0,
    DEFfrio = 0,
    DEF = 0,
    ATK = 0,
    O2 = 0,
    comida = 0;

    void Update()
    {
        SetValues();
        SetInfo();
    }

    private void SetValues()
    {
        int atk = 0, def = 0, cal = 0, fri = 0, o2 = 0, com = 0;
        foreach (var item in partes)
        {
            //defino ataque
            atk = item.SetAtaque(atk);
            //defino defensa
            def = item.SetDefensa(def);
            //defino calor
            cal = item.SetCalor(cal);
            //defino frio
            fri = item.SetFrio(fri);
            //defino o2
            o2 = item.SetO2(o2);
            //defino comida
            com = item.SetComida(com);
        }
        ATK = atk;
        DEF = def;
        DEFcalor = cal;
        DEFfrio = fri;
        O2 = o2;
        comida = com;
    }
    private void SetInfo()
    {
        nombreText.text = nombreMecha;
        atkText.text = "ATK: " + ATK.ToString();
        defText.text = "DEF: " + DEF.ToString();
        calorText.text = "DEF calor: " + DEFcalor.ToString();
        frioText.text = "DEF frio: " + DEFfrio.ToString();
        o2Text.text = "O2: " + O2.ToString();
        comidaText.text = "Comida: " + comida.ToString();
    }
}
