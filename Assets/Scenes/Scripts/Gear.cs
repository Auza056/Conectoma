using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gear : MonoBehaviour
{
    [Header("Configuración")]
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
        // Si está colocado, mantener exactamente en el slot (sin fisicas moviéndolo)
        if (isPlaced && slot != null)
        {
            // fijar transform directamente para evitar drift
            transform.position = slot.position;
            transform.rotation = slot.rotation;
            // hacemos kinematic para evitar que la física lo mueva
            if (!rb.isKinematic) rb.isKinematic = true;
        }
    }

    // Llamado por el GearManager para girar (si está colocado)
    public void Rotate(bool clockwise)
    {
        if (!isPlaced) return; // solo rotar si está colocado
        float dir = clockwise ? -1f : 1f; // invertir para que concuerde con la convención
        transform.Rotate(Vector3.up * rotationSpeed * dir * Time.deltaTime, Space.Self);
    }

    // Método que usa GearSlot cuando se coloca
    public void PlaceAt(Transform targetSlot)
    {
        slot = targetSlot;
        isPlaced = true;

        // Posición exacta
        transform.position = slot.position;

        // Rotación: copiar la del slot
        transform.rotation = slot.rotation;

        // --- OFFSET DE ROTACIÓN AQUÍ ---
        // Por ejemplo, si tu engranaje tiene el eje horizontal en X y debe girar vertical
        transform.rotation *= Quaternion.Euler(90f, 0f, 0f);
        // Cambia los valores según tu modelo (X, Y, Z)

        // Bloquear física
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
