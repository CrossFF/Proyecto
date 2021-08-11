using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaManager : MonoBehaviour
{
    public Material visto, oculto;// materiales para las partes que estan colocadas en el meca
    public List<PartGameObject> parts; // partes del mecha

    void Start()
    {
        CheckParts();
    }

    private void CheckParts()
    {
        // verifico si la parte efectivamente tiene algo equipado
        foreach (var item in parts)
        {
            if(item.Equiped())
            {
                item.AsignMaterial(visto);
            }
            else
            {
                item.AsignMaterial(oculto);
            }
        }
    }


}
