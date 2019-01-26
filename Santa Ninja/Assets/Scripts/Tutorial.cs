using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
	public float timeToWait;
	public string[] sentences;
	public GameObject panel;
	public Text text;
	public GameObject continueButton;

	int index;
	PlayerMovement santa;
	[HideInInspector]
	public bool gotCookie;
	public GameObject milkPrefab;

    // Start is called before the first frame update
    void Start()
    {
		santa = FindObjectOfType<PlayerMovement>();
		santa.movementSpeed = 0f;
		StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
		if(panel.activeSelf)
		{
			if (text.text == sentences[index])
			{
				continueButton.SetActive(true);
			}
		}

    }

	IEnumerator Type()
	{
		char[] letters = sentences[index].ToCharArray();
		for (int i = 0; i < letters.Length; i++)
		{
			char letter = letters[i];
			if(letter == '<')
			{
				bool gotFirstOne = false;
				while(true) // BE CAREFUL IN HERE
				{
					letter = letters[i];
					if (letter != '>')
					{
						text.text += letter;
					} else if (gotFirstOne == false)
					{
						gotFirstOne = true;
						text.text += letter;
					}
					else
					{
						text.text += letter;
						break;
					}
					i++;
				}
			} else
			{
				text.text += letter;
			}			
			yield return new WaitForSeconds(timeToWait);
		}
	}

	public void ReplaceBoostWithMilk()
	{
		StartCoroutine(ReplaceBoost());
	}

	IEnumerator ReplaceBoost()
	{
		yield return new WaitForSeconds(1f);

		Transform boostTransform = FindObjectOfType<Boost>().gameObject.transform;
		Destroy(FindObjectOfType<Boost>().gameObject);
		GameObject milkGameObject = Instantiate(milkPrefab);
		milkGameObject.transform.position = boostTransform.position;
	}

	public void WriteText(int triggerIndex)
	{
		panel.SetActive(true);
		santa.movementSpeed = 0f;
		if (triggerIndex == 10)
		{
			if (!gotCookie) //Before Cookie
			{
				index = 4;
				StartCoroutine(Type());
			}
			else // Before Milk
			{
				index = 5;
				FindObjectOfType<Countdown>().timeLeft = 20;
				FindObjectOfType<Countdown>().StartTimer();
				StartCoroutine(Type());
			}
		} else if(triggerIndex == 11)
		{
			if (gotCookie) //After Cookie
			{
				index = 6;
				StartCoroutine(Type());
			} else
			{
				// Do nothing
			}
		} else
		{
			index = triggerIndex;
			StartCoroutine(Type());
		}		
	}

	public void NextSentence()
	{
		santa.movementSpeed = 2.5f;
		continueButton.SetActive(false);
		panel.SetActive(false);
		text.text = "";
	}
}
