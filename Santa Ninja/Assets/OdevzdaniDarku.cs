using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdevzdaniDarku : MonoBehaviour
{

    public bool jeUzDarek = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && jeUzDarek == false)
        {
            var image = GetComponent<SpriteRenderer>();
            var tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;

            Darky darky = FindObjectOfType<Darky>();
            darky.actualNumberOfGifts--;

            jeUzDarek = true;
        }
    }
}