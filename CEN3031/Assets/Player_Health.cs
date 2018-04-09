using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{

    public int max_hp;          // set in unity, in the player asset itself
    public int current_hp;     // current health, used for calculations
    bool isDead = false;        // checks when player dies

    public UnityEngine.UI.Slider health_bar;        // healthbar UI

    void Start()
    {
        // Initialize our hp
        current_hp = max_hp;

        //Initialize with full health bar, setting the max value to whatever number we set the max health in the player asset
        health_bar.maxValue = max_hp;
        health_bar.value = current_hp;

    }

    // Inflicts damage and check if the object should be destroyed
    public void Damage(int damageCount)
    {
        current_hp -= damageCount; //update current hp

        health_bar.value = current_hp;  //update healthbar

        if (current_hp <= 0 && !isDead)
        {
            // Dead!
            PlayerDies();
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
      
        // Is this a shot?
        Enemy_Projectile shot = otherCollider.gameObject.GetComponent<Enemy_Projectile>();
        if (shot != null)
        {
            Damage(shot.damage); //inflict damage

            // Destroy the shot
            Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
        }
    }

    // When the player dies, deletes player object, and calls Game Over scene
    void PlayerDies()
    {
        isDead = true; // set to true
        Destroy(gameObject); // destroy player
        SceneManager.LoadScene("GameOver");
    }
}
