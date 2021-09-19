using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableDoor : UsableObject
{
    [SerializeField]
    private bool isClosed = false;
    [SerializeField]
    private Transform doorObject = null;
    [SerializeField]
    private bool isOpen = false;

    [SerializeField]
    private AudioSource openSound = null;
    [SerializeField]
    private AudioSource closeSound = null;

    public override void Use()
    {
        if(!isClosed)
        {
            isOpen = !isOpen;
            if (isOpen)
            {
                openSound.Play();
            }
        }
        else
        {
            closeSound.Play();
        }
        base.Use();
    }

    private void Update()
    {
        if(!isOpen && doorObject.localRotation.eulerAngles.y > 0f)
        {
            doorObject.Rotate(0f, 0f, -.5f);
        }
        else if(isOpen && doorObject.localRotation.eulerAngles.y < 90f)
        {
            doorObject.Rotate(0f, 0f, .5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);
    }
}
