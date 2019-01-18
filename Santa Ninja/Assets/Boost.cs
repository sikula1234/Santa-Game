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


<<<<<<< HEAD
    private void OnTriggerEnter2D(Collider2D collision)
=======
	private void OnTriggerEnter(Collider collision)
>>>>>>> 05a8b0a422bdbc73ed6677002c91ac068ff0d56e
	{
		if (collision.tag == "Player" && !boostManager.isBoosted)
		{
<<<<<<< HEAD
            PlayerMovement santaPlayerMovement = FindObjectOfType<PlayerMovement>();
            santaPlayerMovement.movementSpeed *= 2;
            BoostManager boostManager = FindObjectOfType<BoostManager>();
            boostManager.isBoosted = true;
            SoundControls soundControls = FindObjectOfType<SoundControls>();
            soundControls.timeToPlay = true;
            BoostTimer boostTimer = FindObjectOfType<BoostTimer>();
            boostTimer.timeToCountdown = true;

		    Destroy(gameObject);
=======
			boostCountdownStart = true;
			if (boostTimeLeft > 0)
			{
				PlayerMovement santaPlayerMovement = FindObjectOfType<PlayerMovement>();
				santaPlayerMovement.movementSpeed = 5;
				Debug.Log("Given boost");
				Destroy(gameObject);
			}
>>>>>>> 05a8b0a422bdbc73ed6677002c91ac068ff0d56e
		}
	}
}

