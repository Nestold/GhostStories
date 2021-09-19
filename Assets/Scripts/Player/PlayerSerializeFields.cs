using UnityEngine;

public class PlayerSerializeFields : MonoBehaviour
{
    public CharacterController CharacterController => characterController;

    public Transform Camera => camera;

    public Transform TopCheck => topCheck;

    public Transform BottomCheck => bottomCheck;

    public AudioSource FootstepsAudio => footstepsAudio;

    [SerializeField]
    private CharacterController characterController = null;

    [SerializeField]
    private new Transform camera = null;

    [SerializeField]
    private Transform topCheck = null;

    [SerializeField]
    private Transform bottomCheck = null;

    [SerializeField]
    private AudioSource footstepsAudio = null;
}
