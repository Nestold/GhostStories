using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVCamera : MonoBehaviour
{
    public bool IsBroken
    {
        get
        {
            return GetComponent<AberrationBrokenCamera>().IsBroken;
        }
    }

    public ERoomType RoomType => roomType;

    [SerializeField]
    private ERoomType roomType = ERoomType.None;

    [SerializeField]
    private new Camera camera = null;

    public void SetEnable(bool enable)
    {
        camera.gameObject.SetActive(enable);
    }

    public void Setup()
    {
        SetEnable(false);
    }
}
