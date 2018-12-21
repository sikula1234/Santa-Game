using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Transform target;
	public float movementSpeed = 0.1f;

	// Update is called once per frame
	void Update()
	{
		if (target)
		{
			transform.position = Vector3.Lerp(transform.position, target.position, movementSpeed) + new Vector3(0, 0, -10);
		}

	}
}
