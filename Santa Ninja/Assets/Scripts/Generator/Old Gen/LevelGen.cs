using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
	//Spelunky
	private int direction;
	private int downCounter;
	public float moveAmount = 2;
	public float minX = 0;
	public float maxX = 6;
	public float minY = 0;
	public bool stopGeneration;
	private float timeBtwRoom;
	public float startTimeBtwRoom = 0.25f;
	//Spelunky

	public Transform[] entranceTransforms;
	public Transform[] roomTransforms;
	Vector2 genPos;

	Room[] rooms;
	Transform[,] transformArray;

	// Awake is called before Start
	void Awake()
	{
		transformArray = Create2DArray(entranceTransforms, roomTransforms);
		rooms = ConvertToRoom(roomTransforms);

		//GenerateFirst5();
		//GenerateOthers();
		//GenerateRest();
	}

	// Start is called before the first frame update
	void Start()
	{
		//GenerateRest();
	}

	// Update is called once per frame
	void Update()
	{
		if (timeBtwRoom <= 0 && stopGeneration == false)
		{
			MoveSpelunky();
			timeBtwRoom = startTimeBtwRoom;
		}
		else
		{
			timeBtwRoom -= Time.deltaTime;
		}
	}

	//Predela array na 2D array
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
					roomTransforms[roomIndex].GetComponent<Room>().position = new Vector2(x, y);
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
						Debug.Log(x + ", " + y);
						entranceIndex++;
					}
				}
			}
		}

		return array2D;
	}

	//Priradi mistnosti randomni typ
	private void GenerateFirst5()
	{
		// Vybere random mistnost, oznaci ji za objevenou
		int rand = Random.Range(0, 15);
		genPos = rooms[rand].position;
		rooms[rand].isDiscovered = true;

		//For cycle i < 5
		//Vybere random smer (krome toho, kde je objevena mistnost)
		//a udela tim smerem diru, 
		//posune se tam
		for (int i = 0; i < 5; i++)
		{
			Move(false);
		}
	}

	private void GenerateOthers()
	{
		Debug.Log("Success!");
	}

	private void GenerateRest()
	{
		/*
		//For cycle pro kazdou neobjevenou mistnost
		//Udela diru k objevene mistnosti - pokud tam je
		//Udela 1-2 diry random smerem - pokud tam neni
		for (int i = 0; i < 16; i++)
		{
			if (rooms[i].isDiscovered == false)
			{
				genPos = rooms[i].position;
				//Debug.Log("genPos:" + genPos.x + ", " + genPos.y);
				//MoveOnDiscovered();
			}
		} */
	}

	public void MoveOnDiscovered()
	{

		Vector2 position = genPos;
		Vector2 entrancePosition;

		if (position.y + 2 <= 6)
		{
			if (transformArray[(int)position.x, (int)position.y + 2].GetComponent<Room>().isDiscovered == true)
			{
				entrancePosition = new Vector2(position.x, position.y + 1);
				genPos = new Vector2(position.x, position.y + 2);
				transformArray[(int)entrancePosition.x, (int)entrancePosition.y].GetComponent<Entrance>().isOpen = true;
			}
		}
		else if (position.y - 2 >= 0)
		{
			if (transformArray[(int)position.x, (int)position.y - 2].GetComponent<Room>().isDiscovered == true)
			{
				entrancePosition = new Vector2(position.x, position.y - 1);
				genPos = new Vector2(position.x, position.y - 2);
				transformArray[(int)entrancePosition.x, (int)entrancePosition.y].GetComponent<Entrance>().isOpen = true;
			}
		}
		else if (position.x + 2 <= 6)
		{
			if (transformArray[(int)position.x + 2, (int)position.y].GetComponent<Room>().isDiscovered == true)
			{
				entrancePosition = new Vector2(position.x + 1, position.y);
				genPos = new Vector2(position.x + 2, position.y);
				transformArray[(int)entrancePosition.x, (int)entrancePosition.y].GetComponent<Entrance>().isOpen = true;
			}
		}
		else if (position.x - 2 >= 0)
		{
			if (transformArray[(int)position.x - 2, (int)position.y].GetComponent<Room>().isDiscovered == true)
			{
				entrancePosition = new Vector2(position.x - 1, position.y);
				genPos = new Vector2(position.x - 2, position.y);
				transformArray[(int)entrancePosition.x, (int)entrancePosition.y].GetComponent<Entrance>().isOpen = true;
			}
		}
		else
		{
			Move(false);
		}
	}


	//Posune se na zadanou pozici, udela cestou diru ve zdi a oznaci novou mistnost za objevenou 
	private void Move(bool moveOnDiscovered)
	{
		//Vector2 oldPos = genPos;
		Vector2 entrancePosition = RandomDirectionPosition(genPos, moveOnDiscovered);
		//Debug.Log("genPos:" + genPos.x + ", " + genPos.y);
		//Debug.Log("oldPos:" + oldPos.x + ", " + oldPos.y);
		//Debug.Log("entrancePosition:" + entrancePosition.x + ", " + entrancePosition.y);

		transformArray[(int)genPos.x, (int)genPos.y].GetComponent<Room>().isDiscovered = true;
		transformArray[(int)entrancePosition.x, (int)entrancePosition.y].GetComponent<Entrance>().isOpen = true;
	}

	//Vygeneruje nahodnou pozici entrance kolem zadane pozice gen
	//Pokud tam uz je discovered mistnost, tak jede znovu
	private Vector2 RandomDirectionPosition(Vector2 position, bool moveOnDiscovered)
	{
		Vector2 randomPosition = new Vector2();
		bool mamePozici = false;
		while (!mamePozici)
		{
			int random = Random.Range(0, 3);
			switch (random)
			{
				case 0:
					if (position.y + 2 <= 6)
					{
						//if (transformArray[(int)position.x, (int)position.y + 2].GetComponent<Room>().isDiscovered == false)
						//{
							randomPosition = new Vector2(position.x, position.y + 1);
							genPos = new Vector2(position.x, position.y + 2);
							mamePozici = true;
						//}
					}
					break;
				case 1:
					if (position.y - 2 >= 0)
					{
						//if (transformArray[(int)position.x, (int)position.y - 2].GetComponent<Room>().isDiscovered == false)
						//{
							randomPosition = new Vector2(position.x, position.y - 1);
							genPos = new Vector2(position.x, position.y - 2);
							mamePozici = true;
						//}
					}
					break;
				case 2:
					if (position.x + 2 <= 6)
					{
						//if (transformArray[(int)position.x + 2, (int)position.y].GetComponent<Room>().isDiscovered == false)
						//{
							randomPosition = new Vector2(position.x + 1, position.y);
							genPos = new Vector2(position.x + 2, position.y);
							mamePozici = true;
						//}
					}
					break;
				case 3:
					if (position.x - 2 >= 0)
					{
						//if (transformArray[(int)position.x - 2, (int)position.y].GetComponent<Room>().isDiscovered == false)
						//{
							randomPosition = new Vector2(position.x - 1, position.y);
							genPos = new Vector2(position.x - 2, position.y);
							mamePozici = true;
						//}
					}
					break;
			}
		}

		return randomPosition;
	}

	private Vector2 RandomDirectionPosition2(Vector2 position, bool moveOnDiscovered)
	{
		Vector2 randomPosition = new Vector2();
		bool mamePozici = false;
		bool top = false;
		bool bottom = false;
		bool left = false;
		bool right = false;
		List<int> moznePozice = new List<int>();


				if (position.y + 2 <= 6)
				{
					if (transformArray[(int)position.x, (int)position.y + 2].GetComponent<Room>().isDiscovered == false)
					{
					randomPosition = new Vector2(position.x, position.y + 1);
					genPos = new Vector2(position.x, position.y + 2);

					top = true;
					}
				}

				if (position.y - 2 >= 0)
				{
					//if (transformArray[(int)position.x, (int)position.y - 2].GetComponent<Room>().isDiscovered == false)
					//{
					randomPosition = new Vector2(position.x, position.y - 1);
					genPos = new Vector2(position.x, position.y - 2);
					bottom = true;
					//}
				}

				if (position.x + 2 <= 6)
				{
					//if (transformArray[(int)position.x + 2, (int)position.y].GetComponent<Room>().isDiscovered == false)
					//{
					randomPosition = new Vector2(position.x + 1, position.y);
					genPos = new Vector2(position.x + 2, position.y);
					right = true;
					//}
				}

				if (position.x - 2 >= 0)
				{
					//if (transformArray[(int)position.x - 2, (int)position.y].GetComponent<Room>().isDiscovered == false)
					//{
					randomPosition = new Vector2(position.x - 1, position.y);
					genPos = new Vector2(position.x - 2, position.y);
					left = true;
					//}
				}

		


		while (!mamePozici)
		{
			int random = Random.Range(0, 3);
			switch (random)
			{
				case 0:
					if (position.y + 2 <= 6)
					{
						//if (transformArray[(int)position.x, (int)position.y + 2].GetComponent<Room>().isDiscovered == false)
						//{
						randomPosition = new Vector2(position.x, position.y + 1);
						genPos = new Vector2(position.x, position.y + 2);
						mamePozici = true;
						//}
					}
					break;
				case 1:
					if (position.y - 2 >= 0)
					{
						//if (transformArray[(int)position.x, (int)position.y - 2].GetComponent<Room>().isDiscovered == false)
						//{
						randomPosition = new Vector2(position.x, position.y - 1);
						genPos = new Vector2(position.x, position.y - 2);
						mamePozici = true;
						//}
					}
					break;
				case 2:
					if (position.x + 2 <= 6)
					{
						//if (transformArray[(int)position.x + 2, (int)position.y].GetComponent<Room>().isDiscovered == false)
						//{
						randomPosition = new Vector2(position.x + 1, position.y);
						genPos = new Vector2(position.x + 2, position.y);
						mamePozici = true;
						//}
					}
					break;
				case 3:
					if (position.x - 2 >= 0)
					{
						//if (transformArray[(int)position.x - 2, (int)position.y].GetComponent<Room>().isDiscovered == false)
						//{
						randomPosition = new Vector2(position.x - 1, position.y);
						genPos = new Vector2(position.x - 2, position.y);
						mamePozici = true;
						//}
					}
					break;
			}
		}

		return randomPosition;
	}


	public Room[] ConvertToRoom(Transform[] transforms)
	{
		Room[] rooms = new Room[transforms.Length];
		for (int i = 0; i < transforms.Length; i++)
		{
			rooms[i] = transforms[i].GetComponent<Room>();
		}
		return rooms;
	}

	private void MoveSpelunky()
	{
		if (direction == 1 || direction == 2) // Move RIGHT!
		{
			if (genPos.x < maxX)
			{
				downCounter = 0;
				Vector2 newPos = new Vector2(genPos.x + moveAmount, genPos.y);
				genPos = newPos;

				//int rand = Random.Range(0, rooms.Length);
				//Instantiate(rooms[rand], transform.position, Quaternion.identity);

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
				Vector2 newPos = new Vector2(genPos.x - moveAmount, genPos.y);
				genPos = newPos;

				//int rand = Random.Range(0, rooms.Length);
				//Instantiate(rooms[rand], transform.position, Quaternion.identity);

				direction = Random.Range(3, 6);
			}
			else
			{
				direction = 5;
			}

		}
		else if (direction == 5)
		{
			downCounter++;

			if (genPos.y > minY)
			{
				/*
				Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
				if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
				{
					if (downCounter >= 2)
					{
						roomDetection.GetComponent<RoomType>().RoomDestruction();
						Instantiate(rooms[3], transform.position, Quaternion.identity);
					}
					else
					{
						roomDetection.GetComponent<RoomType>().RoomDestruction();

						int randBottomRoom = Random.Range(1, 4);
						if (randBottomRoom == 2)
						{
							randBottomRoom = 1;
						}
						Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
					}

				}

				Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
				transform.position = newPos;

				int rand = Random.Range(2, 4);
				Instantiate(rooms[rand], transform.position, Quaternion.identity);

				direction = Random.Range(1, 6); */
			}
			else
			{
				stopGeneration = true;
			}
		}
	}
}
