using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed = 4f;
	Joystick joystick;
	Rigidbody2D santaRigidBody;

	// Use this for initialization
	void Start () {
		joystick = FindObjectOfType<Joystick>();
		santaRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		santaRigidBody.velocity = new Vector2(joystick.Horizontal * movementSpeed, joystick.Vertical * movementSpeed);
	}
}
