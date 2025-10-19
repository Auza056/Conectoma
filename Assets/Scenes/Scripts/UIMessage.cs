using UnityEngine;
using TMPro;
using System.Collections;

public class UIMessage : MonoBehaviour
{
    public static UIMessage Instance { get; private set; }

    public TextMeshProUGUI messageText;
    public float fadeDuration = 3f;

    private Coroutine currentRoutine;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        // Inicializar transparencia
        SetAlpha(0);
        messageText.text = "";
    }

    public void ShowMessage(string text)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(FadeMessage(text));
    }

    private IEnumerator FadeMessage(string text)
    {
        messageText.text = text;

        // Fade in
        yield return StartCoroutine(FadeTo(1, 0.2f));

        // Espera
        yield return new WaitForSeconds(fadeDuration);

        // Fade out
        yield return StartCoroutine(FadeTo(0, 1f));
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = messageText.alpha;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            messageText.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null;
        }

        messageText.alpha = targetAlpha;
    }

    private void SetAlpha(float alpha)
    {
        messageText.alpha = alpha;
    }
}