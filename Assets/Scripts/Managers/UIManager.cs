using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas UICanvas => uiCanvas;

    [SerializeField]
    private UIFPPManager fppManager = null;

    [SerializeField]
    private Text timerDisplay = null;

    [SerializeField]
    private PauseMenu pauseMenu = null;

    [SerializeField]
    private GameOverPanel gameOverPanel = null;

    [SerializeField]
    private AnomalyBreaker aberrationBreaker = null;

    [SerializeField]
    private AberrationsWindow aberrationsWindow = null;

    [SerializeField]
    private DialogPanel dialogPanel = null;

    [SerializeField]
    private Canvas uiCanvas = null;

    public void Setup()
    {
        GameManager.Instance.SceneManager.OnTimerChange -= OnTimerChange;
        GameManager.Instance.SceneManager.OnTimerChange += OnTimerChange;
        gameOverPanel.Setup();
    }

    public void ShowEndPanel(bool isWin, EMapType endedMap)
    {
        GameManager.Instance.SetCursorState(true);
        GameManager.Instance.GameTime(false);
        gameOverPanel.ShowEndPanel(isWin, endedMap);
    }

    public void ShowPausePanel()
    {
        pauseMenu.SetEnable(true);
    }

    private void OnTimerChange(float timer)
    {
        timerDisplay.text = $"{Mathf.FloorToInt(timer / 60).ToString("00")}:{Mathf.FloorToInt(timer % 60).ToString("00")} 19.08.2021";
    }

    public void ShowAberrationBareaker(bool isFixed)
    {
        if(GameManager.Instance.SceneManager.CurrentPlayerState == EPlayerState.WindowsDisplay)
        {
            aberrationBreaker.SetEnable(true);
            aberrationBreaker.SetText(isFixed);
        }
        aberrationsWindow.SetEnable(true);
    }

    public void SetDialogText(string dialog)
    {
        dialogPanel.SetDialogText(dialog);
    }

    public void HideAberrationBreaker()
    {
        aberrationBreaker.SetEnable(false);
    }

    public void HighlightPointer(bool highlight)
    {
        fppManager.SetPointer(highlight);
    }
}