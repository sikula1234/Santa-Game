using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathPoint {

	public Vector2 position;
	public float timeToWait; //Jak dlouho pocka na miste

	public PathPoint(Vector2 position, float timeToWait)
	{
		this.position = position;
		this.timeToWait = timeToWait;
	}
}
