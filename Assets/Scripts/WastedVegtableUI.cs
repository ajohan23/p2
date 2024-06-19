using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WastedVegtableUI : MonoBehaviour
{
    public TMP_Text textLastScore;
    public TMP_Text textHighscore;

    // Start is called before the first frame update
    
    // the one which shows the High score in the end screen.
    void Start()
    {
        int lastScore = PlayerPrefs.GetInt("LastScore", 0); // gets the latest score from VegetableController
        int highScore = PlayerPrefs.GetInt("SavedHighScore", 0); // gets the score from PlayerPrefs.
        if (lastScore > highScore)
        {
            PlayerPrefs.SetInt("SavedHighScore", lastScore); // this updates the High score 
        }
        
        // the text
        textLastScore.text = $"You Saved {PlayerPrefs.GetInt("LastScore")}kr";
        textHighscore.text = $"Highest amount saved: {PlayerPrefs.GetInt("SavedHighScore")}kr";
    }
}
