using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoIntro : MonoBehaviour
{
    public float waitTime = 3f; // tiempo en segundos

    void Start()
    {
        Invoke("LoadNextScene", waitTime);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("IntroScene");
    }
}

