using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GiftSpawner : MonoBehaviour
{
	public GameObject giftPrefab;
	public GameObject boostPrefab;
	public int giftsToSpawn = 3;
	public int boostsToSpawn = 3;

	public void SpawnGifts()
	{
		GameObject[] giftSpawnPointArray = GameObject.FindGameObjectsWithTag("GiftSpawnPoint");
		List<GameObject> giftSpawnPoints = new List<GameObject>(giftSpawnPointArray);
		List<Vector3> usedPositions = new List<Vector3>();

		// Osetreni aby se boosty a gifty nespawnovaly v spawnovaci mistnosti
		Room room = FindObjectOfType<SpawnRoomGen>().randomSpawnRoom;
		for (int i = 0; i < giftSpawnPoints.Count(); i++)
		{
			if((giftSpawnPoints[i].transform.position.x < room.transform.position.x + 5) &&
				(giftSpawnPoints[i].transform.position.x > room.transform.position.x - 5) &&
				(giftSpawnPoints[i].transform.position.z < room.transform.position.z + 5) &&
				(giftSpawnPoints[i].transform.position.z > room.transform.position.z - 5))
			{
				//Debug.Log("Got one!");
				giftSpawnPoints.RemoveAt(i);
				i--;
			}
		}

		// Spawne darky
		for (int i = 0; i < giftsToSpawn; i++)
		{
			GameObject gift = Instantiate(giftPrefab);
			gift.transform.position = giftSpawnPoints[Random.Range(0, giftSpawnPoints.Count())].transform.position;
			usedPositions.Add(gift.transform.position);
		}

		// Spawne boosty - je potreba osetrit.. moznost bugu - vic susenek na sobe
		for (int i = 0; i < boostsToSpawn; i++)
		{
			GameObject boost = Instantiate(boostPrefab);

			bool havePosition = false;
			int randomIndex = 0;

			while(!havePosition)
			{
				randomIndex = Random.Range(0, giftSpawnPoints.Count());
				for (int j = 0; j < usedPositions.Count(); j++)
				{
					havePosition = true;
					if (giftSpawnPoints[randomIndex].transform.position == usedPositions[j])
					{
						havePosition = false;
					}
				}				
			}

			boost.transform.position = giftSpawnPoints[randomIndex].transform.position;
		}
	}
}
