using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class CameraController : MonoBehaviour
{
    public float thrustForce = 10f;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 90f;

    private Rigidbody rb;
    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Configuración del Rigidbody para el espacio
        rb.useGravity = false;
        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Rotación con el mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        Quaternion localPitch = Quaternion.Euler(xRotation, 0f, 0f); // rotación vertical
        Quaternion yaw = Quaternion.Euler(0f, transform.eulerAngles.y + mouseX, 0f); // rotación horizontal acumulativa

        transform.rotation = yaw * localPitch;
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Jump") - (Input.GetKey(KeyCode.LeftControl) ? 1f : 0f);
        float moveZ = Input.GetAxis("Vertical");

        Vector3 thrust = new Vector3(moveX, moveY, moveZ) * thrustForce;
        rb.AddRelativeForce(thrust, ForceMode.Acceleration);
    }

    void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
