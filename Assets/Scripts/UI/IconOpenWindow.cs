using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconOpenWindow : BaseIcon
{
    [SerializeField]
    private BaseWindow window = null;

    public override void OnClick()
    {
        base.OnClick();
        window.OpenWindow();
    }
}
