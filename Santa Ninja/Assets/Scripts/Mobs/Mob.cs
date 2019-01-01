using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour {

	public float movementSpeed = 3f;
	public PathPoint[] pathPoints;
	public Transform spriteRendererTransform;

	Rigidbody2D mobRigidbody;
	private bool moveMob;
	private int index;

	// Use this for initialization
	void Start () {
		//transform.position = pathPoints[0].position; - Vypnuto pro testovani
		//StartCoroutine(WaitOnPoint());- Vypnuto pro testovani
	}

	// Update is called once per frame
	void Update () {

		// Mob movement
		/*if (moveMob) {	
			//Jestli stoji mob na bode
			if (transform.position.x == pathPoints[index].position.x && transform.position.y == pathPoints[index].position.y)
			{
				moveMob = false;
				StartCoroutine(WaitOnPoint()); //Zacne cekani
			}

			MoveMob();
		}	Vypnuto pro testovani*/	
	}

	
	private void MoveMob()
	{		
		transform.position = Vector3.MoveTowards(transform.position, pathPoints[index].position, movementSpeed * Time.deltaTime);
	}

	//Ceka a pak vysle moba na dalsi bod
	IEnumerator WaitOnPoint()
	{		
		yield return new WaitForSecondsRealtime(pathPoints[index].timeToWait);
		if(index >= pathPoints.Length - 1) {
			//Konec cesty
			moveMob = false;
			index = 0;
		} else {
			index++;
			moveMob = true;

			//obstarava rotaci celeho moba (fov i sprite renderer)
			transform.right = new Vector3(pathPoints[index].position.x, pathPoints[index].position.y, 0) - spriteRendererTransform.parent.position;
		}

	}

	//Vykresluje pohyb moba
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		for(int i = 1; i < pathPoints.Length; i++)
		{
			Gizmos.DrawLine(pathPoints[i-1].position, pathPoints[i].position);
		}		

	}
}
