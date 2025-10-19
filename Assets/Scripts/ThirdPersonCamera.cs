using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;         // jugador a seguir
    public float distance = 7f;      // distancia detr�s del jugador
    public float height = 5f;        // altura de la c�mara
    public float mouseSensitivity = 3f; // sensibilidad del mouse
    public float smoothSpeed = 5f;   // suavizado de la c�mara

    private float currentYaw = 0f;   // rotaci�n horizontal

    void LateUpdate()
    {
        if (player == null) return;

        // Obtener movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        currentYaw += mouseX;

        // Rotar jugador hacia la direcci�n de la c�mara
        player.rotation = Quaternion.Euler(0, currentYaw, 0);

        // Posici�n deseada de la c�mara
        Vector3 offset = new Vector3(0, height, -distance);
        Quaternion rotation = Quaternion.Euler(0, currentYaw, 0);
        Vector3 desiredPosition = player.position + rotation * offset;

        // Movimiento suave de la c�mara
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // La c�mara siempre mira al jugador
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}


