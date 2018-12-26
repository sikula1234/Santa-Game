using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    public float movementSpeed = 4f;    //co za číslo je tady je jedno, záleží na tom, co je v Unity
    Joystick joystick;
    Rigidbody2D santaRigidBody;
    public bool flipRot = true;

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
        transform.rotation = Quaternion.LookRotation(lookVector, Vector3.back);
        transform.Translate(moveVector * Time.deltaTime * movementSpeed, Space.World);
    }

    void LastUpdate()
    {

    }
    

}
