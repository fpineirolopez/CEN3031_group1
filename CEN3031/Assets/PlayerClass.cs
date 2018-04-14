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
    private Animator animator;

    //Booleans used to check what the player is doing and then control the animation controller
    private bool au = false;
    private bool al = false;
    private bool ar = false;
    private bool ad = false;
    private bool wu = false;
    private bool wd = false;
    private bool wl = false;
    private bool wr = false;


    //Player controller script created and used for offloading logic and calculations to testable interface
    public Player_control pctrl = new Player_control();

    // Use this for initialization
    void awake()
    {
        
    }

    //set player to origin - 0,0
    //Reset the player origin at the start of a new level
    public void ResetPlayerPosition()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    //Move player object by specified amount -- used for going through doors
    public void TeleportByAmount(Vector2 movementAmount)
    {
        Vector3 position = transform.position;
        position.x += movementAmount.x;
        position.y += movementAmount.y;
        transform.position = position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        //Get the rb2d for the player
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //Get the raw input for the input axis
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Debug.Log(moveHorizontal);
        Debug.Log(moveVertical);

                
        
        wu = false;
        wd = false;
        wl = false;
        wr = false;
        

        if (moveHorizontal == 1.0)
        {
            wr = true;
        }
        if(moveHorizontal == -1.0)
        {
            wl = true;
        }
        if(moveVertical == 1.0)
        {
            wu = true;
        }
        if(moveVertical == -1.0)
        {
            wd = true;
        }


        //Get the movement vectore based on input from the player controller
        movement = pctrl.movement_calculator(moveHorizontal, moveVertical);
        

        //Move the player based on the player's speed/ movement vector, and time between frames.
        rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);

        // Shooting up
        if (Input.GetKey(KeyCode.UpArrow) || test_key_up)
        {
            au = true;
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
        else
        {
            au = false;
        }

        // Shooting down
        if (Input.GetKey(KeyCode.DownArrow) || test_key_down)
        {
            ad = true;
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
        else
        {
            ad = false;
        }

        // Shooting right
        if (Input.GetKey(KeyCode.RightArrow) || test_key_right)
        {
            ar = true;
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
        else
        {
            ar = false;
        }

        // Shooting left
        if (Input.GetKey(KeyCode.LeftArrow) || test_key_left)
        {
            al = true;
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
        else
        {
            al = false;
        }

        //Animation controller logic
            //If attacking
        if (au || al || ar || ad)
        { 
            //Set the animation bool based on attacking direction

            animator.SetBool("au", au);
            animator.SetBool("al", al);
            animator.SetBool("ar", ar);
            animator.SetBool("ad", ad);

            //move flags are set to false so we play attack and not move animations if attacking
            animator.SetBool("wl", false);
            animator.SetBool("wr", false);
            animator.SetBool("wu", false);
            animator.SetBool("wd", false);

        }
        //else -- not attacking
        else 
        {
            animator.SetBool("au", au);
            animator.SetBool("al", al);
            animator.SetBool("ar", ar);
            animator.SetBool("ad", ad);
            animator.SetBool("wl", wl);
            animator.SetBool("wr", wr);
            animator.SetBool("wu", wu);
            animator.SetBool("wd", wd);

        }
    }

    
}
