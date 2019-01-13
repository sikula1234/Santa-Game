using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class Boost : MonoBehaviour
{
	public int boostTimeLeft = 0;
	//public Text boostCountdown;
	public bool boostCountdownStart = true;
	public bool boostCountdownFirst = true;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (boostCountdownStart == true && boostCountdownFirst == true)
		{
			boostCountdownFirst = false;
			boostTimeLeft = 10;
		}
		//boostCountdown.text = ("" + boostTimeLeft); - nemame pocitadlo zatim
	}

	IEnumerator LoseTime()
	{
		while (boostTimeLeft > 0)
		{
			yield return new WaitForSeconds(1);
			boostTimeLeft--;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			boostCountdownStart = true;
			if (boostTimeLeft > 0)
			{
				PlayerMovement santaPlayerMovement = FindObjectOfType<PlayerMovement>();
				santaPlayerMovement.movementSpeed = 5;
				Debug.Log("Hit");
				Destroy(gameObject);
			}
		}
	}
}
