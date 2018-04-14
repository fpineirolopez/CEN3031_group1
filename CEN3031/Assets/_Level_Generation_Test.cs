using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

//These tests must be run using the playmode setting in the test runner.
//Note that DrawMap() and the SheetAssigner should be commented out in the original script for these test to work correctly.
public class _Level_Generation_Test {
    
    //A GameObject with the LevelGeneration Script is called, which runs Start() because it is in playmode.
    //Public values can be set prior to running yield return null, which is when the Start() function is executed in the gameObject.
    //Then, the method simply checks if the rooms array has exactly x many rooms set.
    //It has been confirmed to work for all valid room sizes. Note that changes will need to be made regarding dimenisons if 
    //it is desired that this code works for all possible world sizes, not just the 8x8 specified at this point.
	[UnityTest]
    public IEnumerator _Level_Generation_Creates_Desired_Number_Of_Rooms() {
        var roomGenerator = new GameObject().AddComponent<LevelGeneration>();//Create the generator object.
        roomGenerator.numberOfRooms = 20;
        yield return null;

        int numberOfRoomsExpected = 20;
        int numberOfRoomsCreated = 0;

        for (int i = 0; i < 8; i++){
            for (int j = 0; j < 8; j++){
                if (roomGenerator.rooms[i, j] != null)
                    numberOfRoomsCreated++;
            }
        }
        Assert.AreEqual(numberOfRoomsExpected, numberOfRoomsCreated);
	}


    //The setup is the exact same as above.
    //This method checks each room in the room array and sees if there is at least one room that is adjacent to it.
    //If it finds one room that does not have any adjacent rooms, the test fails. Otherwise it passes.
    //Tested to work.
    [UnityTest]
    public IEnumerator _Level_Generation_Creates_Connected_Rooms() {
        var roomGenerator = new GameObject().AddComponent<LevelGeneration>();
        roomGenerator.numberOfRooms = 20;
        yield return null;
        bool isWholeLevelConnected = true;
        int[] dirs = new int[]{ 0, 1, 0, -1, 1, 0, -1, 0 };//All four directions, to add to indices.

        bool isRoomConnected = false;
        for (int i = 0; i < 8; i++){
            for (int j = 0; j < 8; j++){
                if (roomGenerator.rooms[i, j] != null){
                    for (int k = 0; k < 4; k++){//Iterate through dirs array to check for adjacent rooms.
                        if (isValidPoint(i + dirs[2 * k], j + dirs[2 * k + 1])){
                            if (roomGenerator.rooms[i + dirs[2 * k], j + dirs[2 * k + 1]] != null)
                                isRoomConnected = true;
                        }
                        if (isRoomConnected)
                            break;
                    }
                    if (!isRoomConnected)
                        isWholeLevelConnected = false;//Technically the entire loop can break here, but I won't for now. This shouldn't change performance.
                }
                isRoomConnected = false;//Reset variable for next room.
            }
        }

        Assert.AreEqual(true, isWholeLevelConnected);
    }

    //Utility function to check if this point is inside of the room array dimensions
    bool isValidPoint(int i, int j){
        if (i < 0 || i >= 8 || j < 0 || j >= 8)
            return false;
        return true;
    }


    [TearDown]
    public void AfterEveryTest(){
        foreach (var gameObject in Object.FindObjectsOfType<LevelGeneration>())
            Object.Destroy(gameObject);
    }
}
