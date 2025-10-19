using UnityEngine;

public class KeyItem : MonoBehaviour
{
    [Header("Configuración de la llave")]
    public string keyName = "Llave";

    private bool isCollected = false;

    void OnMouseDown()
    {
        if (isCollected) return;
        CollectKey();
    }

    void CollectKey()
    {
        isCollected = true;

        // Guardar la llave en el gestor
        KeyManager.Instance.AddKey(this);

        // Mostrar mensaje en consola para probar
        Debug.Log($"[TEST] ¡Has recogido la llave: {keyName}!");

        // Mostrar mensaje en pantalla (UIMessage)
        UIMessage.Instance.ShowMessage($"{keyName} obtenida ");

        // Desactivar la llave
        gameObject.SetActive(false);
    }
}