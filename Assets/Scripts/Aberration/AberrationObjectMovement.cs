using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AberrationObjectMovement : Aberration
{
    [SerializeField]
    private GameObject secondAberrationObject = null;

    public override void OnAberrationAppear()
    {
        base.OnAberrationAppear();

        aberrationObject.SetActive(false);
        secondAberrationObject.SetActive(true);
    }

    public override void OnAberrationFixed()
    {
        base.OnAberrationFixed();

        aberrationObject.SetActive(true);
        secondAberrationObject.SetActive(false);
    }
}
