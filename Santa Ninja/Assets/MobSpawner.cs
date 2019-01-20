using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MobSpawner : MonoBehaviour
{
    public GameObject[] mobPrefabs;
    public int pocetMobu = 6;

	public Transform[] roomArray;
	public List<Transform> rooms = new List<Transform>();

	private void Start()
	{
		CopyArray();
	}

	//Je zavolana LevelGenem az se vygeneruje level
	public void SpawnMobs()
	{
		for (int i = 0; i < pocetMobu; i++)
		{
			int rand = Random.Range(0, mobPrefabs.Length);		

			Vector3 randomPosition = GenerateRandomPosition(ChooseRoom(), 5f);
			GameObject mob = Instantiate(mobPrefabs[rand], randomPosition, Quaternion.identity);

			mob.transform.GetComponent<MobAI>().StartMoving();
		}
	}

	//Vymaze spawn room z listu, je zavolana SpawnRoomGenem pri generaci spawn roomu
	public void RemoveSpawnRoom(int index)
	{
		//Debug.Log("Removing room: " + rooms[index]);
		rooms.RemoveAt(index);
	}

	// Zkopci array do listu
	void CopyArray()
	{
		for (int i = 0; i < roomArray.Length; i++)
		{
			rooms.Add(roomArray[i]);
		}
	}

	//Vybere random mistnost co jeste nebyla vybrana
	Transform ChooseRoom()
	{
		int rand = Random.Range(0, rooms.Count());
		Transform room = rooms[rand];
		rooms.RemoveAt(rand);
		return room;
	}

	//Vybere v dane mistnosti random pozici na spawnuti
	public Vector3 GenerateRandomPosition(Transform roomTransform, float radius)
	{
		while(true)
		{
			Vector3 randomDirection = Random.insideUnitSphere * radius;
			randomDirection += roomTransform.position;
			NavMeshHit hit;
			Vector3 finalPosition = Vector3.zero;
			if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
			{
				finalPosition = hit.position;
				return finalPosition;
			}			
		}		
	}
}
