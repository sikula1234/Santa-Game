using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed = 4f;
	Joystick joystick;
	Rigidbody2D santaRigidBody;
    public bool flipRot = true;

	// Use this for initialization
	void Start () {
		joystick = FindObjectOfType<Joystick>();
		santaRigidBody = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
		santaRigidBody.velocity = new Vector2(joystick.Horizontal * movementSpeed, joystick.Vertical * movementSpeed);

        /*
        rotation of the area
        Vector2 moveVector = new Vector2(joystick.Horizontal, joystick.Vertical);
        Vector3 lookVector = new Vector3(joystick.Horizontal, joystick.Vertical, 4000);
        transform.rotation = Quaternion.LookRotation(lookVector, Vector3.back);
        transform.Translate(moveVector * Time.deltaTime * movementSpeed, Space.World);
        
        the same as:
        float angle = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        */     
    }

}
