using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BaseIconSerializeFields))]
public class BaseIcon : MonoBehaviour
{
    protected BaseIconSerializeFields SerializeFields => GetComponent<BaseIconSerializeFields>();

    public void Setup()
    {
        SerializeFields.Button.onClick.AddListener(() => OnClick());
    }

    public virtual void OnClick()
    {

    }
}