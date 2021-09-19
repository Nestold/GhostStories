using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField]
    private Text descriptions = null;

    [SerializeField]
    private Text activeAberrations = null;

    [SerializeField]
    private Text fixedAberrations = null;

    [SerializeField]
    private Button tryAgainbutton = null;

    [SerializeField]
    private Button backToMenu = null;

    [SerializeField]
    private Button nextMapButton = null;


    public void Setup()
    {
        tryAgainbutton.onClick.AddListener(() => OnTryAgainClick());
        backToMenu.onClick.AddListener(() => OnBackToMenuClick());
        nextMapButton.onClick.AddListener(() => OnNextMapClick());
        gameObject.SetActive(false);
    }

    public void ShowEndPanel(bool iswin, EMapType endedMap)
    {
        GameManager.Instance.SceneManager.UIManager.HideAberrationBreaker();
        descriptions.text = iswin ?
            "Congratulations. You win." :
            "Too many aberrations. You have lost.";
        activeAberrations.text = GameManager.Instance.SceneManager.ActiveAberrationsCount.ToString();
        fixedAberrations.text = GameManager.Instance.SceneManager.FixedAberrations.ToString();
        nextMapButton.gameObject.SetActive(iswin && endedMap != EMapType.CecilStreet);
        gameObject.SetActive(true);
    }

    private void OnNextMapClick()
    {
        GameManager.Instance.GameTime(true);
        GameManager.Instance.NextMap();
    }

    private void OnTryAgainClick()
    {
        GameManager.Instance.GameTime(true);
        GameManager.Instance.TryAgain();
    }

    private void OnBackToMenuClick()
    {
        GameManager.Instance.GameTime(true);
        GameManager.Instance.BackToMainMenu();
    }
}
