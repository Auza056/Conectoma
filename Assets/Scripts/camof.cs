using UnityEngine;

public class CameraAndPlayerRotation : MonoBehaviour
{
    public Transform player;           // Tu Player (Steve)
    public Vector3 offset = new Vector3(0, 5, -7);
    public float cameraSmoothSpeed = 5f;
    public float mouseSensitivity = 3f;
    public float moveSpeed = 5f;

    private float yaw = 0f;

    void Start()
    {
        if (player != null)
        {
            // Mantener rotación inicial en X y Z
            player.rotation = Quaternion.Euler(-90f, 0f, 0f);
        }
    }

    void Update()
    {
        if (player == null) return;

        // 1?? Rotación del jugador con mouse (solo Y)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        yaw += mouseX;
        player.rotation = Quaternion.Euler(-90f, yaw, 0f);

        // 2?? Movimiento adelante/atrás usando el eje Z del mundo, rotado por yaw
        float vertical = Input.GetAxis("Vertical"); // W/S
        Vector3 forward = new Vector3(0, 0, 1);     // eje Z
        Vector3 moveDir = Quaternion.Euler(0, yaw, 0) * forward; // solo Y
        player.position += moveDir * vertical * moveSpeed * Time.deltaTime;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // 3?? Cámara sigue al jugador
        Quaternion rotation = Quaternion.Euler(0, yaw, 0);
        Vector3 desiredPosition = player.position + rotation * offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, cameraSmoothSpeed * Time.deltaTime);

        // 4?? Cámara mira al jugador
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}
