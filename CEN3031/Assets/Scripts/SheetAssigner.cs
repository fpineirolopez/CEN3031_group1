using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetAssigner : MonoBehaviour {
	[SerializeField]
	Texture2D[] sheetsNormal;//Takes in a set of room templates to choose the room layout from. This can be modified in MANY ways.
	[SerializeField]
	GameObject[] RoomObj;//Stores the floor of the room which is what also stands as the 'root' of the room. 
    //Planning on changing to an array of room templates to choose room layout.

    // To modify the below values, must do so in inspector instead of modifying it in here.
    //23x15
	public Vector2 roomDimensions = new Vector2(16*23,16*15);//Hard coded room dimensions, 17x9 in original
	public Vector2 gutterSize = new Vector2(16*4,16*4);//Hard coded gutter size

	public void Assign(Room[,] rooms){
		foreach (Room room in rooms){
			//skip point where there is no room
			if (room == null){
				continue;
			}
			//pick a random index for the array
			int index = Mathf.RoundToInt(Random.value * (sheetsNormal.Length -1));

            int rootIndex = Mathf.RoundToInt(Random.value * (RoomObj.Length - 1));//Randomize the room root object.

			//find position to place room
			Vector3 pos = new Vector3(room.gridPos.x * (roomDimensions.x + gutterSize.x), room.gridPos.y * (roomDimensions.y + gutterSize.y), 0);
            RoomInstance myRoom = Instantiate(RoomObj[rootIndex], pos, Quaternion.identity).GetComponent<RoomInstance>();
            if (room.type == 1){
                myRoom.Setup(sheetsNormal[0], room.gridPos, room.type, room.doorTop, room.doorBot, room.doorLeft, room.doorRight);
            }
            else{
			    myRoom.Setup(sheetsNormal[index], room.gridPos, room.type, room.doorTop, room.doorBot, room.doorLeft, room.doorRight);
            }
		}
	}
}
