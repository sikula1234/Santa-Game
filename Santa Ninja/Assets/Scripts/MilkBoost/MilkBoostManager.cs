using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkBoostManager : MonoBehaviour
{
    public bool isMilkBoosted = false;
    public GameObject milkBoostUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMilkBoosted)
        {
            milkBoostUI.SetActive(true);
        }
        else
        {
            milkBoostUI.SetActive(false);
        }
    }
}
