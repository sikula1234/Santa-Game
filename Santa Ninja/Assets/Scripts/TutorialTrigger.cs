using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
	public int triggerIndex;
	public bool useBlocker;
	public GameObject blocker;

	private void OnTriggerEnter(Collider other)
	{
		GetComponentInParent<Tutorial>().WriteText(triggerIndex);
		if(useBlocker)
		{
			Destroy(blocker);
		}
		Destroy(gameObject);
	}
}
