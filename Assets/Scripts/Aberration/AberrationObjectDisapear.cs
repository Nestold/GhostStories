using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AberrationObjectDisapear : Aberration
{
    public override void OnAberrationAppear()
    {
        base.OnAberrationAppear();
        aberrationObject.gameObject.SetActive(false);
    }

    public override void OnAberrationFixed()
    {
        base.OnAberrationFixed();
        aberrationObject.gameObject.SetActive(true);
    }
}