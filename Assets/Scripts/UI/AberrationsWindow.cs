using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AberrationsWindow : BaseWindow
{
    [SerializeField]
    private UIAudioDropdown roomDropdown = null;

    [SerializeField]
    private UIAudioDropdown aberrationDropdown = null;

    [SerializeField]
    private Button reportButton = null;

    public override void Setup()
    {
        base.Setup();

        string[] roomNames = Consts.GetRoomNames();
        string[] aberrationsNames = Consts.GetAberrationsNames();

        roomDropdown.AddOptions(new List<string>(roomNames));
        aberrationDropdown.AddOptions(new List<string>(aberrationsNames));

        reportButton.onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        SetEnable(false);
        GameManager.Instance.SceneManager.ReportAnomaly((ERoomType)roomDropdown.value, (EAberrationType)aberrationDropdown.value);
    }

    public void SetEnable(bool value)
    {
        reportButton.interactable = value;
        aberrationDropdown.interactable = value;
        roomDropdown.interactable = value;
    }
}
