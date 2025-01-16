using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class LvlController : MonoBehaviour
{
    // public TMP_Text countdownText;
    // public float countdownDuration = 3f;
    private void Update()
    {
        if (!GameManager.instance.levelStarted)
        {
            StartCoroutine(StartCountdown());
        }
    }

    private System.Collections.IEnumerator StartCountdown()
    {
        // // Display countdown
        // for (int i = 3; i > 0; i--)
        // {
        //     countdownText.text = i.ToString();
        yield return new WaitForSeconds(1f);
        // }

        // Clear the text after the countdown
        //countdownText.text = "";

        // Start level or game here
        StartLevel();
    }
    private void StartLevel()
    {
        GameManager.instance.levelStarted = true;
        // Add logic here to start the level or game
        Debug.Log("Level Started!");
    }

}
