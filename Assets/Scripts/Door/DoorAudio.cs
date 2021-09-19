using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAudio : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"OnTriggerExit: {other.name}");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log($"OnCollisionExit: {collision.gameObject.name}");
    }
}
