using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gun : MonoBehaviour {

    public GameObject Enemy_Projectile;
    float timer;
    int waitingtime = 1;
	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > waitingtime)
        {
            Invoke("fireProjectile", 1f);
            timer = 0;
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

            //aim at player
            Vector2 direction = player.transform.position - bullet.transform.position;

            bullet.GetComponent<Enemy_Projectile>().setDir(direction);

        }
    }
}
