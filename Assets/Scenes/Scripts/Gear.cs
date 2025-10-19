using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gear : MonoBehaviour
{
    [Header("Configuraci�n")]
    public bool isPlaced = false;
    public Transform slot;
    public float rotationSpeed = 60f; // grados por segundo
    public string gearTag = "Gear";

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        // Si est� colocado, mantener exactamente en el slot (sin fisicas movi�ndolo)
        if (isPlaced && slot != null)
        {
            // fijar transform directamente para evitar drift
            transform.position = slot.position;
            transform.rotation = slot.rotation;
            // hacemos kinematic para evitar que la f�sica lo mueva
            if (!rb.isKinematic) rb.isKinematic = true;
        }
    }

    // Llamado por el GearManager para girar (si est� colocado)
    public void Rotate(bool clockwise)
    {
        if (!isPlaced) return; // solo rotar si est� colocado
        float dir = clockwise ? -1f : 1f; // invertir para que concuerde con la convenci�n
        transform.Rotate(Vector3.up * rotationSpeed * dir * Time.deltaTime, Space.Self);
    }

    // M�todo que usa GearSlot cuando se coloca
    public void PlaceAt(Transform targetSlot)
    {
        slot = targetSlot;
        isPlaced = true;

        // Posici�n exacta
        transform.position = slot.position;

        // Rotaci�n: copiar la del slot
        transform.rotation = slot.rotation;

        // --- OFFSET DE ROTACI�N AQU� ---
        // Por ejemplo, si tu engranaje tiene el eje horizontal en X y debe girar vertical
        transform.rotation *= Quaternion.Euler(90f, 0f, 0f);
        // Cambia los valores seg�n tu modelo (X, Y, Z)

        // Bloquear f�sica
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }




    public void Unplace()
    {
        isPlaced = false;
        slot = null;
        rb.isKinematic = false;
    }

    // Debug gizmo para ver el eje
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.15f);
    }
}
