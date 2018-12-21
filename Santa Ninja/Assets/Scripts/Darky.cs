using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class Darky : MonoBehaviour  {

    public int giftsStart = 3;
    public int actualNumberOfGifts;
    public Text giftsCounter;

    // Start is called before the first frame update
    void Start()
    {
		actualNumberOfGifts = giftsStart;
	}

    // Update is called once per frame
    void Update()
    {
        giftsCounter.text = ("" + actualNumberOfGifts);        
    }
}
