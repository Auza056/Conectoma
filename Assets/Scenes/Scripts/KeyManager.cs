using UnityEngine;
using System.Collections.Generic;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance { get; private set; }

    private List<string> collectedKeys = new List<string>();

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void AddKey(KeyItem key)
    {
        if (!collectedKeys.Contains(key.keyName))
        {
            collectedKeys.Add(key.keyName);
            Debug.Log($"[KEY] {key.keyName} añadida. Total: {collectedKeys.Count}");

            if (collectedKeys.Count == 3)
                Debug.Log("? Has conseguido las 3 llaves. ¡Compuerta lista para abrir!");
        }
    }

    // ?? Este método permite verificar si ya tienes una llave
    public bool HasKey(string keyName)
    {
        return collectedKeys.Contains(keyName);
    }

    public int KeyCount => collectedKeys.Count;
}
