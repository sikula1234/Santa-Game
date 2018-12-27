using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevelGen : MonoBehaviour
{
	private Vector2[] startingPositions = new Vector2[4];
	//public GameObject[] rooms; // index 0 --> LR, index 1 --> LRB, index 2 --> LRT, index 3 --> LRBT

	private int direction;
	private int moveAmount = 2;

	public float timeBtwRoom;
	public float startTimeBtwRoom = 0.25f;

	private float minX = 0;
	private float maxX = 6;
	private float maxY = 6;
	public bool stopGeneration;
	public bool stopRoomGeneration = true;

	public LayerMask room;

	private int downCounter;

	//Moje promenny
	public Transform[] entranceTransforms;
	public Transform[] roomTransforms;
	Vector2 genPos;
	Vector2 entrPos;
	Transform[,] array;
	Room[] rooms = new Room[16];

	void Awake()
	{
		array = Create2DArray(entranceTransforms, roomTransforms);

		for (int i = 0; i < startingPositions.Length; i++)
		{
			startingPositions[i] = rooms[i].position;
		}
	}

	// Use this for initialization
	void Start()
	{
		int randStartPos = Random.Range(0, 3);
		genPos = startingPositions[randStartPos];
		array[(int)genPos.x, (int)genPos.y].GetComponent<Room>().isDiscovered = true;
		
		direction = Random.Range(1, 6);
	}

	// Update is called once per frame
	void Update()
	{
		if (timeBtwRoom <= 0 && stopGeneration == false)
		{
			Move();
			timeBtwRoom = startTimeBtwRoom;
		}
		else if(timeBtwRoom <= 0 && stopRoomGeneration == false)
		{
			for (int i = 0; i < rooms.Length; i++)
			{
				if(rooms[i].isDiscovered == false)
				{
					genPos = rooms[i].position;
					Vector2 randomPos = RandomEntrancePosition(rooms[i].position);
					MoveGen(randomPos.x, randomPos.y, false);
				}
			}

			if(DiscoverRooms())
			{
				SetRoomTypes(rooms);
				for (int i = 0; i < rooms.Length; i++)
				{
					rooms[i].GenerateRoom();
				}
				stopRoomGeneration = true;
			}
		} else
		{
			timeBtwRoom -= Time.deltaTime;
		}
	}

	private void Move()
	{
		if (direction == 1 || direction == 2) // Move RIGHT!
		{
			if (genPos.x < maxX)
			{
				downCounter = 0;

				MoveGen(moveAmount, 0, true);

				direction = Random.Range(1, 6);
				if (direction == 3)
					direction = 2;
				else if (direction == 4)
					direction = 5;
			}
			else
			{
				direction = 5;
			}

		}
		else if (direction == 3 || direction == 4) // Move LEFT!
		{
			if (genPos.x > minX)
			{
				downCounter = 0;
				MoveGen(-moveAmount, 0, true);

				direction = Random.Range(3, 6);
			}
			else
			{
				direction = 5;
			}

		}
		else if (direction == 5) // Move DOWN!
		{
			downCounter++;

			if (genPos.y < maxY)
			{
				MoveGen(0, moveAmount, true); 
				direction = Random.Range(1, 6);
			}
			else
			{
				stopGeneration = true;
				stopRoomGeneration = false;
			}
		}
	}

	// Posune se na dane misto, udela diru do zdi, oznaci mistnost za objevenou
	private void MoveGen(float x, float y, bool discoverRoom)
	{
		//Debug.Log(x + "," + y);		
		
		Vector2 entrancePosition = new Vector2(genPos.x + (x / 2), genPos.y + (y / 2));
		//Debug.Log("entrpos:" + entrancePosition.x + ", " + entrancePosition.y);

		//Debug.Log("oldgenpos:" + genPos.x + ", " + genPos.y);
		genPos = new Vector2(genPos.x + x, genPos.y + y);
		//Debug.Log("newgenpos:" + genPos.x + ", " + genPos.y);
				
		array[(int)entrancePosition.x, (int)entrancePosition.y].GetComponent<Entrance>().OpenEntrance();
		if(discoverRoom)
		{
			array[(int)genPos.x, (int)genPos.y].GetComponent<Room>().isDiscovered = true;
		}
	}

	private bool DiscoverRooms()
	{
		for (int i = 0; i < rooms.Length; i++)
		{
			Vector2 roomPos = rooms[i].position;
			
			if(roomPos.y + 2 <= 6)
			{
				if(array[(int)roomPos.x, (int)roomPos.y + 1].GetComponent<Entrance>().isOpen &&
					array[(int)roomPos.x, (int)roomPos.y + 2].GetComponent<Room>().isDiscovered)
				{
					rooms[i].isDiscovered = true;
				}
			}
			if (roomPos.y - 2 >= 0)
			{
				if (array[(int)roomPos.x, (int)roomPos.y - 1].GetComponent<Entrance>().isOpen &&
					array[(int)roomPos.x, (int)roomPos.y - 2].GetComponent<Room>().isDiscovered)
				{
					rooms[i].isDiscovered = true;
				}
			}
			if (roomPos.x + 2 <= 6)
			{
				if (array[(int)roomPos.x + 1, (int)roomPos.y].GetComponent<Entrance>().isOpen &&
					array[(int)roomPos.x + 2, (int)roomPos.y].GetComponent<Room>().isDiscovered)
				{
					rooms[i].isDiscovered = true;
				}
			}
			if (roomPos.x - 2 >= 0)
			{
				if (array[(int)roomPos.x - 1, (int)roomPos.y].GetComponent<Entrance>().isOpen &&
					array[(int)roomPos.x - 2, (int)roomPos.y].GetComponent<Room>().isDiscovered)
				{
					rooms[i].isDiscovered = true;
				}
			}		
		}

		for (int i = 0; i < rooms.Length; i++)
		{
			if (!rooms[i].isDiscovered)
			{
				return false;
			}
		}

		return true;
	}

	private Vector2 RandomEntrancePosition(Vector2 position)
	{
		Vector2 randomPosition = new Vector2();

		bool mamePozici = false;
		while (!mamePozici)
		{
			int random = Random.Range(0, 4);
			switch (random)
			{
				case 0:
					if (position.y + 2 <= 6)
					{
						randomPosition = new Vector2(0, 2);
						mamePozici = true;
					}
					break;
				case 1:
					if (position.y - 2 >= 0)
					{
						randomPosition = new Vector2(0, -2);
						mamePozici = true;
					}
					break;
				case 2:
					if (position.x + 2 <= 6)
					{
						randomPosition = new Vector2(2, 0);
						mamePozici = true;
					}
					break;
				case 3:
					if (position.x - 2 >= 0)
					{
						randomPosition = new Vector2(-2, 0);
						mamePozici = true;
					}
					break;
			}
		}

		return randomPosition;
	}

	private void SetRoomTypes(Room[] rooms)
	{
		for (int i = 0; i < rooms.Length; i++)
		{
			Vector2 roomPos = rooms[i].position;
			int roomEntranceCounter = 0;

			if (roomPos.y + 2 <= 6)
			{
				if (array[(int)roomPos.x, (int)roomPos.y + 1].GetComponent<Entrance>().isOpen)
				{
					roomEntranceCounter += 9;
				}
			}
			if (roomPos.y - 2 >= 0)
			{
				if (array[(int)roomPos.x, (int)roomPos.y - 1].GetComponent<Entrance>().isOpen)
				{
					roomEntranceCounter += 1;
				}
			}
			if (roomPos.x + 2 <= 6)
			{
				if (array[(int)roomPos.x + 1, (int)roomPos.y].GetComponent<Entrance>().isOpen)
				{
					roomEntranceCounter += 4;
				}
			}
			if (roomPos.x - 2 >= 0)
			{
				if (array[(int)roomPos.x - 1, (int)roomPos.y].GetComponent<Entrance>().isOpen)
				{
					roomEntranceCounter += 16;
				}
			}


			rooms[i].SetRoomEntrances(roomEntranceCounter);
		}
	}

	//Vytvori 2D array transformu, vyplni rooms a nastavi kazde Room pozici
	private Transform[,] Create2DArray(Transform[] entranceTransforms, Transform[] roomTransforms)
	{
		Transform[,] array2D = new Transform[7, 7];
		int entranceIndex = 0;
		int roomIndex = 0;
		for (int y = 0; y < 7; y++)
		{
			for (int x = 0; x < 7; x++)
			{
				if ((x % 2 == 0) && (y % 2 == 0)) // Na miste je room
				{
					array2D[x, y] = roomTransforms[roomIndex];
					rooms[roomIndex] = roomTransforms[roomIndex].GetComponent<Room>();
					rooms[roomIndex].position = new Vector2(x, y);
					
					roomIndex++;
				}
				else if ((x % 2 != 0) && (y % 2 != 0)) // Na miste neni nic
				{
					array2D[x, y] = null;
				}
				else // Na miste je entrance
				{
					if (entranceIndex < entranceTransforms.Length)
					{
						array2D[x, y] = entranceTransforms[entranceIndex];
						//Debug.Log(x + ", " + y);
						entranceIndex++;
					}
				}
			}
		}

		return array2D;
	}
}
