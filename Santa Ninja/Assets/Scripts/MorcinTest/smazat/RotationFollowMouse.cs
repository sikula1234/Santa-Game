﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFollowMouse : MonoBehaviour
{
	Camera viewCamera;
	float angle;
	Vector2 mousePos;

	private void Start()
	{
		viewCamera = Camera.main;

	}

	private void Update()
	{
		mousePos = viewCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 10000 * Time.deltaTime);
	}
}
