using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
	public Animator transitionAnimator;

	void Update()
	{
		
	}

	public void LoadScene(string sceneName)
	{
		StartCoroutine(LoadSceneCoroutine(sceneName));
	}

	IEnumerator LoadSceneCoroutine(string sceneName)
	{
		transitionAnimator.SetTrigger("fade_out");
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene(sceneName);
	}
}
