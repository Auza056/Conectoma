using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GearSlot : MonoBehaviour
{
    public bool occupied = false;
    public Gear currentGear;
    public Transform snapPoint; // si quieres un punto de snap hijo para alinear mejor

    void Reset()
    {
        // asegurar que el collider sea trigger
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (occupied) return;

        Gear gear = other.GetComponent<Gear>();
        if (gear != null && !gear.isPlaced)
        {
            // colocar y bloquear
            Transform target = snapPoint != null ? snapPoint : this.transform;
            gear.PlaceAt(target);
            currentGear = gear;
            occupied = true;
            Debug.Log($"Gear placed in slot {name} -> {gear.name}");
        }
    }

    // Si permites sacar engranajes, OnTriggerExit no es suficiente si lo haces via código de pickup.
    // Provee una función para liberar el slot:
    public void FreeSlot()
    {
        if (currentGear != null)
        {
            currentGear.Unplace();
            currentGear = null;
            occupied = false;
            Debug.Log($"Slot {name} freed");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = occupied ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
        if (snapPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, snapPoint.position);
        }
    }
}
