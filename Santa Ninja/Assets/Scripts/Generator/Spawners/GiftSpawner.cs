using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GiftSpawner : MonoBehaviour
{
	public GameObject giftPrefab;
	public GameObject boostPrefab;
	int giftsToSpawn = 4;
	int boostsToSpawn = 3;

	public void SpawnGifts()
	{
		GameObject[] giftSpawnPoints = GameObject.FindGameObjectsWithTag("GiftSpawnPoint");
		List<Vector3> usedPositions = new List<Vector3>();

		// Spawne darky
		for (int i = 0; i < giftsToSpawn; i++)
		{
			GameObject gift = Instantiate(giftPrefab);
			gift.transform.position = giftSpawnPoints[Random.Range(0, giftSpawnPoints.Length)].transform.position;
			usedPositions.Add(gift.transform.position);
		}

		// Spawne boosty - je potreba osetrit.. moznost bugu
		for (int i = 0; i < boostsToSpawn; i++)
		{
			GameObject boost = Instantiate(boostPrefab);

			bool havePosition = false;
			int randomIndex = 0;

			while(!havePosition)
			{
				randomIndex = Random.Range(0, giftSpawnPoints.Length);
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
