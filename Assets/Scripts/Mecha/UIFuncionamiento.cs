using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFuncionamiento : MonoBehaviour
{
    public List<InformacionParteUIFuncionamiento> partes;

    public CanvasGroup panelDeEnergia;
    public MechaManager manager;
    private UIActions _uiActions;

    [Header("Esquema de colores")]
    public Color funcionando;
    public Color medioFuncionamiento;
    public Color sinFuncionar;
    public Color sinEquipar;

    [Header("Valores de Meka")]
    public Text textAtaque;
    public Text textDefensa;
    public Text textCalor;
    public Text textFrio;
    public Text textEnergia;

    void Awake()
    {
        _uiActions = new UIActions();
        // desactivo el panel de energia
        _uiActions.OnOffCanvasGroup(panelDeEnergia, false);
    }

    void LateUpdate()
    {
        Funcionamiento();
        SetValues();
    }

    // verifca el estado de las partes, y les asigna un color dependiendo del mismo
    private void Funcionamiento()
    {
        // recorro las partes y verfico su estado en el manager
        int index = 0;
        foreach (var item in partes.ToArray())
        {
            PartMecha part = manager.GetPart(index);
            // si la parte existe
            if (part != null)
            {
                // color de funcionamiento
                item.parte.color = funcionando;
                // chequeo los sistemas de la parte
                if (part.systems.Count != 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        SystemMecha system = part.systems[i];
                        if (system != null)
                        {
                            item.sistemas[i].color = system.Working() ? funcionando : sinFuncionar;
                        }
                        else
                        {
                            item.sistemas[i].color = sinEquipar;
                        }
                    }
                }
            }
            else
            {
                // color no funcionado
                item.parte.color = sinFuncionar;
                // sistemas sin equipar
                foreach (var system in item.sistemas.ToArray())
                {
                    system.color = sinEquipar;
                }
            }
            index++;
        }
    }

    // setea los valores del meka
    private void SetValues()
    {
        textAtaque.text = manager.GetAtaque().ToString();
        textDefensa.text = manager.GetDefensa().ToString();
        textFrio.text = manager.GetFrio().ToString();
        textCalor.text = manager.GetCalor().ToString();
        textEnergia.text = manager.GetEnergia().ToString();
    }
}
