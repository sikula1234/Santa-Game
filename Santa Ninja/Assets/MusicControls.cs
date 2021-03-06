﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControls : MonoBehaviour
{

    public AudioSource[] audioSources;
    public AudioSource chosenAudio;
    public bool paused;
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, audioSources.Length);
        chosenAudio = audioSources[rand];
        chosenAudio.Play();
        paused = false;
    }
    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void PauseMusic()
    {
        if (Time.timeScale == 0f && !paused)
        {
            chosenAudio.volume /= 4;
            paused = true;
        }
        else if (Time.timeScale != 0f)
        {
            chosenAudio.volume *= 4;
            paused = false;
        }
    }
}