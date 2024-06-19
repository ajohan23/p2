
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    private int currentScore = 0;
    public TextMeshProUGUI highScoreText; // The users highscore
    public TextMeshProUGUI savedHighScore; // The saved high score

    // Method to add a certain amount to the score when answered correctly
    public void AddScore(int amount)
    {
        currentScore += amount; 
        PlayerPrefs.SetInt("LastScore", currentScore); // saved in PlayerPrefs
        UpdateText();
    }

    // Method to reduce the score by a certain amount when answered wrong
    public void ReduceScore(int amount)
    {
        currentScore -= amount;
        // Ensure the score doesn't go below zero
        currentScore = Mathf.Max(0, currentScore); // can't go under 0, if so = 0
        PlayerPrefs.SetInt("LastScore", currentScore);
        UpdateText();
    }

    // Method to get the current score
    public int GetCurrentScore() // never used
    {
        return currentScore;
    }

    private void UpdateText() // updates the text.
    {
        highScoreText.text = currentScore.ToString();   
    }
}
