using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Gun : MonoBehaviour {
    public GameObject Enemy_Projectile;
    float timer;
    int waitingtime = 1;
    Vector3 spin;
    // Use this for initialization
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer > waitingtime)
        {
            Invoke("fireProjectile", 1f);
            timer = 0;
        }
        
        
    }

    //fire
    void fireProjectile()
    {
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            GameObject bullet1 = (GameObject)Instantiate(Enemy_Projectile);
            GameObject bullet2 = (GameObject)Instantiate(Enemy_Projectile);
            GameObject bullet3 = (GameObject)Instantiate(Enemy_Projectile);
            GameObject bullet4 = (GameObject)Instantiate(Enemy_Projectile);
            GameObject bullet5 = (GameObject)Instantiate(Enemy_Projectile);
            //initial pos
            bullet1.transform.position = transform.position;
            bullet2.transform.position = transform.position;
            bullet3.transform.position = transform.position;
            bullet4.transform.position = transform.position;
            bullet5.transform.position = transform.position;

            //aim at player
            Vector2 direction1 = player.transform.position - bullet1.transform.position;
            Vector2 direction2 = player.transform.position - bullet2.transform.position;
            Vector2 direction3 = player.transform.position - bullet3.transform.position;
            Vector2 direction4 = player.transform.position - bullet4.transform.position;
            Vector2 direction5 = player.transform.position - bullet5.transform.position;

            direction1 = direction1 + 30*(Vector2.up);
            direction2 = direction2 + 15*(Vector2.up);
            direction3 = direction3 + 15*(Vector2.down);
            direction4 = direction4 + 30*(Vector2.down);


            bullet1.GetComponent<Enemy_Projectile>().setDir(direction1);
            bullet2.GetComponent<Enemy_Projectile>().setDir(direction2);
            bullet3.GetComponent<Enemy_Projectile>().setDir(direction3);
            bullet4.GetComponent<Enemy_Projectile>().setDir(direction4);
            bullet5.GetComponent<Enemy_Projectile>().setDir(direction5);

        }
    }

    
}
