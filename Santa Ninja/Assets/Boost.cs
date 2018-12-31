﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerMovement santaPlayerMovement = FindObjectOfType<PlayerMovement>();
            santaPlayerMovement.movementSpeed *= 2;
            Debug.Log("Hit");
            Destroy(gameObject);

        }
    }
}
