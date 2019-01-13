using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
	//Souradky pro generator
	public bool isDiscovered = false;
	public Vector2 position;
	
	public Tilemap tilemap;
	public roomTypes roomType = roomTypes.Default;
	public roomEntrances roomEntrance = roomEntrances.Default;
	public roomEntranceTypes roomEntranceType = roomEntranceTypes.Default;

	public GameObject[] roomInteriorPrefabs;

	GameObject interiorPrefab;

	public enum roomTypes
	{
		Default, bedRoom, bathRoom, livingRoom, hall, kitchen
	} // Typy mistnosti

	public enum roomEntrances // Combinations of entrances: combination = n1 + n2 + n3 + n4
	{
		Default = 0, T = 1, R = 4, B = 9, L = 16, TB = 10, RL = 20, LTR = 21, TRB = 14, RBL = 29, BLT = 26, X = 30, TR = 5, RB = 13, BL = 25, LT = 17
	}

	public enum roomEntranceTypes // Type of room entrances
	{
		Default, oneEntrance, oppositeEntrances, LEntrance, TEntrance, XEntrance
	}

	// Start is called before the first frame update
	void Start()
    {
		
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

	public void GenerateRoom()
	{
		
		roomType = (roomTypes)Random.Range(1, 6);
		List<GameObject> suitableInteriorPrefabs = new List<GameObject>();
		//Vybere randomnni mistnost podle typu z prefabu
		for (int i = 0; i < roomInteriorPrefabs.Length; i++)
		{
			if(roomInteriorPrefabs[i].GetComponent<RoomInterior>().IsSuitable(roomType, roomEntranceType))
			{
				suitableInteriorPrefabs.Add(roomInteriorPrefabs[i]);
			}
		}

		if (suitableInteriorPrefabs.Count() > 0)
		{
			GameObject randomInteriorPrefab = suitableInteriorPrefabs[(int)Random.Range(0, suitableInteriorPrefabs.Count())]; // odebrano -1

			//Spawne interior
			interiorPrefab = Instantiate(randomInteriorPrefab);
			interiorPrefab.transform.parent = gameObject.transform;
			interiorPrefab.transform.localPosition = new Vector3(0, 0, 0);
			interiorPrefab.GetComponent<RoomInterior>().GenerateInterior(roomEntranceType, roomEntrance);
		} else
		{
			Debug.LogWarning("There is not a suitable room: " + roomType + ", " + roomEntranceType);
		}	
	}

	//Sets roomEntrance and updates roomEntranceType
	public void SetRoomEntrances(int cislo)
	{
		roomEntrance = (roomEntrances)cislo;

		if (cislo == 1 || cislo == 4 || cislo == 9 || cislo == 16)
			roomEntranceType = roomEntranceTypes.oneEntrance;
		else if (cislo == 10 || cislo == 20)
			roomEntranceType = roomEntranceTypes.oppositeEntrances;
		else if (cislo == 21 || cislo == 14 || cislo == 29 || cislo == 26)
			roomEntranceType = roomEntranceTypes.TEntrance;
		else if (cislo == 5 || cislo == 13 || cislo == 25 || cislo == 17)
			roomEntranceType = roomEntranceTypes.LEntrance;
		else if (cislo == 30)
			roomEntranceType = roomEntranceTypes.XEntrance;
		else roomEntranceType = roomEntranceTypes.Default;
	}	

	// Pro SpawnRoomGen
	public void SetRoomInterior(GameObject newInteriorPrefab, GameObject santaPrefab)
	{
		Destroy(interiorPrefab);
		newInteriorPrefab = Instantiate(newInteriorPrefab);
		newInteriorPrefab.transform.parent = gameObject.transform;
		newInteriorPrefab.transform.localPosition = new Vector3(0, 0, 0);
		//newInteriorPrefab.GetComponent<RoomInterior>().GenerateInterior(roomEntranceType, roomEntrance);
		newInteriorPrefab.GetComponent<SpawnRoom>().SpawnSanta(santaPrefab);
	}
}
