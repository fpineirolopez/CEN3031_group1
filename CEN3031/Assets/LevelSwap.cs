﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSwap : MonoBehaviour {
    
    void OnTriggerEnter2D(Collider2D collid) {
        Debug.Log("Advancing to Next Floor...");
        if (collid.gameObject.tag != "Player")
            return;
        else
        {
            FindObjectOfType<AudioManager>().Play("EnterStairs");
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>().Health_Regen(); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
