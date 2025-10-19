using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [Tooltip("El punto de inicio del recorrido.")]
    [SerializeField] private Transform pointA; // Arrastra aquí tu objeto "PuntoA"
    [Tooltip("El punto final del recorrido.")]
    [SerializeField] private Transform pointB; // Arrastra aquí tu objeto "PuntoB"
    [SerializeField] private float speed = 2f;

    private Vector3 targetPosition;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        // La plataforma empieza en la posición del Punto A
        transform.position = pointA.position;

        // Y su primer objetivo es el Punto B
        targetPosition = pointB.position;
    }

    void FixedUpdate()
    {
        // Movemos la plataforma hacia el punto objetivo
        Vector3 newPos = Vector3.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        // Si llegamos al destino, cambiamos el objetivo
        if (Vector3.Distance(rb.position, targetPosition) < 0.01f)
        {
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }
    }

    // El código para que el jugador se mueva con la plataforma sigue igual
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
