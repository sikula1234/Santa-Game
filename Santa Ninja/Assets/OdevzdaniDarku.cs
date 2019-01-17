using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdevzdaniDarku : MonoBehaviour
{
    public bool jeUzDarek = false;
	SpriteRenderer spriteRenderer;
	public Sprite bezDarku;
	public Sprite sDarkem;
    
    // Start is called before the first frame update
    void Start()
    {
		spriteRenderer = transform.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = bezDarku;
    }

    // Update is called once per frame
    void Update()
    {
		
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player" && jeUzDarek == false)
        {
			/*
            var image = GetComponent<SpriteRenderer>();
            var tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor; */
			spriteRenderer.sprite = sDarkem;

			Darky darky = FindObjectOfType<Darky>();
            darky.actualNumberOfGifts--;

            jeUzDarek = true;
        }
    }
}