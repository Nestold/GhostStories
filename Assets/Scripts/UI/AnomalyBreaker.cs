using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnomalyBreaker : MonoBehaviour
{
    [SerializeField]
    private Text anomalyBreakerText = null;

    [SerializeField]
    private AudioSource fixedAberration = null;
    [SerializeField]
    private AudioSource aberrationNotFound = null;

    public void SetText(bool isFixed)
    {
        if (isFixed)
        {
            anomalyBreakerText.text = "Aberration fixed.";
            fixedAberration.Play();
        }
        else
        {
            anomalyBreakerText.text = $"Aberration not found.";
            aberrationNotFound.Play();
        }
    }

    public void SetEnable(bool value)
    {
        gameObject.SetActive(value);
    }

    public void OnAnomalyBreak()
    {
        SetEnable(false);
    }
}
