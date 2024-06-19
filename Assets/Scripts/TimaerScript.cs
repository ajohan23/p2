using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class TimaerScript : MonoBehaviour
{
    public float TimeLeft;
    public bool TimeOn = false;
    public int endSceneId = 4; //The id of the scene to load when the time is up

    public TextMeshProUGUI TimerTxt; // the text for the timer
    public GameObject timesUpText;

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
                TimeLeft -= Time.deltaTime; // the time 
            }
            else
            {
                Debug.Log("Your Time Is UP");
                TimeLeft = 0;
                TimeOn = false;
            }
            updateTimer(TimeLeft); // the visuals
        }
    }
    void updateTimer(float currentTime)
    {
        // floortoInt makes it an Int
        float minutes = Mathf.FloorToInt(currentTime / 60); // the time in mins divided by 60 
        float seconds = Mathf.FloorToInt(currentTime % 60); // the seconds 

        TimerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds); // The Format is how it should look like.

        // could also be written as so:  TimerTxt.text = $"{minutes} : {seconds}";
        
        if (TimeLeft <= 0)
        {
            TimesUp();
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

    void DetermineColor() // makes the colour change.
    {
        if (TimeLeft <= 0)
        {
            TimerTxt.color = Color.red;
        }
        else if (TimeOn == false) // the time is "paused"
        {
            TimerTxt.color = Color.yellow;
        }
        else
        {
            TimerTxt.color = Color.white;
        }
    }

    void ChangeScene() // changes scene
    {
        SceneManager.LoadScene(endSceneId);
    }

    void TimesUp()
    {
        if (VegtableController != null)
            {
                VegtableController.StopInput();
                VegtableController.LogData(); // not used :)
            }
            else
            {
                Debug.Log("VegtableController not set");
            }
            timesUpText.SetActive(true); // shows to the user, that the time is up.
            Invoke("ChangeScene", 5f); // changes sceme.
    }
}
