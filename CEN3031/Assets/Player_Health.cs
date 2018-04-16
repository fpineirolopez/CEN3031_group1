using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{

    public int max_hp;          // set in unity, in the player asset itself
    public int current_hp;     // current health, used for calculations
    bool isDead = false;   // checks when player dies
    float Damage_timer;
    public bool Can_Take_Damage;
    public float Damage_Cooldown;

    public Health phealth = new Health(); // for offloading calculations to a testable interface

    public UnityEngine.UI.Slider health_bar;        // healthbar UI

    public static Player_Health instance = null;

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

    void Start()
    {
        // Initialize our hp
        current_hp = max_hp;

        //Initialize with full health bar, setting the max value to whatever number we set the max health in the player asset
        health_bar.maxValue = max_hp;
        health_bar.value = current_hp;
        Can_Take_Damage = true;
        Damage_timer = .5f;

    }

    void Update()
    {
        if (!Can_Take_Damage)
        {
            Damage_timer -= Time.deltaTime;
            if(Damage_timer <= 0)
            {
                Can_Take_Damage = true;
                Damage_timer = .5f;
            }
           
        }
        
    }
    // Inflicts damage and check if the object should be destroyed
    public void Damage(int damageCount)
    {
        if (Can_Take_Damage)
        {
            //Get the health int based on input
            current_hp = phealth.health_calculator(current_hp, damageCount);

            health_bar.value = current_hp;  //update healthbar

            if (current_hp <= 0 && !isDead)
            {
                // Dead!
                PlayerDies();
            }
            Can_Take_Damage = false;
        }
        else
        {
            //do nothing
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
