using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI_2 : MonoBehaviour {

    public float Speed;
    public int shot_range = 2;
    public const float SHOT_TIME = .5f;
    public const float WALK_TIME = 1f;
    public Vector3 projectile_offset_left = new Vector3(-8f, -2f, 0f);
    public Vector3 projectile_offset_right = new Vector3(8f, -2f, 0f);
    public Vector3 projectile_offset_up = new Vector3(0f, 8f, 0f);
    public Vector3 projectile_offset_down = new Vector3(0f, -8f, 0f);
    public int max_player_distance = 2;
    public GameObject Enemy_Projectile;

    public GameObject Corpse;


    Vector2 playerPos, enemyPos, enemy_movement_vector, enemy_move_direction;
    Animator animator;
    bool can_move = true;
    bool in_range = false;
    bool has_fired_shot;
    int curr_direction;
    int curr_status;
    int last_saved_direction;
    float shot_timer;
    float move_timer;
    Rigidbody2D rb2d;

    enum Direction { Up, Down, Left, Right };
    enum Status { Moving, Shooting };

    void Start() {
        animator = GetComponent<Animator>();
        curr_status = (int)(Status.Moving);
        shot_timer = SHOT_TIME;
        move_timer = WALK_TIME;
    }

    void Update() {
        GameObject player = GameObject.Find("Player");
        rb2d = GetComponent<Rigidbody2D>();

        try
        {
            playerPos = new Vector2(player.GetComponent<Rigidbody2D>().position.x, player.GetComponent<Rigidbody2D>().position.y);//player position 
        }
        catch (Exception e)
        {
            Debug.Log("Player has died!");
        }
        //playerPos = new Vector2(player.GetComponent<Rigidbody2D>().position.x, player.GetComponent<Rigidbody2D>().position.y);//player position 
        enemyPos = new Vector2(rb2d.position.x, rb2d.position.y);//enemy position
        enemy_movement_vector = Vector2.MoveTowards(enemyPos, playerPos, Speed * Time.deltaTime);
        enemy_move_direction = playerPos - enemyPos;

        // Calculate Direction for animation
        float x_val = enemy_move_direction.x;
        float y_val = enemy_move_direction.y;

        if (System.Math.Abs(x_val) >= System.Math.Abs(y_val)) {
            if (x_val >= 0)
                curr_direction = (int)Direction.Right;
            else
                curr_direction = (int)Direction.Left;
        }
        else {
            if (y_val >= 0)
                curr_direction = (int)(Direction.Up);
            else
                curr_direction = (int)(Direction.Down);
        }

        //Debug.Log("Movement: " + enemy_move_direction + "\nDirection: " + (Direction)curr_direction);

        // Determine if the enemy is within shooting range of the player
        in_range = Vector2.Distance(enemyPos, playerPos) < shot_range;

        if (in_range) {
            if (shot_timer == SHOT_TIME && curr_status == (int)(Status.Shooting)) { // If in range and not already shooting, start firing a projectile
                can_move = false; // movement is disabled during firing
                has_fired_shot = false; // delay firing shot til proper point in animation
                UpdateAnimationAttack();
                shot_timer -= Time.deltaTime; // begin decrementing timer
            }
            else if (shot_timer != SHOT_TIME && curr_status == (int)(Status.Shooting)) { // projectile is already being fired, continue doing nothing
                shot_timer -= Time.deltaTime;

                // Check if it is time to create the shot projectile
                if (shot_timer <= SHOT_TIME - .3 && !has_fired_shot) {
                    FireProjectile(); // Creates projectile and changes animation
                    has_fired_shot = true; // used to avoid duplicate projectile creation
                }

                // Reset timer if below zero, and enter walking state
                if (shot_timer <= 0) {
                    shot_timer = SHOT_TIME; // reset shot timer
                    can_move = true;
                    curr_status = (int)(Status.Moving); // Begin moving, which will start the move timer 
                }
            }
            else if (move_timer == WALK_TIME && curr_status == (int)(Status.Moving)) { // switching from shooting to moving status
                // Make sure the enemy isn't already too close. If it is, stop it
                if (Vector2.Distance(playerPos, enemyPos) < max_player_distance)
                    can_move = false;
                else { // otherwise, move toward the player
                    can_move = true;
                    //transform.position = enemy_movement_vector;
                    rb2d.MovePosition(enemy_movement_vector);
                }

                last_saved_direction = curr_direction;
                UpdateAnimationWalk();
                move_timer -= Time.deltaTime; // start movement timer
            }
            else if (move_timer != WALK_TIME && curr_status == (int)(Status.Moving)) { // already moving, continue to do so unless too close
                // Make sure the enemy isn't already too close. If it is, stop it
                if (Vector2.Distance(playerPos, enemyPos) < max_player_distance)
                    can_move = false;
                else { // otherwise, move toward the player
                    can_move = true;
                    //transform.position = enemy_movement_vector;
                    rb2d.MovePosition(enemy_movement_vector);

                }

                last_saved_direction = curr_direction;
                UpdateAnimationWalk();
                move_timer -= Time.deltaTime;
                // Reset timer if below zero, and enter walking state
                if (move_timer <= 0) {
                    move_timer = WALK_TIME; // reset move timer
                    can_move = false;
                    curr_status = (int)(Status.Shooting); // Begin shooting next update (if still in range)
                }
            }
        }

        else { // player is not in range, so reset timers while out of range and continue to move toward player
            shot_timer = SHOT_TIME;
            move_timer = WALK_TIME;
            curr_status = (int)Status.Moving;
            last_saved_direction = curr_direction;
            //transform.position = enemy_movement_vector;
            rb2d.MovePosition(enemy_movement_vector);


            UpdateAnimationWalk();
        }
    }

    void FireProjectile() {

        //ResetAnimationController();

        GameObject bullet = (GameObject)Instantiate(Enemy_Projectile);

        // Spawn bullet with offset depending on direction
        if (last_saved_direction == (int)(Direction.Up)) {
            bullet.transform.position = transform.position + projectile_offset_up;
        }
        else if (last_saved_direction == (int)(Direction.Down)) {
            bullet.transform.position = transform.position + projectile_offset_down;
        }
        else if (last_saved_direction == (int)(Direction.Left)) {
            bullet.transform.position = transform.position + projectile_offset_left;
        }
        else if (last_saved_direction == (int)(Direction.Right)) {
            bullet.transform.position = transform.position + projectile_offset_right;
        }

        // Aim at player
        GameObject player = GameObject.Find("Player");
        Vector2 enemy_shooting_direction = player.transform.position - bullet.transform.position;
        bullet.GetComponent<Enemy_Projectile>().setDir(enemy_shooting_direction);

    }

    void UpdateAnimationAttack() {
        ResetAnimationController();

        // Change animation depending on direction
        if (last_saved_direction == (int)(Direction.Up)) {
            animator.SetBool("au", true);
        }
        else if (last_saved_direction == (int)(Direction.Down)) {
            animator.SetBool("ad", true);
        }
        else if (last_saved_direction == (int)(Direction.Left)) {
            animator.SetBool("al", true);
        }
        else if (last_saved_direction == (int)(Direction.Right)) {
            animator.SetBool("ar", true);
        }
    }

    void UpdateAnimationWalk()
    {
        ResetAnimationController();

        // Change animation appropriately
        if (curr_direction == (int)(Direction.Up))
        {
            animator.SetBool("wu", true);
        }
        else if (curr_direction == (int)(Direction.Down))
        {
            animator.SetBool("wd", true);
        }
        else if (curr_direction == (int)(Direction.Left))
        {
            animator.SetBool("wl", true);
        }
        else if (curr_direction == (int)(Direction.Right))
        {
            animator.SetBool("wr", true);
        }
    }

    void ResetAnimationController()
    {
        animator.SetBool("wu", false);
        animator.SetBool("wd", false);
        animator.SetBool("wl", false);
        animator.SetBool("wr", false);
        animator.SetBool("au", false);
        animator.SetBool("ad", false);
        animator.SetBool("al", false);
        animator.SetBool("ar", false);
    }

    void OnDestroy()
    {
        GameObject deadboi = (GameObject)Instantiate(Corpse);

        deadboi.transform.position = this.transform.position;
    }
}
