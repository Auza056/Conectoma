using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public Transform playerCamera;
    public Transform holdPoint;
    public float pickUpRange = 3f;
    public float holdSmoothness = 15f;
    public float throwForce = 5f;

    private Rigidbody heldRb;
    private GameObject heldObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null)
                TryPickUpObject();
            else
                DropObject();
        }

        if (Input.GetMouseButtonDown(1) && heldObject != null)
            ThrowObject();
    }

    // CAMBIO: Movimos toda la lógica de FixedUpdate a LateUpdate
    // para que se ejecute después del movimiento del jugador, eliminando el temblor.
    void LateUpdate()
    {
        if (heldObject != null)
            MoveHeldObject();
    }

    void TryPickUpObject()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickUpRange))
        {
            if (hit.rigidbody != null)
            {
                heldObject = hit.collider.gameObject;
                heldRb = heldObject.GetComponent<Rigidbody>();
                heldRb.useGravity = false;
                heldRb.drag = 10f;
                heldRb.angularDrag = 10f;
                heldRb.constraints = RigidbodyConstraints.FreezeRotation;
                heldRb.interpolation = RigidbodyInterpolation.Interpolate;

                // --- NUEVO: Notificar al script TimeArtifact que fue recogido ---
                TimeArtifact timeArtifact = heldObject.GetComponent<TimeArtifact>();
                if (timeArtifact != null)
                {
                    timeArtifact.OnPickup(); // Llama a la función OnPickup
                }
            }
        }
    }

    void MoveHeldObject()
    {
        TimeArtifact timeArtifact = heldObject.GetComponent<TimeArtifact>();

        if (timeArtifact != null)
        {
            heldRb.velocity = Vector3.zero;
            Vector3 offset = new Vector3(0.5f, -0.4f, 1.4f);
            Vector3 targetLocalPos = playerCamera.TransformPoint(offset);
            float smoothSpeed = 15f;
            heldObject.transform.position = Vector3.Lerp(
                heldObject.transform.position,
                targetLocalPos,
                Time.deltaTime * smoothSpeed
            );
            Quaternion rotationOffset = Quaternion.Euler(90f, 180f, 0f);
            Quaternion targetRotation = playerCamera.rotation * rotationOffset;
            heldObject.transform.rotation = Quaternion.Lerp(
                heldObject.transform.rotation,
                targetRotation,
                Time.deltaTime * smoothSpeed
            );
        }
        else
        {
            Vector3 targetPos = holdPoint.position;
            Vector3 moveDir = targetPos - heldObject.transform.position;
            heldRb.AddForce(moveDir * holdSmoothness, ForceMode.VelocityChange);
            if (moveDir.magnitude < 0.05f)
                heldRb.velocity = Vector3.zero;

            Gear gearComponent = heldObject.GetComponent<Gear>();
            if (gearComponent != null)
            {
                Quaternion targetRot = Quaternion.Euler(90f, playerCamera.eulerAngles.y, 0f);
                heldObject.transform.rotation = Quaternion.Slerp(heldObject.transform.rotation, targetRot, Time.deltaTime * holdSmoothness);
            }
        }
    }

    void DropObject()
    {
        if (heldRb == null) return;

        // --- NUEVO: Notificar al script TimeArtifact que fue soltado ---
        TimeArtifact timeArtifact = heldObject.GetComponent<TimeArtifact>();
        if (timeArtifact != null)
        {
            timeArtifact.OnDrop(); // Llama a la función OnDrop y resetea el tiempo
        }

        heldRb.useGravity = true;
        heldRb.drag = 1f;
        heldRb.angularDrag = 0.05f;
        heldRb.constraints = RigidbodyConstraints.None;
        heldRb.interpolation = RigidbodyInterpolation.None;
        heldRb = null;
        heldObject = null;
    }

    void ThrowObject()
    {
        if (heldRb == null) return;
        Rigidbody rb = heldRb;
        DropObject();
        rb.AddForce(playerCamera.forward * throwForce, ForceMode.Impulse);
    }
}
