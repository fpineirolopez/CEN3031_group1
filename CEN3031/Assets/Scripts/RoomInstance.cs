using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script doesn't have too much to change as far as the logic goes, but there is plenty of room to add things.
//Most definitely it is necessary to add:
//-Room Clear Conditions
//-Room-type based variation of room layouts (ex. fix certain types of room layout for types of rooms)
//-Run Door Open/Close Scripts
//-Spawn items, enemies, and any other special things.
//-Must add a few more logical things to the template files to accomodate for corners, larger-than-one-tile blocks, and other stuff like that.

public class RoomInstance : MonoBehaviour {
	public Texture2D tex;
	[HideInInspector]
	public Vector2 gridPos;
	public int type; // 0: enemy-spawning, 1: starting room
	[HideInInspector]
	public bool doorTop, doorBot, doorLeft, doorRight;
	[SerializeField]
    GameObject[] doorObjects = new GameObject[4], doorClosedObjects = new GameObject[4], doorWalls = new GameObject[4];
	[SerializeField]
	ColorToGameObject[] mappings;
    [SerializeField]
    GameObject crackedWall;

    [SerializeField]
    GameObject[] enemies;

    GameObject[] doorInstances = new GameObject[4];

    [SerializeField]
    GameObject[] cornerWalls = new GameObject[2];

    [SerializeField]
    GameObject exitObject;

    Color black = new Color(0, 0, 0);
    Color white = new Color(1,1,1);
    Color blue = new Color(0,0,1);//Blue tiles refer to the wall tile. Use to place cracked wall at random.
    float tileSize = 16;

    List<Vector3> exitSpawnLocations = new List<Vector3>();
    List<Vector3> enemySpawnLocations = new List<Vector3>();

    bool isCleared;//Boolean to check if this room has been cleared by the player.
    bool isInRoom;//Boolean to check if the player is currently inside of this room.

    LevelGeneration levelGenManager;

    Vector2 roomSizeInTiles = new Vector2(15,23);//(15, 23)
	public void Setup(Texture2D _tex, Vector2 _gridPos, int _type, bool _doorTop, bool _doorBot, bool _doorLeft, bool _doorRight){
		tex = _tex;
		gridPos = _gridPos;
		type = _type;
		doorTop = _doorTop;
		doorBot = _doorBot;
		doorLeft = _doorLeft;
		doorRight = _doorRight;
        isInRoom = false;

        if(type == 1)//If type == 1, then this is the starting room; otherwise it is a standard room w/ clear condition
            isCleared = true;
        else
            isCleared = false;

        levelGenManager = GameObject.Find("LevelGenerator").GetComponent<LevelGeneration>();

		MakeDoors();
		GenerateRoomTiles();
	}

    void Update(){
        if (!isInRoom)
            return;
        if (isCleared)
            return;

        //If(levelGenManager.IsClearOfEnemies())
        if (Input.GetKeyDown("c")){//DEBUG ONLY, hit c to force clear the room. This isCleared -> MakeDoors flow will be the same.
            isCleared = true;
            RemakeDoors();//Use remake to avoid instantiating excess walls. DON'T hit again to avoid remaking doors.
            levelGenManager.clearedRoom();
        }

    }

    void OnTriggerEnter2D(Collider2D collid){//Use collider to detect when the player is in the room.
        if (collid.gameObject.tag != "Player")
            return;
        isInRoom = true;
        if (isCleared)
            return;
        MakeClosedDoors();
        //Spawn enemies
    }

    void OnTriggerExit2D(Collider2D collid){
        if (collid.gameObject.tag != "Player")
            return;
        isInRoom = false;
    }


    void SpawnEnemies(){
        //It may be sensible to pause for ~.5 seconds. Thus you may want to yield IEnumerator...
        /*Get instance of enemy spawner, most likely attached to the level generator gameobject.
        Randomly select one of the enemy types.
        int numberOfEnemies = instance.getCount(level);
        levelGenManager.addEnemy(numberOfEnemies);
        for(int i = 0; i < numberOfEnemies; i++){
            int index = Mathf.RoundToInt(Random.value * (EnemySpawnLocations.Count - 1))
            Instantiate(enemyPrefab, enemySpawnLocations[index], Quaternion.Identity);
        }
        */
    }

