﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OldFieldOfView : MonoBehaviour
{
	public float viewRadius = 5;
	public float viewAngle = 135;
	Collider2D[] playerInRadius;
	public LayerMask obstacleMask, playerMask;
	public List<Transform> visiblePlayer = new List<Transform>();
	public float angle;

	public bool zmen1;
	public bool zmen2;
	public bool zmen3;
	public int change;
	private NavMeshAgent navMeshAgent;

	private void Start()
	{
		navMeshAgent = transform.GetComponent<NavMeshAgent>();
	}

	private void FixedUpdate()
	{
		FindVisiblePlayer();
	}

	void FindVisiblePlayer()
	{
		playerInRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius);

		visiblePlayer.Clear();

		for(int i = 0; i < playerInRadius.Length; i++)
		{
			Transform player = playerInRadius[i].transform;
			Vector2 dirPlayer = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
			if(Vector2.Angle(dirPlayer, transform.right) < viewAngle / 2)
			{
				float distancePlayer = Vector2.Distance(transform.position, player.position);

				if(!Physics2D.Raycast(transform.position, dirPlayer, distancePlayer, obstacleMask))
				{
					visiblePlayer.Add(player);
					DetectSanta(player);
				}
			}
		}
	}

	public Vector2 DirFromAngle(float angleDeg, bool global)
	{
		if(!global)
		{
			//angleDeg -= 360f;
			angleDeg += transform.eulerAngles.z; //x - 180f
			angle = angleDeg;
		}
		return new Vector2(Mathf.Sin(angleDeg * Mathf.Deg2Rad), Mathf.Cos(angleDeg * Mathf.Deg2Rad));
	}

	public Vector2 DirFromAngle2(float angleDeg, int firstLoop)
	{

		angleDeg -= 360f;
		angleDeg += transform.eulerAngles.y; //x - 180f
		angle = angleDeg;

		Vector2 vect = new Vector2(Mathf.Sin(angleDeg * Mathf.Deg2Rad), Mathf.Cos(angleDeg * Mathf.Deg2Rad));
		/*
		if(Mathf.Sin(angleDeg * Mathf.Deg2Rad) < 0 && Mathf.Cos(angleDeg * Mathf.Deg2Rad) < 0 && firstLoop)
		{
			Debug.Log("ted");
			vect = new Vector2(Mathf.Sin(angleDeg * Mathf.Deg2Rad), -Mathf.Cos(angleDeg * Mathf.Deg2Rad));
		} */

		if (firstLoop == 0)
		{
			if (Mathf.Sin(angleDeg * Mathf.Deg2Rad) < 0 && Mathf.Cos(angleDeg * Mathf.Deg2Rad) < 0)
			{
				zmen1 = true;
			}
			if (Mathf.Sin(angleDeg * Mathf.Deg2Rad) <= 0 && Mathf.Cos(angleDeg * Mathf.Deg2Rad) >= 0)
			{
				zmen2 = true;

				if (Mathf.Sin(angleDeg * Mathf.Deg2Rad) < -0.09f && Mathf.Cos(angleDeg * Mathf.Deg2Rad) > 0.09f)
				{
					zmen3 = true;

					if (Mathf.Sin(angleDeg * Mathf.Deg2Rad) < -0.9f && Mathf.Cos(angleDeg * Mathf.Deg2Rad) > 0.15f)
					{
						zmen3 = false;
					}
					if (Mathf.Sin(angleDeg * Mathf.Deg2Rad) > -0.9f && Mathf.Cos(angleDeg * Mathf.Deg2Rad) > 0.4f)
					{
						zmen3 = false;

					}
				}
			}
		} 

		//Debug.Log(Mathf.Sin(angleDeg * Mathf.Deg2Rad) + ", " + Mathf.Cos(angleDeg * Mathf.Deg2Rad));

		if (zmen1)
		{
			vect = new Vector2(Mathf.Sin(angleDeg * Mathf.Deg2Rad), -Mathf.Cos(angleDeg * Mathf.Deg2Rad));
		}
		if (zmen2)
		{
			vect = new Vector2(Mathf.Sin(angleDeg * Mathf.Deg2Rad), -Mathf.Cos(angleDeg * Mathf.Deg2Rad));
		}
		if (zmen3)
		{
			vect = new Vector2(-Mathf.Sin(angleDeg * Mathf.Deg2Rad), Mathf.Cos(angleDeg * Mathf.Deg2Rad));
		}

		if (firstLoop == 2)
		{
			zmen1 = false;
			zmen2 = false;
			zmen3 = false;
		}

		return vect;
	}

	void DetectSanta(Transform transform)
	{
		if (transform.GetComponent<Santa>() != null)
		{
			transform.GetComponent<Santa>().Die();
		}
	}
}

