using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class FreeCameraController : MonoBehaviour
{
    public float thrustForce = 10f;
    public float rotationSensitivity = 2f;

    private Rigidbody rb;
    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Configuración del Rigidbody para simulación espacial
        rb.useGravity = false;
        rb.linearDamping = 0f;             // Sin fricción lineal
        rb.angularDamping = 0f;      // Sin fricción angular
        rb.freezeRotation = true; // Evitamos que la física modifique la rotación

        // Bloquear cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Rotación manual de la cámara
        float mouseX = Input.GetAxis("Mouse X") * rotationSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // opcional, para evitar voltear

        transform.rotation *= Quaternion.Euler(-mouseY, mouseX, 0f);
    }

    void FixedUpdate()
    {
        // Movimiento espacial
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Jump") - (Input.GetKey(KeyCode.LeftControl) ? 1 : 0);
        float moveZ = Input.GetAxis("Vertical");

        Vector3 thrust = new Vector3(moveX, moveY, moveZ) * thrustForce;
        rb.AddRelativeForce(thrust, ForceMode.Acceleration);
    }
    public void DisableCamera()
    {
        this.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
