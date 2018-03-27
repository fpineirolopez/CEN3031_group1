using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    public int dmg = 1; //Damage value

    public bool isEnemyShot = false; //did it hit enemy?

    void Start () {
        Destroy(gameObject, 2); //destroy after 2 sec
	}
}
