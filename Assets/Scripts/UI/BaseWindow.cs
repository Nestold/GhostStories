using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(BaseWindowSerializeFields))]
public class BaseWindow : MonoBehaviour
{
    protected BaseWindowSerializeFields SerializeFields => GetComponent<BaseWindowSerializeFields>();

    public virtual void Setup()
    {
        SerializeFields.CancelButton.onClick.AddListener(() => OnCancel());
        CloseWindow();
    }

    public void OpenWindow()
    {
        if (!gameObject.activeSelf)
        {
            SerializeFields.Window.gameObject.SetActive(true);
        }
        SerializeFields.Window.SetAsLastSibling();
    }

    public void CloseWindow()
    {
        SerializeFields.Window.gameObject.SetActive(false);
    }

    public virtual void OnCancel()
    {
        CloseWindow();
    }
}