using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostTimer : MonoBehaviour
{

    public int boostTimeLeft = 10;
    public bool timeToCountdown = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToCountdown)
        {
            StartTimer();
            timeToCountdown = false;
        }
    }
    IEnumerator LoseTime()
    {
        while (boostTimeLeft > 0)
        {
            yield return new WaitForSeconds(1);
            boostTimeLeft--;
        }
        PlayerMovement santaPlayerMovement = FindObjectOfType<PlayerMovement>();
        santaPlayerMovement.movementSpeed /= 2;
        BoostManager boostManager = GetComponent<BoostManager>();
        boostManager.isBoosted = false;
        boostTimeLeft = 10;
    }

    void StartTimer()
    {
        StartCoroutine("LoseTime");
        Time.timeScale = 1;
    }
}