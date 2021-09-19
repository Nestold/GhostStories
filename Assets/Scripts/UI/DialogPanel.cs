using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanel : MonoBehaviour
{
    [SerializeField]
    private Text dialogText = null;

    [SerializeField]
    private AudioSource audioSource = null;

    [SerializeField]
    private GameObject tipText = null;

    public void SetDialogText(string text)
    {
        dialogText.text = text;
        if (text.Length > 0)
        {
            tipText.SetActive(true);
            audioSource.Play();
        }
        else
        {
            tipText.SetActive(false);
        }
    }
}
