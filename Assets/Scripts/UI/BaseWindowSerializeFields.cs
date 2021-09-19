using UnityEngine;
using UnityEngine.UI;

public class BaseWindowSerializeFields : MonoBehaviour
{
    public CanvasGroup CanvasGroup => canvasGroup;
    public RectTransform Window => window;
    public Button CancelButton => cancelButton;

    [SerializeField]
    private CanvasGroup canvasGroup = null;
    [SerializeField]
    private RectTransform window = null;
    [SerializeField]
    private Button cancelButton = null;
}