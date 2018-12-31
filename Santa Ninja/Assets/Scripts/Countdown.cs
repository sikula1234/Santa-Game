using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class Countdown : MonoBehaviour
{
    public int timeLeft = 60;
    public Text countdown;
	SceneTransitions sceneTransitions;

	// Start is called before the first frame update
	void Start()
    {
		sceneTransitions = FindObjectOfType<SceneTransitions>();
    }

    // Update is called once per frame
    void Update()
    {
        countdown.text = ("" + timeLeft);
    }

    IEnumerator LoseTime()
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
		sceneTransitions.LoadScene("TestLevel", 1);
	}

	public void StartTimer()
	{
		StartCoroutine("LoseTime");
		Time.timeScale = 1;
	}
}
