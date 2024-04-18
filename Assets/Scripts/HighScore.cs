
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    private int currentScore = 0;
    public TextMeshProUGUI highScoreText;
    // Method to add a certain amount to the score when answered correctly
    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateText();
    }

    // Method to reduce the score by a certain amount when answered wrong
    public void ReduceScore(int amount)
    {
        currentScore -= amount;
        // Ensure the score doesn't go below zero
        currentScore = Mathf.Max(0, currentScore);
        UpdateText();
    }

    // Method to get the current score
    public int GetCurrentScore()
    {
        return currentScore;
    }
    private void UpdateText()
    {
        highScoreText.text = currentScore.ToString();   
    }
}
