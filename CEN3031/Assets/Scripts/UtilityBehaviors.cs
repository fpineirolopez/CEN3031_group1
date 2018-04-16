using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UtilityBehaviors : MonoBehaviour {

    public static UtilityBehaviors instance = null;

    void Awake(){
        if (instance == null)
            instance = this;
        else if (instance != this){
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

	void Update () {
        /*
		if (Input.GetKeyDown("r")){//reload scene, for testing purposes, will be worked in to the overall level script.
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
        */      
	}
}