    void SpawnBoss(){
        //Fixed spawn location? Center?
        //Instantiate(bossPrefab, center(?), Quaternion.Identity);
    }

    //Exit will only spawn in the white space in the room template. As of now, it is only the first empty room.
    public void SpawnWarp(){
        if (type != 1)
            return;
        int index = Mathf.RoundToInt(Random.value * (exitSpawnLocations.Count - 1));
        Instantiate(exitObject, exitSpawnLocations[index], Quaternion.identity).transform.parent = transform;

    }

    //So the previous calculations don't work because the dimensions are no longer (n, 2n-1). Therefore it is better to just take the dimension and shift is as needed.
    //To find the location around the end of the room, simply divide the appropriate dimension by two and offset by half a tile. This offset is needed because the origin is 1/2 off to begin with.
	void MakeDoors(){
        destroyDoors();//Destroy any doors in place now.
		//top door, get position then spawn
        Vector3 spawnPos = transform.position + Vector3.up*(roomSizeInTiles.x/2 * tileSize) - Vector3.up*(tileSize/2);
		PlaceDoor(spawnPos, doorTop, 1);
		//bottom door
		spawnPos = transform.position + Vector3.down*(roomSizeInTiles.x/2 * tileSize) - Vector3.down*(tileSize/2);
		PlaceDoor(spawnPos, doorBot, 2);
		//right door
		spawnPos = transform.position + Vector3.right*(roomSizeInTiles.y/2 * tileSize) - Vector3.right*(tileSize/2);
		PlaceDoor(spawnPos, doorRight, 4);
		//left door
        spawnPos = transform.position + Vector3.left*(roomSizeInTiles.y/2 * tileSize) - Vector3.left*(tileSize/2);
		PlaceDoor(spawnPos, doorLeft, 3);
	}

    //Instantiates the appropriate type of door or wall, depending on the type.
    void PlaceDoor(Vector3 spawnPos, bool door, int doorType){
        // check whether its a door or wall, then spawn
        if (door){
            doorInstances[doorType-1] = Instantiate(doorObjects[doorType-1], spawnPos, Quaternion.identity);//Instantiate door.
            doorInstances[doorType-1].SendMessage("setDir", doorType);//Use sendmessage to tell door which type of door it is.
            doorInstances[doorType-1].transform.parent = transform;
        }else{
            doorInstances[doorType - 1] = null;
            Instantiate(doorWalls[doorType-1], spawnPos, Quaternion.identity).transform.parent = transform;
        }
    }

    //Similar to the make doors, but 
    void MakeClosedDoors(){
        destroyDoors();
        //top door, get position then spawn
        Vector3 spawnPos = transform.position + Vector3.up*(roomSizeInTiles.x/2 * tileSize) - Vector3.up*(tileSize/2);
        PlaceClosedDoor(spawnPos, doorTop, 1);
        //bottom door
        spawnPos = transform.position + Vector3.down*(roomSizeInTiles.x/2 * tileSize) - Vector3.down*(tileSize/2);
        PlaceClosedDoor(spawnPos, doorBot, 2);
        //right door
        spawnPos = transform.position + Vector3.right*(roomSizeInTiles.y/2 * tileSize) - Vector3.right*(tileSize/2);
        PlaceClosedDoor(spawnPos, doorRight, 4);
        //left door
        spawnPos = transform.position + Vector3.left*(roomSizeInTiles.y/2 * tileSize) - Vector3.left*(tileSize/2);
        PlaceClosedDoor(spawnPos, doorLeft, 3);
    }

    //Similar to above PlaceDoor, but only instantiates if the door actually exists.
    void PlaceClosedDoor(Vector3 spawnPos, bool door, int doorType){
        if (door){
            doorInstances[doorType-1] = Instantiate(doorClosedObjects[doorType-1], spawnPos, Quaternion.identity);//Instantiate door.
            doorInstances[doorType-1].transform.parent = transform;
        }
    }

