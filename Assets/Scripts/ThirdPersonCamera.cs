using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;         // jugador a seguir
    public float distance = 7f;      // distancia detrás del jugador
    public float height = 5f;        // altura de la cámara
    public float mouseSensitivity = 3f; // sensibilidad del mouse
    public float smoothSpeed = 5f;   // suavizado de la cámara

    private float currentYaw = 0f;   // rotación horizontal

    void LateUpdate()
    {
        if (player == null) return;

        // Obtener movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        currentYaw += mouseX;

        // Rotar jugador hacia la dirección de la cámara
        player.rotation = Quaternion.Euler(0, currentYaw, 0);

        // Posición deseada de la cámara
        Vector3 offset = new Vector3(0, height, -distance);
        Quaternion rotation = Quaternion.Euler(0, currentYaw, 0);
        Vector3 desiredPosition = player.position + rotation * offset;

        // Movimiento suave de la cámara
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // La cámara siempre mira al jugador
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}


