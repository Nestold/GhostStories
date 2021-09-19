using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private Button playButton = null;

    [SerializeField]
    private Button settingsButton = null;

    [SerializeField]
    private Button quitButton = null;

    [SerializeField]
    private Button continueButton = null;

    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        playButton.onClick.AddListener(() => OnPlayButtonClick());
        settingsButton.onClick.AddListener(() => OnSettingsButtonClick());
        quitButton.onClick.AddListener(() => OnQuitClick());
        continueButton.onClick.AddListener(() => OnContinueClick());
    }

    private void OnQuitClick()
    {
        Application.Quit();
    }

    private void OnContinueClick()
    {

    }

    private void OnSettingsButtonClick()
    {

    }

    private void OnPlayButtonClick()
    {
        GameManager.Instance.NewGame();
    }
}
