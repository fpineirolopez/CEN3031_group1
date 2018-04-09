using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_laser : MonoBehaviour {
    
 
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {

        Vector2 position = GameObject.Find("Boss").transform.position;
        transform.position = position;
        death();


    }

    void death()
    { 
        Object.Destroy(gameObject, 2);
    }
}
