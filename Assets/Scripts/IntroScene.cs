using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("Se presionó una tecla, cargando menú...");
            SceneManager.LoadScene("MenuScene");
        }
    }
}

