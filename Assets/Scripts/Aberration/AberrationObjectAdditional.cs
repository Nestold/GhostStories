public class AberrationObjectAdditional : Aberration
{
    public override void Setup()
    {
        base.Setup();
        gameObject.SetActive(false);
    }

    public override void OnAberrationAppear()
    {
        base.OnAberrationAppear();
        aberrationObject.SetActive(true);
    }

    public override void OnAberrationFixed()
    {
        base.OnAberrationFixed();
        aberrationObject.SetActive(false);
    }
}
