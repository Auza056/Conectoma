using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInputManager : MonoBehaviour
{
    void Update()
    {
        // Detecta si se presiona la tecla ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Carga la escena de Opciones
            SceneManager.LoadScene("OptionsScene");
        }
    }
}
