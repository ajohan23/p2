using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] soundsSaved;
    [SerializeField] AudioClip[] soundsWasted;
    [SerializeField] AudioClip gibberishBird;
    [SerializeField] AudioClip trashBin;
    [SerializeField] AudioClip cutKnife;

    AudioSource myAudioSource; 

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        
    }   

    public void SoundsSaved()
    {
        AudioClip clip = soundsSaved[UnityEngine.Random.Range(0, soundsSaved.Length)]; 
        myAudioSource.PlayOneShot(clip);
    }

    public void SoundsWasted()
    {
        AudioClip clip = soundsWasted[UnityEngine.Random.Range(0, soundsSaved.Length)];
        myAudioSource.PlayOneShot(clip);
    }

    public void SoundsGibberish()
    {
        AudioClip clip = gibberishBird;
        myAudioSource.PlayOneShot(clip);
    }

    public void SoundsTrashBin()
    {
        AudioClip clip = trashBin;
        myAudioSource.PlayOneShot(clip);
    }
    public void SoundsCut()
    {
        AudioClip clip = cutKnife;
        myAudioSource.PlayOneShot(clip);
    }

}

