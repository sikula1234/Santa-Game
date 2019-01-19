using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControls : MonoBehaviour
{

    public AudioSource audioSource;
    public bool paused;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PauseMusic()
    {
        if (paused == false)
        {
            audioSource.volume /= 5;
            paused = true;
        }
        else
        {
            audioSource.volume *= 5;
            paused = false;
        }
    }
}