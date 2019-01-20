using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
	public float enlargementTime;
	public float maxCircleSize = 1f;
	float timeLeft;
	public float ratio;
	public float velocity;
	public float radius;

	CapsuleCollider capsuleCollider;
	SpriteRenderer sprite;


	// Use this for initialization
	void Start()
	{
		capsuleCollider = GetComponent<CapsuleCollider>();
		sprite = GetComponent<SpriteRenderer>();
		timeLeft = 0;
	}

	// Update is called once per frame
	void Update()
	{
		timeLeft -= Time.deltaTime;

		velocity = transform.parent.GetComponent<Rigidbody>().velocity.magnitude * 100;

		if (velocity > 4.2f)
		{
			velocity = 4.2f;
		}

		if (timeLeft <= 0)
		{
			ratio = velocity / 4.2f; // Vypocita polomer kruhu pri jeho vysledne velikosti
			enlargementTime = 2 - (ratio); // Na zaklade velikosti kruhu zvoli rychlost, kterou se bude zvetsovat
			timeLeft = enlargementTime;
			transform.localScale = new Vector3(0f, 0f, 0f);
			capsuleCollider.radius = 0f;
		}

		// Spocita radius a ujisti ze ze je > 0
		radius = ratio - (timeLeft / enlargementTime);
		if(radius < 0)
		{
			radius = 0;
		}

		// Nastavi velikost collideru a objektu podle promenny radius
		transform.localScale = new Vector3(radius, radius, 0f);
		capsuleCollider.radius = radius;

		//Nastavi barvu a pruhlednost kruhu
		if(velocity >= 4f)
		{
			sprite.color = new Color(255, 0, 0, 255);
		} else
		{
			float opacity = velocity * 255 / 4f;
			sprite.color = new Color(255, 255, 255, opacity);
		}
		
	}

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.tag == "Mob")
		{
			transform.parent.GetComponent<Santa>().Die();
		}
	}
}
