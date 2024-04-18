using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimaerScript : MonoBehaviour
{
    public float TimeLeft;
    public bool TimeOn = false;

    public TextMeshProUGUI TimerTxt;

    void Start()
    {
        TimeOn = true;
    }

    void Update()
    {
        if(TimeOn)
        {
            if(TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Your Time Is UP");
                TimeLeft = 0;
                TimeOn = false;
            }
            updateTimer(TimeLeft);
        }
    }
    void updateTimer(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
