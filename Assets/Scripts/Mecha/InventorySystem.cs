using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySystem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    //parametro
    public SystemMecha system;
    /// referencias
    public CanvasGroup canvasGroup;
    public Text nombreSistema;
    public Image imagenSistema;
    public Canvas canvas;
    public RectTransform rectTransform;
    public Transform parent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        transform.parent = canvas.transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // cuando termina el drag
    public void OnEndDrag(PointerEventData eventData)
    {
        // uso un raycast
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            // si el objeto es un mecha
            if (hit.transform.tag == "Mecha")
            {
                // pido la informacion de la parte inpactada
                PartGameObject part = hit.transform.GetComponent<PartGameObject>();
                // si la parte esta equipada
                if (part.Equiped())
                {
                    //verifico si hay espacio disponible para el sistema y si el systema es compatible con la parte
                    if (part.CheckSystemCapacity() && part.CheckSystemCompatibility(system))
                    {
                        // equipo el sistema
                        part.SetSystem(system);
                    }
                }
                else
                {
                    // la accion no es posible
                }
            }
        }
        canvasGroup.blocksRaycasts = true;
        transform.parent = parent;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.8f;
    }
}
