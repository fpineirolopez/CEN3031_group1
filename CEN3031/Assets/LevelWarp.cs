using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWarp : MonoBehaviour {
    //For the moment just refresh the scene.
    void OnTriggerEnter2D(Collider2D collid){
        //Debug.Log("Entered stairs");
        if (collid.gameObject.tag != "Player")
            return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
