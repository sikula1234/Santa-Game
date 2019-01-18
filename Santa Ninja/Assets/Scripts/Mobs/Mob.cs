using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mob : MonoBehaviour {

	public float movementSpeed = 3f;
	public PathPoint[] pathPoints;
	public Transform spriteRendererTransform;
	//public Transform fovTransform;

	Rigidbody2D mobRigidbody;
	private bool moveMob;
	private int index;

	//Testovani
	public bool move = false;
	public Vector2 pos;
	NavMeshAgent navMeshAgent;

	//Testovani 2
	public Vector2 vector;
	public float angle;

	// Use this for initialization
	void Start () {
		//transform.position = pathPoints[0].position; - Vypnuto pro testovani
		//StartCoroutine(WaitOnPoint());- Vypnuto pro testovani
		if(transform.GetComponent<NavMeshAgent>() == true)
		{
			navMeshAgent = transform.GetComponent<NavMeshAgent>();
			navMeshAgent.updateRotation = false;
		}
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
		if(move == true)
		{			
			MoveWithNavMesh();
			move = false;
		}
		
		
		if (transform.GetComponent<NavMeshAgent>() == true)
			if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
			{
				//transform.Rotate(-90, 0, 0);
				//vector = new Vector2(navMeshAgent.velocity.normalized.x, navMeshAgent.velocity.normalized.y);
				//angle = CalculateAngle(vector);
				//transform.rotation = Quaternion.Euler(0, 0, angle);
				//transform.RotateAround(new Vector3(0, 0, 1), 90f);

				//float angle = Vector3.Angle(navMeshAgent.velocity.normalized, new Vector3(0, -1, 0));



				//Debug.Log(angle);
				//transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);

				//transform.rotation = transform.rotation * Quaternion.AngleAxis(45, new Vector3(0, 0, 1));
				//transform.Rotate(0, 0, -45);

				//transform.Rotate(0, 0, -45);

				//transform.Rotate(new Vector3(0, 0, 1), 90f);
				transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized, Vector3.up); //, Vector3.back																						  //transform.rotation = 
				//spriteRendererTransform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized, Vector3.forward);
				//fovTransform.rotation = transform.rotation;
			}
	}

	float CalculateAngle (Vector2 vector)
	{
		float angle;
		if (vector.x > 0 && vector.y > 0)
		{
			//angle = Mathf.Atan(vector.x / vector.y * Mathf.Deg2Rad);
			angle = Mathf.Rad2Deg * Mathf.Atan2(vector.x, vector.y);
			angle = -angle;
		}
		else if (vector.x < 0 && vector.y > 0)
		{
			//angle = Mathf.Atan(vector.x / vector.y * Mathf.Deg2Rad);
			angle = Mathf.Rad2Deg * Mathf.Atan2(vector.x, vector.y);
		}
		else if (vector.x > 0 && vector.y < 0)
		{
			//angle = Mathf.Atan(vector.y / vector.x * Mathf.Deg2Rad);
			angle = Mathf.Rad2Deg * Mathf.Atan2(vector.y, vector.x);
			//angle = angle + 90f;
		}
		else if (vector.x < 0 && vector.y < 0)
		{
			//angle = Mathf.Atan(vector.y / vector.x * Mathf.Deg2Rad);
			angle = Mathf.Rad2Deg * Mathf.Atan2(vector.y, vector.x);
			angle = angle + 90f;
		}
		else if (vector.x == 0 && vector.y > 0)
		{
			angle = 0;
		}
		else if (vector.x == 0 && vector.y < 0)
		{
			angle = -90f;
		}
		else if (vector.x > 0 && vector.y == 0)
		{
			angle = 90f;
		}
		else if (vector.x < 0 && vector.y == 0)
		{
			angle = -90f;
		}
		else angle = 0;
		//angle = angle / 2;
		return angle;
	}
	
	/*
	private void LateUpdate()
	{
		if(transform.GetComponent<NavMeshAgent>() == true)
		if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
		{
			transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
				spriteRendererTransform.rotation = transform.rotation * Quaternion.Euler(0, 270, -90);
				fovTransform.rotation = spriteRendererTransform.rotation;
				//fovTransform.Rotate(0f, 90f, 90f);
			}
	}*/


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
			//transform.right = new Vector3(pathPoints[index].position.x, pathPoints[index].position.y, 0) - spriteRendererTransform.parent.position;
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

	void MoveWithNavMesh()
	{
		if(transform.GetComponent<NavMeshAgent>() == true)
		{
			navMeshAgent.SetDestination(pos);
		}	
		
		//transform.right = new Vector3(pathPoints[index].position.x, pathPoints[index].position.y, 0) - spriteRendererTransform.parent.position;
	}
}
