using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class Darky : MonoBehaviour  {

    public int giftsStart = 4;
    public int actualNumberOfGifts = 4;
    public Text giftsCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        giftsCounter.text = ("" + actualNumberOfGifts);        
    }
}
