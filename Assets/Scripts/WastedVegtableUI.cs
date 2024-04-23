using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WastedVegtableUI : MonoBehaviour
{
    public TMP_Text textLastScore;
    public TMP_Text textHighscore;

    // Start is called before the first frame update
    void Start()
    {
        int lastScore = PlayerPrefs.GetInt("LastScore", 0);
        int highScore = PlayerPrefs.GetInt("SavedHighScore", 0);
        if (lastScore > highScore)
        {
            PlayerPrefs.SetInt("SavedHighScore", lastScore);
        }
        textLastScore.text = $"You Wasted {PlayerPrefs.GetInt("LastScore")}kr";
        textHighscore.text = $"Highest amount saved: {PlayerPrefs.GetInt("SavedHighScore")}kr";
    }
}
