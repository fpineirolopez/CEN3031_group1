using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    [HideInInspector]
    public int dir = 0;//The door direction, meaning which side the door is on (UDLR)

    void OnTriggerEnter2D(Collider2D coll){//Detect when the player collides with the door.
        if (coll.gameObject.tag != "Player")
            return;
        /*
        switch (dir){
            case 1:
                Debug.Log("Top Door");
                break;
            case 2:
                Debug.Log("Bottom Door");
                break;
            case 3:
                Debug.Log("Left Door");
                break;
            case 4:
                Debug.Log("Right Door");
                break;
            default:
                Debug.Log("ERROR, door type not found");
                return;
        }
        */
        GameObject.FindWithTag("MainCamera").GetComponent<CameraMovement>().MoveCameraOneRoom(dir);//Move the camera.
        FindObjectOfType<AudioManager>().Play("OpenDoor");
        SheetAssigner SA = FindObjectOfType<SheetAssigner>();//Find SheetAssigner that holds overall room information.
        float posOrNeg;
        Vector2 playerMove = Vector2.zero;

        //16*3 might overshoot a bit, but the overshoot amount should feel fairly trivial, but it's better than undershooting.
        //This is subject to change with the new graphics as they are in different dimensions.
        if (dir == 1 || dir == 2){
            playerMove.y = SA.gutterSize.y + 16*3;//Move by 3 units further than it should be to ensure that the player will not collide with the door collider in the next room.
        }
        else{
            playerMove.x = SA.gutterSize.x + 16*3;
        }
        if (dir == 1 || dir == 4){
            posOrNeg = 1.0f;
        }
        else{
            posOrNeg = -1.0f;
        }
        playerMove *= posOrNeg;
        GameObject.FindWithTag("Player").GetComponent<PlayerClass>().TeleportByAmount(playerMove);//This isn't named/set up correctly right now.
    }

    void setDir(int d){
        dir = d;
    }
}

