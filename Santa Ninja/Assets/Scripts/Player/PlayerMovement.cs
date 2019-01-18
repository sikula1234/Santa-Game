using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed = 4f;    //co za číslo je tady je jedno, záleží na tom, co je v Unity
	Joystick joystick;
	Rigidbody santaRigidBody;
	Vector3 lastLook = new Vector3(0, 0, 4000);

	// Use this for initialization
	void Start()
	{
		joystick = FindObjectOfType<Joystick>();
		santaRigidBody = GetComponent<Rigidbody>();

	}

	// Update is called once per frame
	void Update()
	{
		santaRigidBody.velocity = new Vector3(joystick.Horizontal * movementSpeed * Time.deltaTime, 0 , joystick.Vertical * movementSpeed * Time.deltaTime);

		//rotation of the player transform
		Vector3 moveVector = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
		Vector3 lookVector = new Vector3(joystick.Horizontal, 0, joystick.Vertical);


		if (lookVector == new Vector3(0, 0, 0))
		{
			lookVector = lastLook;
		}

		transform.rotation = Quaternion.LookRotation(lookVector, Vector3.up);
		transform.Translate(moveVector * Time.deltaTime * movementSpeed, Space.World);
		transform.Rotate(new Vector3(90f, 0));

		lastLook = lookVector;
	}
}
