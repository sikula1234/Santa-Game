using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class Boost : MonoBehaviour
{
    BoostManager boostManager;
    // Start is called before the first frame update
    void Start()
    {
        boostManager = FindObjectOfType<BoostManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player" && !boostManager.isBoosted)
        {
            PlayerMovement santaPlayerMovement = FindObjectOfType<PlayerMovement>();
            santaPlayerMovement.movementSpeed = 5f;
            BoostManager boostManager = FindObjectOfType<BoostManager>();
            boostManager.isBoosted = true;
            SoundControls soundControls = FindObjectOfType<SoundControls>();
            soundControls.timeToPlay = true;
            BoostTimer boostTimer = FindObjectOfType<BoostTimer>();
            boostTimer.timeToCountdown = true;
            Destroy(gameObject);
		}
    }
}