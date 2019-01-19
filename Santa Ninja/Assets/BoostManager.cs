using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    public bool isBoosted = false;
    public GameObject boostUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isBoosted)
        {
            boostUI.SetActive(true);
        }
        else
        {
            boostUI.SetActive(false);
        }
    }
}