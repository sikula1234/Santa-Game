using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    Animator animator;
    Rigidbody santaRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        santaRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float rychlost = santaRigidBody.velocity.magnitude * 100 / 4.2f;

        animator.SetFloat("Rychlost", rychlost);
    }
}
