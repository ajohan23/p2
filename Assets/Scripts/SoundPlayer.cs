using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] soundsSaved;
    [SerializeField] AudioClip[] soundsWasted;

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

}

