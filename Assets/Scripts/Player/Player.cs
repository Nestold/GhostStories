using UnityEngine;

[RequireComponent(typeof(PlayerSerializeFields))]
public class Player : MonoBehaviour
{
    private PlayerSerializeFields serializeFields;

    private void Awake()
    {
        serializeFields = GetComponent<PlayerSerializeFields>();
    }

    private void Update()
    {
        if(GameManager.Instance.SceneManager.CurrentPlayerState == EPlayerState.FPP && !GameManager.Instance.SceneManager.Freeze)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 2f, 1 << 9) && hit.transform.TryGetComponent(out UsableObject usableObject))
            {
                if(usableObject.IsEnable)
                {
                    GameManager.Instance.SceneManager.UIManager.HighlightPointer(true);
                    if (Input.GetButtonDown("ActionButton"))
                    {
                        usableObject.Use();
                    }
                }
                else
                {
                    GameManager.Instance.SceneManager.UIManager.HighlightPointer(false);
                }
            }
            else
            {
                GameManager.Instance.SceneManager.UIManager.HighlightPointer(false);
            }
        }
    }

    public void SetEnable(bool enable)
    {
        serializeFields.Camera.gameObject.SetActive(enable);
    }
}
