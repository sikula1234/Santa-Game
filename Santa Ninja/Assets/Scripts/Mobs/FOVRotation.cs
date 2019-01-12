using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVRotation : MonoBehaviour
{
	//Quaternion rotation;

	void Awake()
	{
		//rotation = transform.rotation;
	}
	void LateUpdate()
	{
		//transform.rotation = rotation * Quaternion.Euler(1, 1, -90);
		//Debug.Log(rotation);
		//transform.rotation = transform.parent.transform.rotation;
	}
}
