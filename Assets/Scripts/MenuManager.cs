using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string gameSceneName = "GameScene"; // cambia si tu escena de juego tiene otro nombre

    public void PlayGame()
    {
        Time.timeScale = 1f; // restablece el juego al iniciar
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenOptionsJ()
    {
        SceneManager.LoadScene("OptionsSceneJ");
    }

    public void OpenOptionsM()
    {
        SceneManager.LoadScene("OptionsSceneM");
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f; // evita que el menú quede congelado si vienes de una pausa
        SceneManager.LoadScene("MenuScene");
    }

    public void BackToPreviousScene(string previousScene)
    {
        Time.timeScale = 1f; // asegura que cualquier escena previa corra normalmente
        SceneManager.LoadScene(previousScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Salir del juego (en Editor no cierra).");
    }
}
