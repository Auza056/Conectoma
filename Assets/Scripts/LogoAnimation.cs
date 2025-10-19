using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoAnimation : MonoBehaviour
{
    public float duration = 2f; // tiempo total de la animación
    private Image logoImage;
    private Vector3 targetScale;

    void Start()
    {
        logoImage = GetComponent<Image>();
        targetScale = transform.localScale;   // guarda el tamaño final
        transform.localScale = Vector3.zero;  // empieza en 0
        Color c = logoImage.color;
        c.a = 0; logoImage.color = c;         // empieza invisible
        StartCoroutine(AnimateLogo());
    }

    System.Collections.IEnumerator AnimateLogo()
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            float progress = t / duration;

            // Escala de 0 ? 1
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, progress);

            // Alpha de 0 ? 1
            Color c = logoImage.color;
            c.a = progress;
            logoImage.color = c;

            yield return null;
        }
    }
}

