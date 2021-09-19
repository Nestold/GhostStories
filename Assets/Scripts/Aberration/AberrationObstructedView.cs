using UnityEngine;

public class AberrationObstructedView : Aberration
{
    [SerializeField]
    private Light light = null;

    public override void Setup()
    {
        base.Setup();
        OnAberrationFixed();
    }

    public override void OnAberrationAppear()
    {
        base.OnAberrationAppear();
        aberrationObject.SetActive(true);
        light.enabled = false;
    }

    public override void OnAberrationFixed()
    {
        base.OnAberrationFixed();
        aberrationObject.SetActive(false);
        light.enabled = true;
    }
}