using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
	public Sprite floor;
	public bool isOpen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OpenEntrance()
	{
		isOpen = true;

		BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>();
		
		for (int i = 0; i < colliders.Length; i++)
		{
			colliders[i].enabled = false;
		}

		SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < spriteRenderers.Length; i++)
		{
			spriteRenderers[i].sprite = floor;
		}
	}
}
