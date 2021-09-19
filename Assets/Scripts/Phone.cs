using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : UsableObject
{
    [SerializeField]
    private AudioSource ringing = null;

    [SerializeField]
    private AudioSource pickup = null;

    public override void Use()
    {
        var sMan = GameManager.Instance.SceneManager;
        if (IsEnable)
        {
            ringing.Stop();
            sMan.SetDialog((int)GameManager.Instance.SelectedMapType, new System.Action(() => sMan.StartAberration()));
            pickup.Play();
            IsEnable = false;
        }
    }
}
