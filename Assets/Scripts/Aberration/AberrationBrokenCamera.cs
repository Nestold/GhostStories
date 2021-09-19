public class AberrationBrokenCamera : Aberration
{
    public bool IsBroken { get; private set; } = false;

    public override void OnAberrationAppear()
    {
        base.OnAberrationAppear();
        IsBroken = true;
    }

    public override void OnAberrationFixed()
    {
        base.OnAberrationFixed();
        IsBroken = false;
    }
}
