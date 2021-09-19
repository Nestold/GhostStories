using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Button returnButton = null;
    [SerializeField]
    private Button returnToMenu = null;

    public void SetEnable(bool value)
    {
        GameManager.Instance.SetCursorState(value);
        GameManager.Instance.SceneManager.Freeze = value;
        gameObject.SetActive(value);
        GameManager.Instance.GameTime(!value);
    }

    private void Start()
    {
        returnButton.onClick.AddListener(() => SetEnable(false));
        returnToMenu.onClick.AddListener(() => OnReturnToMenuClick());
    }

    private void OnReturnToMenuClick()
    {
        GameManager.Instance.BackToMainMenu();
    }
}
