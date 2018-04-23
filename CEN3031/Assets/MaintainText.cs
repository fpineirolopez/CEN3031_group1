using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainText : MonoBehaviour {

    public static MaintainText instance = null; //this sript is so that the score doesn't get destroyed everytime we switch levels

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
