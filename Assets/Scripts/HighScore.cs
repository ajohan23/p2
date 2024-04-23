
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    private int currentScore = 0;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI savedHighScore;
    // Method to add a certain amount to the score when answered correctly
    public void AddScore(int amount)
    {
        currentScore += amount;
        PlayerPrefs.SetInt("LastScore", currentScore);
        UpdateText();
    }

    // Method to reduce the score by a certain amount when answered wrong
    public void ReduceScore(int amount)
    {
        currentScore -= amount;
        // Ensure the score doesn't go below zero
        currentScore = Mathf.Max(0, currentScore);
        PlayerPrefs.SetInt("LastScore", currentScore);
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
