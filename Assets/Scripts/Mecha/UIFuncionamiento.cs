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
    public GestionDeEnergia gestionDeEnergia;

    [Header("Esquema de colores")]
    public Color funcionando;
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
        for (int i = 0; i < partes.Count; i++)
        {
            PartMecha part = manager.GetPart(i);
            // si la parte existe
            if (part != null)
            {
                // color de funcionamiento
                partes[i].parte.color = funcionando;
                // chequeo los sistemas de la parte
                for (int x = 0; x < 3; x++)
                {
                    if (x < part.systems.Count)
                    {
                        SystemMecha system = part.systems[x];
                        partes[i].sistemas[x].GetComponent<SystemUIFuncionamiento>().system = system;
                        // le asigno un color
                        partes[i].sistemas[x].color = system.Working() ? funcionando : sinFuncionar;
                    }
                    else
                    {
                        partes[i].sistemas[x].color = sinEquipar;
                        partes[i].sistemas[x].GetComponent<SystemUIFuncionamiento>().system = null;
                    }
                }
            }
            else
            {
                // color no funcionado
                partes[i].parte.color = sinFuncionar;
                // sistemas sin equipar
                foreach (var system in partes[i].sistemas.ToArray())
                {
                    system.color = sinEquipar;
                }
            }
        }
    }

    // setea los valores del meka
    private void SetValues()
    {
        textAtaque.text = manager.GetAtaque().ToString();
        textDefensa.text = manager.GetDefensa().ToString();
        textFrio.text = manager.GetFrio().ToString();
        textCalor.text = manager.GetCalor().ToString();
        textEnergia.text = manager.GetEnergiaTotal().ToString();
    }

    // muestro el menu de gestion de energia
    public void GestionarEnergia(SystemMecha system)
    {
        _uiActions.OnOffCanvasGroup(panelDeEnergia, true);
        gestionDeEnergia.SetSystem(system);
    }

    public void OcultarPanelDeEnergia()
    {
        _uiActions.OnOffCanvasGroup(panelDeEnergia, false);
    }
}
