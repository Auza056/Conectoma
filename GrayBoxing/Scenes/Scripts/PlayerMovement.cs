using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f; // Altura del salto
    public float mouseSensitivity = 100f;
    public Transform playerCamera;

    // --- NUEVAS VARIABLES PARA EL SALTO Y GROUND CHECK ---
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    // ---

    private float xRotation = 0f;
    private Vector3 velocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // --- COMPROBAR SI ESTÁ EN EL SUELO ---
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Si estamos en el suelo y cayendo, reseteamos la velocidad de caída
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Una pequeña fuerza hacia abajo para mantenerlo pegado
        }

        // --- Movimiento ---
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // --- LÓGICA DE SALTO ---
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // La fórmula física para calcular la velocidad necesaria para alcanzar una altura 'h'
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // --- Gravedad ---
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // --- Cámara ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
