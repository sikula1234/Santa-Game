using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
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
