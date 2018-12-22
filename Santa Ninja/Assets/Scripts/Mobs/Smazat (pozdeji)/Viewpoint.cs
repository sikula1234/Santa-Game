using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewpoint : MonoBehaviour {

	void Start()
	{

	}

	void Update()
	{
		
	}

	public void LookAtTarget(Vector3 target)
	{
		transform.right = target - transform.parent.position;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player") //Pri dotyku santa umre
		{
			collision.transform.GetComponent<Santa>().Die();
		}
	}
}
