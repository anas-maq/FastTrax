using System.Collections;
using System.Collections.Generic;
//using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    [SerializeField] public GameObject winMenu;
    [SerializeField] public GameObject loseMenu;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            winMenu.SetActive(true);
            Time.timeScale=0f;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            loseMenu.SetActive(true);
            Time.timeScale=0f;
            //SceneManager.LoadScene("Menu");
        }
    }
    public void NextLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale=1f;
    }
    public void Retry()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale=1f;
    }
}
