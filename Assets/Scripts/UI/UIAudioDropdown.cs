using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UIAudioDropdown : Dropdown
{
    protected override void Start()
    {
        base.Start();
        onValueChanged.AddListener(delegate { GetComponent<AudioSource>().Play(); });
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        GetComponent<AudioSource>().Play();
    }
}