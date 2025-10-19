using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public static DoorController Instance { get; private set; }

    [Header("Configuración de la puerta")]
    public int boxesToUnlock = 3; // cuántas cajas deben abrirse
    private int boxesOpened = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void NotifyBoxOpened()
    {
        boxesOpened++;
        Debug.Log($"[DOOR] Cajas abiertas: {boxesOpened}/{boxesToUnlock}");

        if (boxesOpened >= boxesToUnlock)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        Debug.Log("[DOOR]Todas las cajas desbloqueadas. ¡La puerta se abre!");
        StartCoroutine(ShowDoorMessage());
        gameObject.SetActive(false);
    }

    IEnumerator ShowDoorMessage()
    {
        // Esperar 0.5 segundos para no chocar con el mensaje de la caja
        yield return new WaitForSeconds(1.0f);
        UIMessage.Instance.ShowMessage("¡La puerta se ha desbloqueado! ");
    }
}
