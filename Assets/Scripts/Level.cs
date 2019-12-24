using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float loadDelayAmount = 3f; // Defines scene load delay time.

    // Loads GameOver scene.
    public void LoadGameOver()
    {
        StartCoroutine(WaitThenLoad());
    }

    // Loads Game scene.
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }

    // Loads StartMenu scene.
    public void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    // Quits Application.
    public void QuitGame()
    {
        Application.Quit();
    }

    // Waits for defined time then loads GameOver scene.
    IEnumerator WaitThenLoad()
    {
        yield return new WaitForSeconds(loadDelayAmount);
        SceneManager.LoadScene("GameOver");
    }
}
