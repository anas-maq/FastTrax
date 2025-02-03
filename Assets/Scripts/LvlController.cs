using UnityEngine;
using TMPro;

public class LvlController : MonoBehaviour
{
    private void Update()
    {
        if (!GameManager.instance.levelStarted)
        {
            StartCoroutine(StartCountdown());
        }
    }

    private System.Collections.IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(1f);
        StartLevel();
    }
    private void StartLevel()
    {
        GameManager.instance.levelStarted = true;
        Debug.Log("Level Started!");
    }

}
