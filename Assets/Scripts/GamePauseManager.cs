using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseManager : MonoBehaviour
{
    private string optionsSceneName = "OptionsSceneJ";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Verifica si la escena de opciones está cargada
            if (!IsSceneLoaded(optionsSceneName))
            {
                OpenOptions();
            }
            else
            {
                CloseOptions();
            }
        }
    }

    void OpenOptions()
    {
        Time.timeScale = 0f; // pausa el juego
        if (!IsSceneLoaded(optionsSceneName))
        {
            SceneManager.LoadScene(optionsSceneName, LoadSceneMode.Additive);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseOptions()
    {
        if (IsSceneLoaded(optionsSceneName))
        {
            SceneManager.UnloadSceneAsync(optionsSceneName);
        }
        Time.timeScale = 1f; // reanuda el juego
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
                return true;
        }
        return false;
    }
}
