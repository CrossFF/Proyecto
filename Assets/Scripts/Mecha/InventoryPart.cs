using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryPart : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //parametro
    public PartMecha part;
    /// referencias
    public CanvasGroup canvasGroup;
    public Text nombreParte;
    public Image imagenParte;
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

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.transform.tag == "Mecha")
            {
                PartGameObject temp = hit.transform.GetComponent<PartGameObject>();
                if (temp.Equippable(part))
                {
                    temp.SetPart(part);
                }
            }
        }
        canvasGroup.blocksRaycasts = true;
        transform.parent = parent;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
