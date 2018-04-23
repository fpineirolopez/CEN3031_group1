using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour {

    // Total hitpoints
    public int hp = 5;
    bool dead = false;

    // Inflicts damage and check if the object should be destroyed
    public void Damage(int damageCount)
    {
        if (dead)
            return;
        hp -= damageCount;

        if (hp <= 0)
        {
            // Dead!
            dead = true;
            Debug.Log("Killed enemy");
            Destroy(gameObject);
            GameObject.Find("Level Generator").GetComponent<LevelGeneration>().killEnemy();
            GameObject.Find("Score_num").GetComponent<Score_num>().set_score(50); //update score!!
            FindObjectOfType<AudioManager>().Play("EnemyMageDead");
            return;
        }

        FindObjectOfType<AudioManager>().Play("EnemyMageHit");
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Is this a shot?
        Shot shot = otherCollider.gameObject.GetComponent<Shot>();
        if (shot != null)
        {
                Damage(shot.dmg); //inflict damage

                // Destroy the shot
                Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
        }
    }

}
