using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class TimaerScript : MonoBehaviour
{
    public float TimeLeft;
    public bool TimeOn = false;
    public int endSceneId = 4; //The id of the scene to load when the time is up

    public TextMeshProUGUI TimerTxt;

    //refrences
    [SerializeField] VegtableController VegtableController;

    void Start()
    {
        TimeOn = true;
    }

    void Update()
    {
        DetermineColor();
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

        if (TimeLeft <= 0)
        {
            if (VegtableController != null)
            {
                VegtableController.StopInput();
                VegtableController.LogData();
            }
            else
            {
                Debug.Log("VegtableController not set");
            }
            Invoke("ChangeScene", 5f);
        }
        
    }
    public void Pause()
    {
        TimeOn = false;
    }
    public void UnPause()
    {
        TimeOn = true;
    }

    void DetermineColor()
    {
        if (TimeLeft <= 0)
        {
            TimerTxt.color = Color.red;
        }
        else if (TimeOn == false) 
        {
            TimerTxt.color = Color.yellow;
        }
        else
        {
            TimerTxt.color = Color.white;
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(endSceneId);
    }
}
