using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInterior : MonoBehaviour
{
	public Room.roomEntranceTypes[] supportedEntranceTypes;
	public Room.roomTypes roomType;

	public bool IsSuitable(Room.roomTypes roomType, Room.roomEntranceTypes roomEntranceType)
	{
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
}
