using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControls : MonoBehaviour
{
    public AudioSource audioSource;
    public bool timeToPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (timeToPlay)
        {
            audioSource.Play(0);
            timeToPlay = false;
        }
    }
}
