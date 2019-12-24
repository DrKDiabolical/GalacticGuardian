using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int score = 0; // Serialized for debugging purposes

    // Start is called before the first frame update
    void Awake()
    {
        GameSessionSingleton();
    }

    // Handles game session singleton.
    void GameSessionSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Handles getting score.
    public int GetScore()
    {
        return score;
    }

    // Handles adding to score.
    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    // Handles resetting the score.
    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
