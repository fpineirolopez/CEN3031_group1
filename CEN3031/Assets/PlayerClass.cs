using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    public float speed;
    public float range;
    public int damage;
    public float fire_rate;
    public float shot_speed;

    public bool test_key_up = false;
    public bool test_key_left = false;
    public bool test_key_right = false;
    public bool test_key_down = false;


    private Rigidbody2D rb2d;
    private Vector2 movement = new Vector2(0, 0);


    //Player controller script created and used for offloading logic and calculations to testable interface
    public Player_control pctrl = new Player_control();

    // Use this for initialization
    void awake()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Get the rb2d for the player
        rb2d = GetComponent<Rigidbody2D>();

        //Get the raw input for the input axis
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        //Get the movement vectore based on input from the player controller
        movement = pctrl.movement_calculator(moveHorizontal, moveVertical);
        Debug.Log(movement);

        //Move the player based on the player's speed/ movement vector, and time between frames.
        rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);

        // Shooting up
        if (Input.GetKey(KeyCode.UpArrow) || test_key_up)
        {
            WeaponFire weapon = GetComponent<WeaponFire>();
            weapon.set_dmg(damage);
            weapon.set_fire_rate(fire_rate);
            weapon.set_range(range);
            weapon.set_shot_speed(shot_speed);


            if (weapon != null)
            {
                // false because the player is not an enemy
                weapon.AttackUp();
            }
        }

        // Shooting down
        if (Input.GetKey(KeyCode.DownArrow) || test_key_down)
        {
            WeaponFire weapon = GetComponent<WeaponFire>();
            weapon.set_dmg(damage);
            weapon.set_fire_rate(fire_rate);
            weapon.set_range(range);
            weapon.set_shot_speed(shot_speed);

            if (weapon != null)
            {
                // false because the player is not an enemy
                weapon.AttackDown();
            }
        }

        // Shooting right
        if (Input.GetKey(KeyCode.RightArrow) || test_key_right)
        { 
            WeaponFire weapon = GetComponent<WeaponFire>();
            weapon.set_dmg(damage);
            weapon.set_fire_rate(fire_rate);
            weapon.set_range(range);
            weapon.set_shot_speed(shot_speed);

            if (weapon != null)
            {
                // false because the player is not an enemy
                weapon.AttackRight();
            }
        }

        // Shooting left
        if (Input.GetKey(KeyCode.LeftArrow) || test_key_left)
        {
            WeaponFire weapon = GetComponent<WeaponFire>();
            weapon.set_dmg(damage);
            weapon.set_fire_rate(fire_rate);
            weapon.set_range(range);
            weapon.set_shot_speed(shot_speed);

            if (weapon != null)
            {
                // false because the player is not an enemy
                weapon.AttackLeft();
            }
        }
    }
}
