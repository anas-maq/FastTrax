using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale=1f;
        SceneManager.LoadScene("Level 1");
    }
    public void QuitGame()
    {
        Time.timeScale=0f;
        Application.Quit();
        Debug.Log("Quitting Game");
    }
}
