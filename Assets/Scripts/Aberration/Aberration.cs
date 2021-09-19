using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aberration : MonoBehaviour
{
    public int AberrationWeight => aberrationWeight;
    public ERoomType AberrationPlace => aberrationPlace;
    public EAberrationType AberrationType => aberrationType;
    public bool IsActive { get; private set; }

    [SerializeField]
    private int aberrationWeight = 20;
    [SerializeField]
    private ERoomType aberrationPlace = ERoomType.None;
    [SerializeField]
    private EAberrationType aberrationType = EAberrationType.None;
    [SerializeField]
    protected GameObject aberrationObject = null;

    public virtual void Setup()
    {

    }

    public virtual void OnAberrationAppear()
    {
        Debug.Log($"Aberration {aberrationType}, in {aberrationPlace} room, on object {aberrationObject.name} appear.");
        IsActive = true;
    }

    public virtual void OnAberrationFixed()
    {
        IsActive = false;
    }
}
