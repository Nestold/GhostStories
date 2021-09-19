using UnityEngine;

public class UIFPPManager : MonoBehaviour
{
    [SerializeField]
    private GameObject normalPointer = null;

    [SerializeField]
    private GameObject highlightedPointer = null;

    public void SetPointer(bool isHighlighted)
    {
        normalPointer.SetActive(!isHighlighted);
        highlightedPointer.SetActive(isHighlighted);
    }
}
