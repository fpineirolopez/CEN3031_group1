using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI_2 : MonoBehaviour {

    public float Speed;
    public int shot_range = 2;
    public const float SHOT_TIME = .5f;
    public const float WALK_TIME = 1f;
    public int max_player_distance = 2;
    public GameObject Enemy_Projectile;


    Vector2 playerPos, enemyPos, enemy_movement_vector, enemy_shooting_direction, enemy_move_direction;
    Animator animator;
    bool can_move = true;
    bool in_range = false;
    bool doing_shot = false;
    int curr_direction;
    int curr_status;
    int last_saved_direction;
    float shot_timer;
    float move_timer;


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

        playerPos = new Vector2(player.transform.localPosition.x, player.transform.localPosition.y);//player position 
        enemyPos = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y);//enemy position
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

        Debug.Log("Movement: " + enemy_move_direction + "\nDirection: " + (Direction)curr_direction);

        // Determine if the enemy is within shooting range of the player
        in_range = Vector2.Distance(enemyPos, playerPos) < shot_range;

        if (in_range) {
            if (shot_timer == SHOT_TIME && curr_status == (int)(Status.Shooting)) { // If in range and not already shooting, start firing a projectile
                can_move = false; // movement is disabled during firing
                FireProjectile(); // Creates projectile and changes animation
                shot_timer -= Time.deltaTime; // begin decrementing timer
            }
            else if (shot_timer != SHOT_TIME && curr_status == (int)(Status.Shooting)) { // projectile is already being fired, continue doing nothing
                shot_timer -= Time.deltaTime;

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
                    transform.position = enemy_movement_vector;
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
                    transform.position = enemy_movement_vector;
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
            transform.position = enemy_movement_vector;

            UpdateAnimationWalk();
        }
    }

    void FireProjectile() {
        GameObject player = GameObject.Find("Player");

        UpdateAnimationAttack();

        if (player != null) {
            GameObject bullet = (GameObject)Instantiate(Enemy_Projectile);
            //initial pos
            bullet.transform.position = transform.position;
            Debug.Log(transform.position);

            //aim at player
            enemy_shooting_direction = player.transform.position - bullet.transform.position;
            bullet.GetComponent<Enemy_Projectile>().setDir(enemy_shooting_direction);

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

    void UpdateAnimationAttack()
    {
        ResetAnimationController();

        // Change animation appropriately
        if (last_saved_direction == (int)(Direction.Up))
        {
            animator.SetBool("au", true);
        }
        else if (last_saved_direction == (int)(Direction.Down))
        {
            animator.SetBool("ad", true);
        }
        else if (last_saved_direction == (int)(Direction.Left))
        {
            animator.SetBool("al", true);
        }
        else if (last_saved_direction == (int)(Direction.Right))
        {
            animator.SetBool("ar", true);
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
}
