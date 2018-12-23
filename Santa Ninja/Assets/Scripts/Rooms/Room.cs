using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
	//Souradky pro generator
	public bool isDiscovered = false;
	public Vector2 position;

	public Tilemap tilemap;

	enum roomType
	{
		diningRoom, bathRoom, livingRoom, hall, kitchen
	} //Dalsi napady: library, office, cellar

	enum roomEntrances // Combinations of entrances
	{
		oneEntrance, oppositeEntrances, LEntrances, TEntrance, XEntrance
	}
	
    // Start is called before the first frame update
    void Start()
    {
		GenerateRoom();
    }

    // Update is called once per frame
    void Update()
    {
		//Pro Testovani - pak smazat
		if (!isDiscovered)
		{
			tilemap.color = new Color(0, 0, 0);
		} else tilemap.color = new Color(255, 255, 255);

	}

	public static void GenerateRoom()
	{
		//Vybere randomnni mistnost podle typu z prefabu
		RandomizeTextures();
	}

	public static void RandomizeTextures()
	{
		//Randomne priradi danym objektum textury podle danyho theme
	}
}
