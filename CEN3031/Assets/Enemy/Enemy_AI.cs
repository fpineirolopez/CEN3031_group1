using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour {

    public Transform target;//set target from inspector instead of looking in Update
    Quaternion enemyRotation;
    Vector2 playerPos, enemyPos;
    public float Speed;
    public float Distance_From_Player;
    public GameObject Boss_corpse;

    void Start()
    {
        enemyRotation = this.transform.localRotation;
    }

    void Update()
    {
        try
        {
            playerPos = new Vector2(target.localPosition.x, target.localPosition.y); //player position 
            //playerPos = new Vector2(target.localPosition.x, target.localPosition.y);//player position 
            enemyPos = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y);//enemy position
            if (Vector3.Distance(transform.transform.position, target.transform.position) > Distance_From_Player)//move towards if not close by 
            {
                transform.position = Vector2.MoveTowards(enemyPos, playerPos, Speed * Time.deltaTime);
            }
            if (Vector3.Distance(transform.transform.position, target.transform.position) < Distance_From_Player - 0.05)//move away if too close 
            {
                //transform.position = Vector2.MoveTowards(enemyPos, playerPos, -1 * Time.deltaTime);
            }

            if (target.position.x > transform.position.x)//rotates enemy to the right if player is to the right  
            {
                enemyRotation.x = 180;
                transform.localRotation = enemyRotation;
            }
            if (target.position.x < transform.position.x)//rotates enemy to the left if player is to the left 
            {
                enemyRotation.x = 0;
                transform.localRotation = enemyRotation;
            }

        }
        catch (Exception e)
        {
            Debug.Log("Player has died!");
        }
    }

    void OnDestroy()
    {
        GameObject deadboi = (GameObject)Instantiate(Boss_corpse);

        deadboi.transform.position = this.transform.position;
    }
}
