using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed = 4f;    //co za číslo je tady je jedno, záleží na tom, co je v Unity
	Joystick joystick;
	Rigidbody2D santaRigidBody;
	Vector3 lastLook = new Vector3(0, 0, 4000);

	// Use this for initialization
	void Start()
	{
		joystick = FindObjectOfType<Joystick>();
		santaRigidBody = GetComponent<Rigidbody2D>();

	}

	// Update is called once per frame
	void Update()
	{
		santaRigidBody.velocity = new Vector2(joystick.Horizontal * movementSpeed * Time.deltaTime, joystick.Vertical * movementSpeed * Time.deltaTime);

		//rotation of the player transform
		Vector2 moveVector = new Vector2(joystick.Horizontal, joystick.Vertical);
		Vector3 lookVector = new Vector3(joystick.Horizontal, joystick.Vertical, 4000);


		if (lookVector == new Vector3(0, 0, 4000))
		{
			lookVector = lastLook;
		}

		transform.rotation = Quaternion.LookRotation(lookVector, Vector3.back);
		transform.Translate(moveVector * Time.deltaTime * movementSpeed, Space.World);

		lastLook = lookVector;
	}
}
