using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_laser : MonoBehaviour {
    
 
    // Use this for initialization
    void Start () {
        GameObject boss = GameObject.Find("Boss");
        transform.position = boss.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        death();
        GameObject boss = GameObject.Find("Boss");
        transform.position = boss.transform.position;


    }

    void death()
    {
        
        Object.Destroy(gameObject, 2);
    }
}
