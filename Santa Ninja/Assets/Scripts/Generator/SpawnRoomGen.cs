using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomGen : MonoBehaviour
{
	public GameObject santaPrefab;

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
		Room randomSpawnRoom = spawnRooms[Random.Range(0, spawnRooms.Length)];
		GameObject randomSpawnInteriorPrefab = spawnInteriorPrefabs[Random.Range(0, spawnInteriorPrefabs.Length)];
		randomSpawnRoom.SetRoomInterior(randomSpawnInteriorPrefab, santaPrefab);
	}
}
