using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Transform target;
	public float movementSpeed = 0.1f;
	bool foundSanta = false;

	// Update is called once per frame
	void Update()
	{
		// Hodne neefektivni - predelat
		if(!foundSanta && FindObjectOfType<Santa>())
		{
			target = FindObjectOfType<Santa>().transform;
			transform.GetComponent<Camera>().orthographicSize = 3.5f;
			foundSanta = true;
		}

		if (target)
		{			
			transform.position = Vector3.Lerp(transform.position, target.position, movementSpeed) + new Vector3(0, 50, 0);
		} 

	}
}
