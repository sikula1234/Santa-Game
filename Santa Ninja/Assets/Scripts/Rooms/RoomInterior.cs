using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomInterior : MonoBehaviour
{
	Room.roomEntranceTypes[] supportedEntranceTypes; 
	Room.roomEntrances[] supportedRoomEntrances;
	public Room.roomTypes roomType;
	public bool topEntrance;
	public bool rightEntrance;
	public bool bottomEntrance;
	public bool leftEntrance;
	public bool isSpawnRoom;

	private void Awake()
	{
		transform.rotation = Quaternion.AngleAxis(90, new Vector3(1, 0, 0));
	}

	// Zjisti, jestli je tento prefab vhodny pro mistnost na zaklade typu a vchodu
	public bool IsSuitable(Room.roomTypes roomType, Room.roomEntranceTypes roomEntranceType)
	{
		supportedEntranceTypes = SetSupportedEntranceTypes();

		if(this.roomType == roomType)
		{
			for(int i = 0; i < supportedEntranceTypes.Length; i++)
			{
				if(supportedEntranceTypes[i] == roomEntranceType)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Vygeneruje interier
	public void GenerateInterior(Room.roomEntranceTypes requestedRoomEntranceType, Room.roomEntrances requestedRoomEntrance)
	{
		//Vytvori arraye podle 4 booleanu
		supportedEntranceTypes = SetSupportedEntranceTypes();
		supportedRoomEntrances = SetSupportedRoomEntrances();

		Room.roomEntrances randomSupportedRoomEntrance = ChooseRandomEntrance(requestedRoomEntranceType);

		RotateInterior(requestedRoomEntrance, randomSupportedRoomEntrance);		
	}

	// Zmeni rotaci interieru tak aby sedela na vsechny vchody
	void RotateInterior(Room.roomEntrances requestedRoomEntrance, Room.roomEntrances randomSupportedRoomEntrance)
	{
		Vector3 modifier = GenerateRotationModifier(randomSupportedRoomEntrance);

		switch (requestedRoomEntrance)
		{
			case Room.roomEntrances.T:
				transform.Rotate(new Vector3(0, 0, 0) + modifier);
				break;
			case Room.roomEntrances.R:
				transform.Rotate(new Vector3(0, 0, -90) + modifier);
				break;
			case Room.roomEntrances.B:
				transform.Rotate(new Vector3(0, 0, -180) + modifier);
				break;
			case Room.roomEntrances.L:
				transform.Rotate(new Vector3(0, 0, -270) + modifier);
				break;
			case Room.roomEntrances.TB:
				transform.Rotate(new Vector3(0, 0, 0) + modifier);
				break;
			case Room.roomEntrances.RL:
				transform.Rotate(new Vector3(0, 0, -90) + modifier);
				break;
			case Room.roomEntrances.LTR:
				transform.Rotate(new Vector3(0, 0, 0) + modifier);
				break;
			case Room.roomEntrances.TRB:
				transform.Rotate(new Vector3(0, 0, -90) + modifier);
				break;
			case Room.roomEntrances.RBL:
				transform.Rotate(new Vector3(0, 0, -180) + modifier);
				break;
			case Room.roomEntrances.BLT:
				transform.Rotate(new Vector3(0, 0, -270) + modifier);
				break;
			case Room.roomEntrances.X:
				transform.Rotate(new Vector3(0, 0, 0) + modifier);
				break;
			case Room.roomEntrances.TR:
				transform.Rotate(new Vector3(0, 0, 0) + modifier);
				break;
			case Room.roomEntrances.RB:
				transform.Rotate(new Vector3(0, 0, -90) + modifier);
				break;
			case Room.roomEntrances.BL:
				transform.Rotate(new Vector3(0, 0, -180) + modifier);
				break;
			case Room.roomEntrances.LT:
				transform.Rotate(new Vector3(0, 0, -270) + modifier);
				break;
		}
	}

	// Zapise do array vsechny kombinace vstupu podle 4 bool entrance
	Room.roomEntrances[] SetSupportedRoomEntrances()
	{
		List<Room.roomEntrances> supportedEntrancesList = new List<Room.roomEntrances>();
		
		// Jeden vchod
		if (topEntrance)
			supportedEntrancesList.Add(Room.roomEntrances.T);
		if (rightEntrance)
			supportedEntrancesList.Add(Room.roomEntrances.R);
		if (bottomEntrance)
			supportedEntrancesList.Add(Room.roomEntrances.B);
		if (leftEntrance)
			supportedEntrancesList.Add(Room.roomEntrances.L);

		// Vsechny vchody
		if (topEntrance && rightEntrance && bottomEntrance && leftEntrance)
			supportedEntrancesList.Add(Room.roomEntrances.X);

		// Dva vchody do tvaru I
		if (topEntrance && bottomEntrance)
			supportedEntrancesList.Add(Room.roomEntrances.TB);
		if (rightEntrance && leftEntrance)
			supportedEntrancesList.Add(Room.roomEntrances.RL);

		// Dva vchody do tvaru L
		if (topEntrance && rightEntrance)
			supportedEntrancesList.Add(Room.roomEntrances.TR);
		if (rightEntrance && bottomEntrance)
			supportedEntrancesList.Add(Room.roomEntrances.RB);
		if (bottomEntrance && leftEntrance)
			supportedEntrancesList.Add(Room.roomEntrances.BL);
		if (leftEntrance && topEntrance)
			supportedEntrancesList.Add(Room.roomEntrances.LT);
		
		// Tri vchody do tvaru T
		if (leftEntrance && topEntrance && rightEntrance)
			supportedEntrancesList.Add(Room.roomEntrances.LTR);
		if (topEntrance && rightEntrance && bottomEntrance)
			supportedEntrancesList.Add(Room.roomEntrances.TRB);
		if (rightEntrance && bottomEntrance && leftEntrance)
			supportedEntrancesList.Add(Room.roomEntrances.RBL);
		if (bottomEntrance && leftEntrance && topEntrance)
			supportedEntrancesList.Add(Room.roomEntrances.BLT);

		// Prevod listu na array
		Room.roomEntrances[] roomEntrances = new Room.roomEntrances[supportedEntrancesList.Count()];
		for(int i = 0; i < roomEntrances.Length; i++)
		{
			roomEntrances[i] = supportedEntrancesList[i];
		}
		return roomEntrances;
	}

	// Zapise do array vsechny roomEntranceTypes podle 4 bool entrance
	Room.roomEntranceTypes[] SetSupportedEntranceTypes()
	{
		List<Room.roomEntranceTypes> supportedEntranceTypesList = new List<Room.roomEntranceTypes>();

		// Jeden vchod
		if (topEntrance || rightEntrance || bottomEntrance || leftEntrance)
		{
			supportedEntranceTypesList.Add(Room.roomEntranceTypes.oneEntrance);
		}

		// Vsechny vchody
		if (topEntrance && rightEntrance && bottomEntrance && leftEntrance)
			supportedEntranceTypesList.Add(Room.roomEntranceTypes.XEntrance);

		// Dva vchody do tvaru I
		if ((topEntrance && bottomEntrance) || (rightEntrance && leftEntrance))
		{
			supportedEntranceTypesList.Add(Room.roomEntranceTypes.oppositeEntrances);
		}

		// Dva vchody do tvaru L
		if((topEntrance && rightEntrance) ||
			(rightEntrance && bottomEntrance) ||
			(bottomEntrance && leftEntrance) ||
			(leftEntrance && topEntrance))
		{
			supportedEntranceTypesList.Add(Room.roomEntranceTypes.LEntrance);
		}

		// Tri vchody do tvaru T
		if((leftEntrance && topEntrance && rightEntrance) ||
			(topEntrance && rightEntrance && bottomEntrance) || 
			(rightEntrance && bottomEntrance && leftEntrance) ||
			(bottomEntrance && leftEntrance && topEntrance))
		{
			supportedEntranceTypesList.Add(Room.roomEntranceTypes.TEntrance);
		}

		// Prevod listu na array
		Room.roomEntranceTypes[] supportedEntranceTypes = new Room.roomEntranceTypes[supportedEntranceTypesList.Count()];
		for (int i = 0; i < supportedEntranceTypes.Length; i++)
		{
			supportedEntranceTypes[i] = supportedEntranceTypesList[i];
		}
		return supportedEntranceTypes;
	}

	// Vybere random entrance z supportedRoomEntrances arraye podle pozadovaneho typu entrance
	Room.roomEntrances ChooseRandomEntrance(Room.roomEntranceTypes roomEntranceType)
	{
		if (roomEntranceType == Room.roomEntranceTypes.oneEntrance)
		{
			List<Room.roomEntrances> roomEntranceGroup = new List<Room.roomEntrances>();
			for (int i = 0; i < supportedRoomEntrances.Length; i++)
			{
				if (supportedRoomEntrances[i] == Room.roomEntrances.T ||
					supportedRoomEntrances[i] == Room.roomEntrances.R ||
					supportedRoomEntrances[i] == Room.roomEntrances.B ||
					supportedRoomEntrances[i] == Room.roomEntrances.L)
				{
					roomEntranceGroup.Add(supportedRoomEntrances[i]);
				}
			}
			int random = Random.Range(0, roomEntranceGroup.Count());

			return roomEntranceGroup[random];
		}
		else if (roomEntranceType == Room.roomEntranceTypes.oppositeEntrances)
		{
			List<Room.roomEntrances> roomEntranceGroup = new List<Room.roomEntrances>();
			for (int i = 0; i < supportedRoomEntrances.Length; i++)
			{
				if (supportedRoomEntrances[i] == Room.roomEntrances.RL ||
					supportedRoomEntrances[i] == Room.roomEntrances.TB)
				{
					roomEntranceGroup.Add(supportedRoomEntrances[i]);
				}
			}
			int random = Random.Range(0, roomEntranceGroup.Count());

			return roomEntranceGroup[random];
		}
		else if (roomEntranceType == Room.roomEntranceTypes.LEntrance)
		{
			List<Room.roomEntrances> roomEntranceGroup = new List<Room.roomEntrances>();
			for (int i = 0; i < supportedRoomEntrances.Length; i++)
			{
				if (supportedRoomEntrances[i] == Room.roomEntrances.TR ||
					supportedRoomEntrances[i] == Room.roomEntrances.RB ||
					supportedRoomEntrances[i] == Room.roomEntrances.BL ||
					supportedRoomEntrances[i] == Room.roomEntrances.LT)
				{
					roomEntranceGroup.Add(supportedRoomEntrances[i]);
				}
			}
			int random = Random.Range(0, roomEntranceGroup.Count());

			return roomEntranceGroup[random];
		}
		else if (roomEntranceType == Room.roomEntranceTypes.TEntrance)
		{
			List<Room.roomEntrances> roomEntranceGroup = new List<Room.roomEntrances>();
			for (int i = 0; i < supportedRoomEntrances.Length; i++)
			{
				if (supportedRoomEntrances[i] == Room.roomEntrances.LTR ||
					supportedRoomEntrances[i] == Room.roomEntrances.TRB ||
					supportedRoomEntrances[i] == Room.roomEntrances.RBL ||
					supportedRoomEntrances[i] == Room.roomEntrances.BLT)
				{
					roomEntranceGroup.Add(supportedRoomEntrances[i]);
				}
			}
			int random = Random.Range(0, roomEntranceGroup.Count());

			return roomEntranceGroup[random];
		}
		else if (roomEntranceType == Room.roomEntranceTypes.XEntrance)
		{
			for (int i = 0; i < supportedRoomEntrances.Length; i++)
			{
				if (supportedRoomEntrances[i] == Room.roomEntrances.X)
				{
					return Room.roomEntrances.X;
				}
			}
		}
		
		return Room.roomEntrances.Default;
	}
		
	// Vygeneruje rotationModifier na zaklade typu entrance
	Vector3 GenerateRotationModifier(Room.roomEntrances roomEntrance)
	{
		Vector3 modifier = new Vector3(0, 0, 0);

		int random2 = Random.Range(0, 2);
		int random4 = Random.Range(0, 4);

		switch (roomEntrance)
		{
			case Room.roomEntrances.T:
				modifier = new Vector3(0, 0, 0);
				break;
			case Room.roomEntrances.R:
				modifier = new Vector3(0, 0, 90);
				break;
			case Room.roomEntrances.B:
				gameObject.transform.Rotate(new Vector3(0, 0, 180));
				break;
			case Room.roomEntrances.L:
				gameObject.transform.Rotate(new Vector3(0, 0, 270));
				break;
			case Room.roomEntrances.TB:				
				if(random2 == 0)
				{
					gameObject.transform.Rotate(new Vector3(0, 0, 0));
				} else
				{
					gameObject.transform.Rotate(new Vector3(0, 0, -180));
				}				
				break;
			case Room.roomEntrances.RL:				
				if (random2 == 0)
				{
					gameObject.transform.Rotate(new Vector3(0, 0, 90));
				}
				else
				{
					gameObject.transform.Rotate(new Vector3(0, 0, -90));
				}
				break;
			case Room.roomEntrances.LTR:
				modifier = new Vector3(0, 0, 0);
				break;
			case Room.roomEntrances.TRB:
				gameObject.transform.Rotate(new Vector3(0, 0, 90));
				break;
			case Room.roomEntrances.RBL:
				gameObject.transform.Rotate(new Vector3(0, 0, 180));
				break;
			case Room.roomEntrances.BLT:
				gameObject.transform.Rotate(new Vector3(0, 0, 270));
				break;
			case Room.roomEntrances.X:				
				switch(random4)
				{
					case 0: modifier = new Vector3(0, 0, 0); break;
					case 1: modifier = new Vector3(0, 0, -90); break;
					case 2: modifier = new Vector3(0, 0, -180); break;
					case 3: modifier = new Vector3(0, 0, -270); break;
				}
				break;
			case Room.roomEntrances.TR:
				modifier = new Vector3(0, 0, 0);
				break;
			case Room.roomEntrances.RB:
				gameObject.transform.Rotate(new Vector3(0, 0, 90));
				break;
			case Room.roomEntrances.BL:
				gameObject.transform.Rotate(new Vector3(0, 0, 180));
				break;
			case Room.roomEntrances.LT:
				gameObject.transform.Rotate(new Vector3(0, 0, 270));
				break;
		}

		return modifier;
	} 

}
