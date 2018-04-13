using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Gun2 : MonoBehaviour {
    public GameObject Enemy_Projectile;
    float timer, timer2;
    int waitingtime = 1;
    Vector3 spin;
    // Use this for initialization
    public GameObject Blaser;
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        spin = new Vector3(Random.value, Random.value,Random.value);
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        if (timer > waitingtime)
        {
            Invoke("fireProjectile", 1f);
            timer = 0;
        }
        if (timer2 > 10)
        {
            Invoke("firelaser", 1f);
            timer2 = 0;
        }

    }

    //fire
    void fireProjectile()
    {
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            GameObject bullet = (GameObject)Instantiate(Enemy_Projectile);
            //initial pos
            bullet.transform.position = transform.position;

            //aim at player
            
            Vector2 direction = player.transform.position - bullet.transform.position;
            direction += Vector2.up;


            bullet.GetComponent<Enemy_Projectile>().setDir(direction);

        }
    }

    void firelaser()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            Instantiate(Blaser);
            Blaser.transform.position = transform.position;
        }
        
    }
}
