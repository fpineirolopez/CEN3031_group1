using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Collision_Damage : MonoBehaviour {

    public float timeBetweenAttacks = 0.5f;
    public int attack = 1;
    bool canAttack;
    GameObject player;
    Player_Health playerHealth;
    Enemy_Health enemyHealth;
    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Player_Health>();
        enemyHealth = GetComponent<Enemy_Health>();
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.gameObject == player)
        {
            canAttack = true;
        }
    }
    
    
    void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject == player)
        {
            canAttack = false;
        }
    }
   
	
	// Update is called once per frame
	void Update () {
       

        if( enemyHealth.hp > 0)
        {
            doDamage();
        }
	}


    void doDamage()
    {

        if(playerHealth.current_hp > 0 && playerHealth.Can_Take_Damage && canAttack)
        {
            playerHealth.Damage(attack);
        }
    }

}
