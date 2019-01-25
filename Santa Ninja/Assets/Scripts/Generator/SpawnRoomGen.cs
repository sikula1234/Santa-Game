using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomGen : MonoBehaviour
{	
	public GameObject santaPrefab;
	public Room randomSpawnRoom;

	public GameObject[] spawnInteriorPrefabs;
	public Room[] spawnRooms;
	

	// Start is called before the first frame update
	void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SpawnRandomRoom()
	{
		// Spawne mistnost
		int randSpawnRoom = Random.Range(0, spawnRooms.Length);
		randomSpawnRoom = spawnRooms[randSpawnRoom];
		GameObject randomSpawnInteriorPrefab = spawnInteriorPrefabs[Random.Range(0, spawnInteriorPrefabs.Length)];
		randomSpawnRoom.SetRoomInterior(randomSpawnInteriorPrefab, santaPrefab);

		// Vymaze ji z MobSpawneru
		MobSpawner mobSpawner = FindObjectOfType<MobSpawner>();

		int spawnRoomIndex = 0;
		switch(randSpawnRoom)
		{
			case 0:
				spawnRoomIndex = 5;
				break;
			case 1:
				spawnRoomIndex = 6;
				break;
			case 2:
				spawnRoomIndex = 9;
				break;
			case 3:
				spawnRoomIndex = 10;
				break;
			default:
				Debug.Log("Neco tu nesedi!");
				break;
		}

		mobSpawner.RemoveSpawnRoom(spawnRoomIndex);
	}
}
