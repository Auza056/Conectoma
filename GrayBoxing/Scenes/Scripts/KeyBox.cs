using UnityEngine;

public class KeyBox : MonoBehaviour
{
    [Header("Configuración de la caja")]
    public string requiredKeyName = "Llave Dorada"; // la llave que necesita
    private bool isOpened = false;

    void OnMouseDown()
    {
        if (isOpened) return; // si ya se abrió, no hacer nada
        TryOpenBox();
    }

    void TryOpenBox()
    {
        if (KeyManager.Instance.HasKey(requiredKeyName))
        {
            isOpened = true;

            // Mensaje en consola
            Debug.Log($"[BOX] Caja abierta, tenías la : {requiredKeyName}");

            // ? Avisar al DoorController que una caja fue abierta
            if (DoorController.Instance != null)
            {
                DoorController.Instance.NotifyBoxOpened();
            }
            else
            {
                Debug.LogWarning("[BOX] No hay DoorController en la escena.");
            }

            // Por ahora, simplemente ocultar el objeto
            gameObject.SetActive(false);

            // Mensaje en UI (si funciona)
            UIMessage.Instance.ShowMessage($"Caja abierta con {requiredKeyName}");
        }
        else
        {
            // Mensaje en consola
            Debug.Log($"[BOX] No tienes la  {requiredKeyName}, no se puede abrir");

            // Mensaje en UI (si funciona)
            UIMessage.Instance.ShowMessage($"No tienes la {requiredKeyName} ");
        }
    }
}
