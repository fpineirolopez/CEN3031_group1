using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_laser : MonoBehaviour {
    
 
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {

        death();


    }

    void death()
    {
       
        Object.Destroy(gameObject, 2);
    }
}
