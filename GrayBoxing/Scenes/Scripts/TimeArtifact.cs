using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeArtifact : MonoBehaviour
{
    [Header("Configuración de Tiempo")]
    [SerializeField] private float slowMotionFactor = 0.5f; // Mitad de la velocidad normal
    [SerializeField] private float fastMotionFactor = 2.0f; // Doble de la velocidad normal

    private bool isHeld = false;
    private float defaultFixedDeltaTime;

    void Awake()
    {
        // Guardamos el valor por defecto de FixedDeltaTime para poder restaurarlo
        this.defaultFixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        // Solo podemos controlar el tiempo si tenemos el objeto en la mano
        if (!isHeld) return;

        // Ralentizar tiempo
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetTimeScale(slowMotionFactor);
            Debug.Log("Time slowed down!");
        }

        // Acelerar tiempo
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetTimeScale(fastMotionFactor);
            Debug.Log("Time sped up!");
        }

        // Resetear el tiempo si soltamos las teclas (Opcional, pero recomendado)
        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E))
        {
            ResetTimeScale();
            Debug.Log("Time reset to normal.");
        }
    }

    // Esta función es llamada por PickUpObject.cs cuando recogemos el objeto
    public void OnPickup()
    {
        isHeld = true;
    }

    // Esta función es llamada por PickUpObject.cs cuando soltamos el objeto
    public void OnDrop()
    {
        isHeld = false;
        // Siempre reseteamos el tiempo al soltar el objeto para no dejar el juego alterado
        ResetTimeScale();
    }

    private void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
        // Ajustamos fixedDeltaTime para que la física siga siendo consistente
        Time.fixedDeltaTime = this.defaultFixedDeltaTime * Time.timeScale;
    }

    private void ResetTimeScale()
    {
        SetTimeScale(1.0f);
    }
}


