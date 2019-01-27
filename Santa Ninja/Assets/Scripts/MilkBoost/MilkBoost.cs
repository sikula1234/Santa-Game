using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilkBoost : MonoBehaviour
{
    MilkBoostManager milkBoostManager;
    // Start is called before the first frame update
    void Start()
    {
        milkBoostManager = FindObjectOfType<MilkBoostManager>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player"&& !milkBoostManager.isMilkBoosted)
		{
            Countdown timeLeft = FindObjectOfType<Countdown>();
            timeLeft.timeLeft += 20;

			// Vypnul jsem to, nejspis nebudem potrebovat milk UI -Morcinus
			MilkBoostManager milkBoostManager = FindObjectOfType<MilkBoostManager>();
            milkBoostManager.isMilkBoosted = true;
            
            Destroy(gameObject);
        }
    }
}
