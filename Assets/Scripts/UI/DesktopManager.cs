using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopManager : MonoBehaviour
{
    [SerializeField]
    private GameObject desktopPanel = null;

    [SerializeField]
    private Transform windowsParent = null;
    [SerializeField]
    private Transform baseIconsParent = null;

    public void Setup()
    {
        foreach(Transform w in windowsParent)
        {
            BaseWindow baseWindow;
            if(w.TryGetComponent(out baseWindow))
            {
                baseWindow.Setup();
            }
        }
        foreach(Transform b in baseIconsParent)
        {
            BaseIcon baseIcon;
            if(b.TryGetComponent(out baseIcon))
            {
                baseIcon.Setup();
            }
        }
    }

    public void SetEnable(bool isEnable)
    {
        desktopPanel.SetActive(isEnable);
    }
}
