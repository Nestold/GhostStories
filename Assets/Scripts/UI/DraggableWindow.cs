using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWindow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField]
    private BaseWindowSerializeFields SerializeFields = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        SerializeFields.CanvasGroup.alpha = 0.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        SerializeFields.Window.anchoredPosition += eventData.delta / GameManager.Instance.SceneManager.UIManager.UICanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SerializeFields.CanvasGroup.alpha = 1f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SerializeFields.Window.SetAsLastSibling();
    }
}