    //Called to destroy all of the spawned door instances. This must be called after the doors have been created. 
    void destroyDoors(){
        for (int i = 0; i < 4; i++){
            if (doorInstances[i] != null){
                Destroy(doorInstances[i]);
                doorInstances[i] = null;
            }
        }
    }

    void RemakeDoors(){
        destroyDoors();//Destroy any doors in place now.
        //top door, get position then spawn
        Vector3 spawnPos = transform.position + Vector3.up*(roomSizeInTiles.x/2 * tileSize) - Vector3.up*(tileSize/2);
        ReplaceOpenDoor(spawnPos, doorTop, 1);
        //bottom door
        spawnPos = transform.position + Vector3.down*(roomSizeInTiles.x/2 * tileSize) - Vector3.down*(tileSize/2);
        ReplaceOpenDoor(spawnPos, doorBot, 2);
        //right door
        spawnPos = transform.position + Vector3.right*(roomSizeInTiles.y/2 * tileSize) - Vector3.right*(tileSize/2);
        ReplaceOpenDoor(spawnPos, doorRight, 4);
        //left door
        spawnPos = transform.position + Vector3.left*(roomSizeInTiles.y/2 * tileSize) - Vector3.left*(tileSize/2);
        ReplaceOpenDoor(spawnPos, doorLeft, 3);
    }

    //Places the doors again, this time by ignoring the walls unlike the PlaceOpenDoor method.
    void ReplaceOpenDoor(Vector3 spawnPos, bool door, int doorType){
        if (door){
            doorInstances[doorType-1] = Instantiate(doorObjects[doorType-1], spawnPos, Quaternion.identity);//Instantiate door.
            doorInstances[doorType-1].SendMessage("setDir", doorType);//Use sendmessage to tell door which type of door it is.
            doorInstances[doorType-1].transform.parent = transform;
        }
    }

    //Should loop backwards from top to bottom for crates to line up correctly.
	void GenerateRoomTiles(){
		//loop through every pixel of the texture
		for(int x = 0; x < tex.width; x++){
            for (int y = tex.height-1; y >= 0; y--){
                GenerateTile(x, y);
			}
		}
	}

    //Structure of tex:
    //It turns out that the template file is read with coordinates (x, y) which has origins at the bottom left corner of the image.
    //Because of this, it feels like a regular cartesian coordinate system, which isn't exactly how array notation works. However, it's nearly identical, but the original code only sidestepped it...

    void GenerateTile(int x, int y){
		Color pixelColor = tex.GetPixel(x,y);
		//skip clear spaces in texture
		if (pixelColor.a == 0){
			return;
		}


		//find the color to match the pixel
		foreach (ColorToGameObject mapping in mappings){
			if (mapping.color.Equals(pixelColor)){
                Vector3 spawnPos = positionFromTileGrid(x, y);
                if (mapping.color.Equals(black)){
                    enemySpawnLocations.Insert(0, spawnPos);
                    return;
                }
                else if (mapping.color.Equals(white)){
                    exitSpawnLocations.Insert(0, spawnPos);
                    return;
                }
                else{
                    if (mapping.color.Equals(blue)){//If this is a wall tile
                        if (Random.Range(0.0f, 1.0f) < .95f){//95% chance that this will be a cracked wall.
                            Instantiate(mapping.prefab, spawnPos, Quaternion.identity).transform.parent = this.transform;
                        }
                        else{
                            Instantiate(crackedWall, spawnPos, Quaternion.identity).transform.parent = this.transform;
                        }
                        return;
                    }
                    Instantiate(mapping.prefab, spawnPos, Quaternion.identity).transform.parent = this.transform;
                    return;
                }
			}
		}
	}

    //Gets the position of a given tile (obstruction/wall) relative to the current room.

	Vector3 positionFromTileGrid(int x, int y){
		Vector3 ret;
        Vector3 offset = new Vector3((-roomSizeInTiles.y / 2 * tileSize) + tileSize / 2, (-roomSizeInTiles.x / 2 * tileSize) + tileSize / 2, 0);
		ret = new Vector3(tileSize * (float) x, tileSize * (float) y, 0) + offset + transform.position;
		return ret;
	}


}
