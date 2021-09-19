public class CameraManagement : UsableObject
{
    public override void Use()
    {
        if (GameManager.Instance.SceneManager.IsActiveAberrationsCooldown)
        {
            GameManager.Instance.SceneManager.CurrentPlayerState = EPlayerState.WindowsDisplay;
        }
    }
}
