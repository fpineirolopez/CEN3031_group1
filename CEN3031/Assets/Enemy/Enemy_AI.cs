using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour {

    Quaternion enemyRotation;
    Vector2 playerPos, enemyPos;
    public float Speed;
    public float shot_range;
    public Vector2 enemy_movement_vector;

    //Control signals for AI, not used currently
    public bool movement_enabled = true;
    public bool in_range = false; 


    void Start()
    {

    }

    void Update()
    {
        GameObject player = GameObject.Find("Player");

        playerPos = new Vector2(player.transform.localPosition.x, player.transform.localPosition.y);//player position 
        enemyPos = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y);//enemy position
        enemy_movement_vector = Vector2.MoveTowards(enemyPos, playerPos, Speed * Time.deltaTime);

        if (movement_enabled)//move towards if not close by 
        {
            
            transform.position = enemy_movement_vector;
            
        }

        if(Vector3.Distance(transform.position, player.transform.position) < shot_range )
        {
            in_range = true;
        }
        else
        {
            in_range = false;
        }

    }
}
