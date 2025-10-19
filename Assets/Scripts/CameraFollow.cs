using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // jugador
    public Vector3 offset = new Vector3(0, 5, -7); // posición relativa al jugador
    public float smoothSpeed = 5f;    // suavizado del movimiento
    public float rotationSpeed = 100f; // velocidad de rotación con mouse

    void LateUpdate()
    {
        if (target == null) return;

        // Movimiento suave de la cámara
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Rotación con mouse
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        target.Rotate(Vector3.up * horizontal);

        transform.LookAt(target.position + Vector3.up * 1.5f); // mira hacia la cabeza del jugador
    }
}

