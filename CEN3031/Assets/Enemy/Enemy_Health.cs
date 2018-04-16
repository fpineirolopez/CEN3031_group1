using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour {

    // Total hitpoints
    public int hp = 5;

    // Inflicts damage and check if the object should be destroyed
    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if (hp <= 0)
        {
            // Dead!
            GameObject.Find("LevelGenerator").GetComponent<LevelGeneration>().killEnemy();
            Destroy(gameObject);
        }
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
