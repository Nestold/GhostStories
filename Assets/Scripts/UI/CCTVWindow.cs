using UnityEngine;
using UnityEngine.UI;

public class CCTVWindow : BaseWindow
{
    [SerializeField]
    private Text cameraLabel = null;
    [SerializeField]
    private Button leftArrow = null;
    [SerializeField]
    private Button rightArrow = null;

    public override void Setup()
    {
        base.Setup();
        var sMan = GameManager.Instance.SceneManager;
        leftArrow.onClick.AddListener(() => sMan.PrevCamera());
        rightArrow.onClick.AddListener(() => sMan.NextCamera());
        sMan.OnCameraChange += OncameraChange;
    }

    private void OncameraChange(ERoomType room)
    {
        cameraLabel.text = $"Camera_{Consts.GetRoomName(room)}";
    }
}
