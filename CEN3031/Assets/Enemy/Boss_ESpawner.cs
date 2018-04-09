using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_ESpawner : MonoBehaviour {

    float timer;
    int waitingtime = 3;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > waitingtime)
        {

            Invoke("MinionSpwaner", 1f);
            timer = 0;

        }
        
	}

    void MinionSpwaner()
    {
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            GameObject minion = Instantiate(GameObject.Find("Boss_Minion"));
            minion.transform.position = transform.position;
        }
        
    }
}
