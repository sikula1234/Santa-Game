using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCookieTrigger : MonoBehaviour
{
	int triggerIndex = 11;

	private void OnTriggerEnter(Collider other)
	{
		if(GetComponentInParent<Tutorial>().gotCookie == false)
		{
			GetComponentInParent<Tutorial>().gotCookie = true;
			GetComponentInParent<Tutorial>().ReplaceBoostWithMilk();
		} else
		{
			GetComponentInParent<Tutorial>().WriteText(triggerIndex);
		}
		
		Destroy(gameObject);
	}
}
