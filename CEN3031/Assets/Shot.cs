using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {
  
    // Damage inflicted
    public int dmg = 1;

    // Projectile damage player or enemies?
    public bool isEnemyShot = false;

    void Start()
    {
        // 2 - Limited time to live to avoid any leak
        Destroy(gameObject, 2); // 2sec
    }
}

