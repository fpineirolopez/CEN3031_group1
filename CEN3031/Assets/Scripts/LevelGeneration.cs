using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credit to Six Dot Studios on Youtube for providing the base code structure and logic.
//Some parts of the algorithm has been used while a large majority has been modified to change the way that it operates.

//Note that all room coordinates are simplified to integers.
/*
Everything feels backwards concerning indices because Unity and array notation are different.
In array notation, for instance, negative y means up, but in Unity negative y means down.
So we are treating the C# code indices like unity indices and using + and - in a literal sense.
 * 
  */
public class LevelGeneration : MonoBehaviour {
	Vector2 worldSize = new Vector2(4,4);//Half Sizes, 64 possible spots.
    [HideInInspector]
	public Room[,] rooms;//2D array of rooms to store general world info. This will be used to manage adjacency as well. This shouldn't actually be public but it is kept this way for testing purposes.
    List<Vector2> edgePositions;//List of all positions that have open neighbors for floor generation.
    int gridSizeX, gridSizeY;
    public int numberOfRooms = 10;//We'll make this public, default to 10.
	public GameObject roomWhiteObj;//The central room object for drawing minimap. Calls MapSpriteSelector after being Instantiated.
	public Transform mapRoot;

    public static LevelGeneration instance = null;
   
    int level;

    void Awake(){
        if (instance == null)
            instance = this;
        else if (instance != this){
            Destroy(gameObject);
            return;
        }

        //Debug.Log("Level Generation Created");
        DontDestroyOnLoad(gameObject);
        level = 0;

        InitLevel();
    }

//	void Start () no longer needed.

    //This is called every time the scene is reloaded.
    void OnLevelWasLoaded(int index){
        if (this != instance)
            return;
        //Debug.Log("Loading Level " + level);
        numberOfRooms = 10 + level++;
        if (numberOfRooms > 50)
            numberOfRooms = 50;
        InitLevel();
    }

    void InitLevel(){
        edgePositions = new List<Vector2>();//Initialize edge position list.
            
        gridSizeX = (int) worldSize.x;//Stores half-size of map, we'll hard code this to 4..
        gridSizeY = (int) worldSize.y;

        if (numberOfRooms > gridSizeX * gridSizeY * 4)
            numberOfRooms = 50;//For now fix to fifty if trying to exceed the capacities.

        CreateRooms(); //lays out the actual map
        //DrawMap(); //instantiates objects to make up a map; Minimap will be edited/moved/removed later.

        //Comment Out the following when running tests, as the sheet assigner is not needed.
        GetComponent<SheetAssigner>().Assign(rooms); //passes room info to another script which handles generatating the level geometry
    }

    //Create room objects for the room array.
	void CreateRooms(){
		//setup
		rooms = new Room[gridSizeX * 2,gridSizeY * 2];//Define the array of rooms to 2 times worldSize.
        rooms[gridSizeX,gridSizeY] = new Room(Vector2.zero, 1);//Place at center, which is at origin.
        edgePositions.Insert(0,Vector2.zero);//Mark starting room as having open sides.
		Vector2 checkPos = Vector2.zero;//Initialize the vector that stores where the next room will be placed.
		for (int i = 0; i < numberOfRooms-1; i++){//numberOfRooms-1 because first room was alread placed.
			//grab new position
            checkPos = NewPosition();
			//Create a new room at the designate position.
			rooms[(int) checkPos.x + gridSizeX, (int) checkPos.y + gridSizeY] = new Room(checkPos, 0);//Create new room in corresponding place in array.
            updateDoors(checkPos);//Update door booleans for current and adjacent rooms.
            if (rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY].getOpenSide(gridSizeX,gridSizeY) != 0)
                edgePositions.Insert(0, checkPos);//Add to list of edges if there is an available side.
        }
	}
        
    //New position is given as half-sizes. Must add world size to make it room coordinates.
	Vector2 NewPosition(){//Selects room that has open sides and returns the coordinates.
		Vector2 newPos = Vector2.zero;
        int relativeLoc, index, x, y;
        while (true){
            index = Mathf.RoundToInt(Random.value * (edgePositions.Count - 1)); // pick a random room that has an open side.
            x = (int)edgePositions[index].x;
            y = (int)edgePositions[index].y;
            relativeLoc = rooms[x+gridSizeX, y+gridSizeY].getOpenSide(gridSizeX, gridSizeY);//Get integer that designates direction.
            if (relativeLoc == 0){
                edgePositions.RemoveAt(index);//Eliminate from the edges list if this one doesn't have any adjacent openings.
                continue;
            }
            if (relativeLoc == 1)      //Top
                y += 1;
            else if (relativeLoc == 2) //Bottom
                y -= 1;
            else if (relativeLoc == 3) //Left
                x -= 1;
            else                       //Right
                x += 1;
            newPos = new Vector2(x, y);
            return newPos;
        }
	}
     

    //Validate if given coordinates represent a room or not.
    bool IsValidRoom(int x, int y){
        if (x >= gridSizeX*2 || x < 0 || y >= gridSizeY*2 || y < 0){
            return false;//Check if the coordinates are within the bounds of the room grid.
        }
        else {//If this does not point to a room, return false.
            if (rooms[x,y] == null)
                return false;
            return true;//Otherwise the given coordinates are rooms, return true.
        }
    }

    //Called to update the surrounding rooms of the added room to set door booleans.
    void updateDoors(Vector2 addedRoom){
        int x = (int)addedRoom.x + gridSizeX;
        int y = (int)addedRoom.y + gridSizeY;
        //Check four directions for doors, updating as needed.
        if (IsValidRoom(x, y+1)){//top
            if (rooms[x, y+1] != null){
                rooms[x,y].doorTop = true;
                rooms[x, y+1].doorBot = true;
            }
        }
        if (IsValidRoom(x,y-1)){//bottom
            if (rooms[x, y-1] != null){
                rooms[x, y].doorBot = true;
                rooms[x, y-1].doorTop = true;
            }
        }
        if (IsValidRoom(x-1, y)){//left
            if (rooms[x-1, y] != null){
                rooms[x, y].doorLeft = true;
                rooms[x-1, y].doorRight = true;
            }
        }
        if (IsValidRoom(x+1, y)){//right
            if (rooms[x+1, y] != null){
                rooms[x, y].doorRight = true;
                rooms[x+1, y].doorLeft = true;
            }
        }
    }


    //This method draws the minimap. This may or may not be actually used; Ignore for now.
    void DrawMap(){
        foreach (Room room in rooms){
            if (room == null){
                continue; //skip where there is no room
            }
            Vector2 drawPos = room.gridPos;
            drawPos.x *= 16;//aspect ratio of map sprite
            drawPos.y *= 8;
            //create map obj and assign its variables
            MapSpriteSelector mapper = Object.Instantiate(roomWhiteObj, drawPos, Quaternion.identity).GetComponent<MapSpriteSelector>();
            mapper.type = room.type;
            mapper.up = room.doorTop;
            mapper.down = room.doorBot;
            mapper.right = room.doorRight;
            mapper.left = room.doorLeft;
            mapper.gameObject.transform.parent = mapRoot;
        }
    }

}
