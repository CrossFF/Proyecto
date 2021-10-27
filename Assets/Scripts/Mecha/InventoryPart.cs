using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryPart : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler,IPointerExitHandler
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
        SonidoManager sonidoManager = GameObject.Find("Manager de Sonido").GetComponent<SonidoManager>();
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.transform.tag == "Mecha")
            {
                PartGameObject temp = hit.transform.GetComponent<PartGameObject>();
                if (temp.Equipable(part))
                {
                    // instalo parte
                    temp.SetPart(part);
                    // sonido de instalacion de parte
                    sonidoManager.PlayUISound(EventoSonoroUI.ParteInstalada);
                }
                else
                {
                    // sonido de error
                    sonidoManager.PlayUISound(EventoSonoroUI.Error);
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
