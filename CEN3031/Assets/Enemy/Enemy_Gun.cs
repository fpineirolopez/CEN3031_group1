using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gun : MonoBehaviour {

    public GameObject Enemy_Projectile;
    float startup_sleep_timer;
 
    int waitingtime = 1;
    public Vector2 enemy_shooting_direction;

    //Control signals for AI - not used currently
    private bool shooting_enabled; 


	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        startup_sleep_timer += Time.deltaTime;
        if(startup_sleep_timer > waitingtime)
        {
            Invoke("fireProjectile", 1f);
            startup_sleep_timer = 0;
        }


    }

    //fire
    void fireProjectile()
    {
        GameObject player = GameObject.Find("Player");

        if(player != null)
        {
            GameObject bullet = (GameObject)Instantiate(Enemy_Projectile);
            //initial pos
            bullet.transform.position = transform.position;
            Debug.Log(transform.position);

            //aim at player
            enemy_shooting_direction = player.transform.position - bullet.transform.position;
            Vector2 direction = enemy_shooting_direction;

            bullet.GetComponent<Enemy_Projectile>().setDir(direction);

        }
    }
}
