using UnityEngine;

[RequireComponent(typeof(PlayerSerializeFields))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private bool canJump = true;

    [SerializeField]
    private LayerMask groundMask = 1 << 0;
    [SerializeField]
    private float mouseSensivity = 1f;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private PlayerSerializeFields serializeFields;
    private Vector3 playerVelocity;
    private float xRotation = 0f;
    private bool isGrounded = false;

    private void Awake()
    {
        serializeFields = GetComponent<PlayerSerializeFields>();
    }

    private void Update()
    {
        if (GameManager.Instance.SceneManager.CurrentPlayerState == EPlayerState.FPP && !GameManager.Instance.SceneManager.Freeze)
        {
            isGrounded = Physics.CheckSphere(serializeFields.BottomCheck.position, 0.1f, groundMask);

            if (isGrounded && playerVelocity.y < 0 ||
                Physics.CheckSphere(serializeFields.TopCheck.position, 0.1f, groundMask))
            {
                playerVelocity.y = -2f;
            }

            if (Input.GetButton("Jump") && isGrounded && canJump)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            }

            var direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            var look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSensivity;

            xRotation -= look.y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            var move = transform.right * direction.x + transform.forward * direction.z;
            playerVelocity.y += gravityValue * Time.deltaTime;

            transform.Rotate(Vector3.up * look.x);
            serializeFields.Camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            serializeFields.CharacterController.Move(move * playerSpeed * Time.deltaTime);
            serializeFields.CharacterController.Move(playerVelocity * Time.deltaTime);

            if (serializeFields.CharacterController.isGrounded == true && move != Vector3.zero && serializeFields.FootstepsAudio.isPlaying == false)
            {
                serializeFields.FootstepsAudio.volume = Random.Range(.1f, .4f);
                serializeFields.FootstepsAudio.Play();
            }
            if(move == Vector3.zero)
            {
                serializeFields.FootstepsAudio.Stop();
            }
        }
    }
}
