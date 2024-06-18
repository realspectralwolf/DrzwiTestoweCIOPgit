using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] Camera targetCamera;
    [SerializeField] float speed = 5.0f;
    [SerializeField] float mouseSensitivity = 2.0f;
    [SerializeField] float gravity = -9.81f;

    CharacterController characterController;
    Vector3 velocity;
    float verticalLookRotation;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Handle mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        targetCamera.transform.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);

        // Handle movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        characterController.Move(move * speed * Time.deltaTime);

        // Handle gravity
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep the character grounded
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
