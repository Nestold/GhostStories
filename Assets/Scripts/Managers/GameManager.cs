using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManger = UnityEngine.SceneManagement.SceneManager;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField]
    private Animator sceneLoaderAnimator = null;

    public EMapType SelectedMapType { get; set; }

    public SceneManager SceneManager
    {
        get => FindObjectOfType(typeof(SceneManager)) as SceneManager;
    }

    public MainMenuManager MainMenuManager
    {
        get => FindObjectOfType(typeof(MainMenuManager)) as MainMenuManager;
    }

    public void BackToMainMenu()
    {
        StartCoroutine(WaitToLoadScene(0));
    }

    public void TryAgain()
    {
        StartCoroutine(WaitToLoadScene(SelectedMapType));
    }
    public void NewGame()
    {
        StartCoroutine(WaitToLoadScene(EMapType.KellyStreet));
    }
    public void NextMap()
    {
        var sceneIndex = (int)SelectedMapType + 1;
        StartCoroutine(WaitToLoadScene((EMapType)(sceneIndex)));
    }

    public void SetCursorState(bool isEnable)
    {
        Cursor.visible = isEnable;
        Cursor.lockState = isEnable ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void GameTime(bool start)
    {
        Time.timeScale = start ? 1 : 0;
    }

    private IEnumerator WaitToLoadScene(EMapType mapType)
    {
        GameTime(true);
        sceneLoaderAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        AsyncOperation asyncLoadLevel = UnitySceneManger.LoadSceneAsync((int)mapType, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone)
            yield return null;

        yield return new WaitForEndOfFrame();

        sceneLoaderAnimator.SetTrigger("End");
        OnSceneLoad(mapType);
    }

    public void OnSceneLoad(EMapType mapType)
    {
        switch (mapType)
        {
            case EMapType.MainStreet:
            case EMapType.CecilStreet:
            case EMapType.KellyStreet:
            case EMapType.NevilleStreet:
                SceneManager.Setup();
                break;
        }
        GameTime(true);
    }

    public int LoadSavedSceneIndex()
    {
        return 0;
    }
}