using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkTextTMP : MonoBehaviour
{
    public float speed = 1.5f;
    TMP_Text t;
    void Awake() { t = GetComponent<TMP_Text>(); }
    void Update()
    {
        var a = (Mathf.Sin(Time.time * speed) + 1f) * 0.5f; // 0..1
        var c = t.color; c.a = Mathf.Lerp(0.2f, 1f, a);
        t.color = c;
    }
}

