using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
	public Animator transitionAnimator;
	public Text text;

	public void LoadScene(string sceneName)
	{
		StartCoroutine(LoadSceneCoroutine(sceneName));
	}

	public void LoadScene(string sceneName, int cislo)
	{
		StartCoroutine(LoadSceneCoroutine(sceneName, cislo));			
	}

	IEnumerator LoadSceneCoroutine(string sceneName)
	{
		transitionAnimator.SetTrigger("fade_out");
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene(sceneName);
	}

	IEnumerator LoadSceneCoroutine(string sceneName, int cislo)
	{
		// Nejspis bude potreba zpomalit/zastavit cas, aby se zamezilo glitchum!!! 
		switch (cislo)
		{
			case 0: // Spotted
				text.text = "You have been spotted!";
				break;
			case 1: // Time
				text.text = "You ran out of time!";
				break;
			case 2: // Victory
				text.text = "You have won!";
				break;
		}
		transitionAnimator.SetTrigger("text");
		transitionAnimator.SetTrigger("fade_out");
		yield return new WaitForSeconds(2.5f);
		SceneManager.LoadScene(sceneName);
	}
}
