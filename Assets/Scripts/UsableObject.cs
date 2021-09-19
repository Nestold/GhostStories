using UnityEngine;

public class UsableObject : MonoBehaviour
{
    public bool IsEnable { get; set; } = true;

    public virtual void Use()
    {

    }
}
