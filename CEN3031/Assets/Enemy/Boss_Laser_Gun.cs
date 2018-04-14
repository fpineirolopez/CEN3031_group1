using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Laser_Gun : MonoBehaviour { 

    float timer2;
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
        timer2 += Time.deltaTime;
        if (timer2 > 10)
        {
            Invoke("firelaser", 1f);
            timer2 = 0;
        }

    }

    

    void firelaser()
    {
        GameObject boss = GameObject.Find("Boss");
        if (boss != null)
        {
            Instantiate(Blaser);
            Blaser.transform.position = boss.transform.position;
        }

    }
}