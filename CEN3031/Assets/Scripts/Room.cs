using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to hold very general room information. 
//Types: 0 = Normal room, 1 = Starting Room/Empty Room, 2 = Puzzle Room, and 3 = Boss Room.
//Has method getOpenSide: 0 = Does not exist, 1 = Up, 2 = Down, 3 = Left, 4 = Right
//

public class Room {
	public Vector2 gridPos;
	public int type, gridSizeX, gridSizeY;
	public bool doorTop, doorBot, doorLeft, doorRight;//These values default to 0.
    public Room(Vector2 _gridPos, int _type){
		gridPos = _gridPos;
		type = _type;
        doorTop = false;
        doorBot = false;
        doorLeft = false;
        doorRight = false;
	}

    public int getOpenSide(int gridSizeX, int gridSizeY){
        //Randomly select a side that is open by checking its booleans, then and return it; otherwise return 0.
        //This also detects if the candidate coordinate is within the bounds of the world dimensions.
        List<int> openSides = new List<int>();
        if (!doorTop){
            if(gridSizeY + gridPos.y + 1 < gridSizeY*2)
                openSides.Add(1);
        }
        if (!doorBot){
            if(gridSizeY + gridPos.y - 1 >= 0)
                openSides.Add(2);
        }
        if (!doorLeft){
            if (gridSizeX + gridPos.x - 1 >= 0)
                openSides.Add(3);
        }
        if (!doorRight){
            if (gridSizeX + gridPos.x + 1 < gridSizeX*2)
                openSides.Add(4);
        }

        if (openSides.Count == 0)
            return 0;

        int randIndex = Mathf.RoundToInt(Random.value * (openSides.Count - 1));
        return openSides[randIndex];
    }
}
