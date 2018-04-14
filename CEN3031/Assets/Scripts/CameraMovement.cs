using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    Vector3 moveJump = Vector2.zero;//Vector3 instead of 2 because it is used with transform.position, which is by default Vector3.
    Vector3 movePosition;
    Vector3 remainingMovement;
	float horMove, vertMove;//Floats that determine what type of movement will occur.
    float transitionSpeed = 8;
	void Start(){
		SheetAssigner SA = FindObjectOfType<SheetAssigner>();//Find SheetAssigner that holds overall room information.
        Vector2 tempJump = SA.roomDimensions + SA.gutterSize;//This makes sense; because all rooms are evenly spread out the room dimension + gutter should be just enough offset to move the camera one room in one of four directions.
		moveJump = new Vector3(tempJump.x, tempJump.y, 0); //distance b/w rooms: to be used for movement
        horMove = 0.0f;
        vertMove = 0.0f;
        movePosition = transform.position;
        remainingMovement = Vector3.zero;
	}
    //Method to set the movement for the camera when the player comes into contact with the door.
    //First, it detemines the direction based off of the passed parameter. The horiz/vertMove variables simply indicate the direction of movement.
    //Then, the rest of the motion is computed for the case in which the next movement is called before the previous one ends.
    //This is added to the new movePosition, which moves based off of the above defined moveJump distances.
    //Note the float comparison isn't an issue in this case because we are only dealing with the difference if it is not equal.
    public void MoveCameraOneRoom(int moveDirection){
        //Determine direction to move camera based off of direction passed in.
        if (moveDirection == 1)//Top
            vertMove = 1.0f;
        else if (moveDirection == 2)//Down
            vertMove = -1.0f;
        else if (moveDirection == 3)//Left
            horMove = -1.0f;
        else if (moveDirection == 4)//Right
            horMove = 1.0f;
        if (movePosition != transform.position){//If the previous interpolation has not yet finished
            remainingMovement = movePosition - transform.position;
        }
		movePosition = transform.position + remainingMovement;//Consider offset from still-incomplete motions.
		movePosition += Vector3.right * horMove * moveJump.x; //jump between rooms based on input
		movePosition += Vector3.up * vertMove * moveJump.y; //One of the two will actually change as no diagonal movement is allowed

        //Reset the direction variables for the next call.
        horMove = 0.0f;
        vertMove = 0.0f;
        remainingMovement = Vector3.zero;
	}

    //Use update to move the camera, as it is necessary to call lerp from update for it to function properly.
    //Lerp is used to create a smooth movement for the camera instead of a sudden jump with nothing in between frames.
    void Update(){
        transform.position = Vector3.Lerp(transform.position, movePosition, Time.deltaTime * transitionSpeed);
    }
}
