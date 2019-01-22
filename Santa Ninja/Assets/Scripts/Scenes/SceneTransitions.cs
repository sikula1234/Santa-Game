using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
	public Animator transitionAnimator;
	public Text text;
    public GameObject MessageSpotted;
    public GameObject MessageTimesup;
    public GameObject MessageVictory;
    public GameObject ButtonRetry;
    public GameObject ButtonContinue;

    public GameObject gameoverMenuUI;
    public MusicControls musicControls;
    public AudioSource uLostSound;

    public void LoadScene(string sceneName)
	{
		StartCoroutine(LoadSceneCoroutine(sceneName));
	}

	public void LoadScene(string sceneName, int cislo)
	{
		StartCoroutine(LoadSceneCoroutine(sceneName, cislo));			
	}

	public IEnumerator LoadSceneCoroutine(string sceneName)
	{
		transitionAnimator.SetTrigger("fade_out");
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene(sceneName);
	}

	public IEnumerator LoadSceneCoroutine(string sceneName, int cislo)
	{
        // Nejspis bude potreba zpomalit/zastavit cas, aby se zamezilo glitchum!!!

        switch (cislo)
		{
			case 0: // Spotted
                MessageSpotted.SetActive(true);
                ButtonRetry.SetActive(true);
                musicControls.chosenAudio.Stop();
                //uLostSound.Play();
                text.text = "You have been spotted!";
                break;
			case 1: // Time
                MessageTimesup.SetActive(true);
                ButtonRetry.SetActive(true);
                text.text = "You ran out of time!";
				break;
			case 2: // Victory
                MessageVictory.SetActive(true);
                ButtonContinue.SetActive(true);
                text.text = "You have won!";
				break;
		}
		/* transitionAnimator.SetTrigger("text");
		transitionAnimator.SetTrigger("fade_out"); */
		yield return new WaitForSeconds(0f);
        gameoverMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
