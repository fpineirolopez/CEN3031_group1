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


    Vector2 playerPos, enemyPos, enemy_movement_vector, enemy_shooting_direction;
    Enemy_Animation_Control anim_ctrl;
    bool movement_enabled = true;
    bool in_range = false;
    bool doing_shot = false;
    int curr_direction;
    int curr_status;
    float shot_timer;
    float move_timer;


    public enum Direction { Up, Down, Left, Right };
    public enum Status { Moving, Shooting };

    void Start() {
        anim_ctrl = GetComponent<Enemy_Animation_Control>();
        curr_status = (int)(Status.Moving);
        shot_timer = SHOT_TIME;
        move_timer = WALK_TIME;
    }

    void Update() {
        GameObject player = GameObject.Find("Player");

        playerPos = new Vector2(player.transform.localPosition.x, player.transform.localPosition.y);//player position 
        enemyPos = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y);//enemy position
        enemy_movement_vector = Vector2.MoveTowards(enemyPos, playerPos, Speed * Time.deltaTime);

        // Calculate Direction for animation
        float x_val = enemy_movement_vector.x;
        float y_val = enemy_movement_vector.y;

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

        // Determine if the enemy is within shooting range of the player
        in_range = Vector2.Distance(enemyPos, playerPos) < shot_range;

        if (in_range) {
            if (shot_timer == SHOT_TIME && curr_status == (int)(Status.Shooting)) { // If in range and not already shooting, start firing a projectile
                movement_enabled = false; // movement is disabled during firing
                FireProjectile(); // Creates projectile and changes animation
                shot_timer -= Time.deltaTime; // begin decrementing timer
            }
            else if (shot_timer != SHOT_TIME && curr_status == (int)(Status.Shooting)) { // projectile is already being fired, continue doing nothing
                shot_timer -= Time.deltaTime;

                // Reset timer if below zero, and enter walking state
                if (shot_timer <= 0) {
                    shot_timer = SHOT_TIME; // reset shot timer
                    movement_enabled = true;
                    curr_status = (int)(Status.Moving); // Begin moving, which will start the move timer 
                }
            }
            else if (move_timer == WALK_TIME && curr_status == (int)(Status.Moving)) { // switching from shooting to moving status
                // Make sure the enemy isn't already too close. If it is, stop it
                if (Vector2.Distance(playerPos, enemyPos) < max_player_distance)
                    movement_enabled = false;
                else { // otherwise, move toward the player
                    movement_enabled = true;
                    transform.position = enemy_movement_vector;
                }

                move_timer -= Time.deltaTime;
            }
            else if (move_timer != WALK_TIME && curr_status == (int)(Status.Moving)) { // already moving, continue to do so unless too close
                // Make sure the enemy isn't already too close. If it is, stop it
                if (Vector2.Distance(playerPos, enemyPos) < max_player_distance)
                    movement_enabled = false;
                else { // otherwise, move toward the player
                    movement_enabled = true;
                    transform.position = enemy_movement_vector;
                }

                move_timer -= Time.deltaTime;
                // Reset timer if below zero, and enter walking state
                if (move_timer <= 0) {
                    move_timer = WALK_TIME; // reset shot timer
                    movement_enabled = false;
                    curr_status = (int)(Status.Shooting); // Begin moving, which will start the move timer 
                }
            }
        }

        else { // player is not in range, so reset timers while out of range and continue to move toward player

            // TODO - set status??

            shot_timer = SHOT_TIME;
            move_timer = WALK_TIME;
            curr_status = (int)Status.Moving;
            transform.position = enemy_movement_vector;

        }


    }

    void FireProjectile() {
        GameObject player = GameObject.Find("Player");

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
}
