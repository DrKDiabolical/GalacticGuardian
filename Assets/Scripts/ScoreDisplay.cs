using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    TMP_Text scoreText; // Container for reference to score text.
    GameSession gameSession; // Container for reference to gameSession.

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TMP_Text>(); // Gets component from TMP text.
        gameSession = FindObjectOfType<GameSession>(); // Finds and stores reference to GameSession.
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = gameSession.GetScore().ToString(); // Sets text to GameSession score value.
    }
}
