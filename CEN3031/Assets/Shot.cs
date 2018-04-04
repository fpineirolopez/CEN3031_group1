using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {
  
    //How do you get range and speed?
    //Passing of variables between scripts is difficult

    // Damage inflicted
    public int dmg;
    public float range;
    public float shot_speed;

    void Start()
    {
        // 2 - Limited time to live to avoid any leak
        Destroy(gameObject, range / shot_speed); // range divided by velocity
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Enviroment"))
        {
            Destroy(gameObject);
        }
    }
}

