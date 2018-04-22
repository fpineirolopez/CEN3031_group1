using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour {
    
    // Projectile prefab for shooting
    public Transform shotPrefab;

    // Cooldown in seconds between two shots
    private float range;
    private int damage;
    private float fire_rate;
    private float shot_speed;

    public void set_range(float range)
    {
        this.range = range;
    }

    public void set_dmg(int damage)
    {
        this.damage = damage;
    }

    public void set_fire_rate(float fire_rate)
    {
        this.fire_rate = fire_rate;
    }

    public void set_shot_speed(float shot_speed)
    {
        this.shot_speed = shot_speed;
    }


    // 2 - Cooldown
    private float shootCooldown;

    void Start()
    {
        shootCooldown = 0f;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    // Is the weapon ready to create a new projectile?
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }

    // Shooting from another script...
    // Create a new projectile if possible, shooting Up
    public void AttackUp()
    {
        if (CanAttack)
        {
            shootCooldown = fire_rate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Assign position
            // Position adjusted based on player - also affects enemy
            shotTransform.position = transform.position + new Vector3(0f, 2f, 0f);

            // add values to shot object
            Shot shot = shotTransform.gameObject.GetComponent<Shot>();
            shot.dmg = damage;
            shot.range = range;
            shot.shot_speed = shot_speed;

            // add movement to shot
            Move move = shotTransform.gameObject.GetComponent<Move>();
            move.speed = new Vector2(shot_speed, shot_speed);

            if (move != null)
            {
                move.direction = this.transform.up; // towards in 2D space is the right of the sprite
            }
        }
    }

    // Create a new projectile if possible, shooting Down
    public void AttackDown()
    {
        if (CanAttack)
        {
            shootCooldown = fire_rate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Assign position
            shotTransform.position = transform.position + new Vector3(0f, -2f, 0f);

            // add values to shot object
            Shot shot = shotTransform.gameObject.GetComponent<Shot>();
            shot.dmg = damage;
            shot.range = range;
            shot.shot_speed = shot_speed;

            // add movement to shot
            Move move = shotTransform.gameObject.GetComponent<Move>();
            move.speed = new Vector2(shot_speed, shot_speed);

            if (move != null)
            {
                move.direction = -(this.transform.up); // towards in 2D space is the right of the sprite
            }
        }
    }

    // Create a new projectile if possible, shooting Right
    public void AttackRight()
    {
        if (CanAttack)
        {
            shootCooldown = fire_rate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Assign position
            shotTransform.position = transform.position + new Vector3(2f, 0f, 0f);

            // add values to shot object
            Shot shot = shotTransform.gameObject.GetComponent<Shot>();
            shot.dmg = damage;
            shot.range = range;
            shot.shot_speed = shot_speed;

            // add movement to shot
            Move move = shotTransform.gameObject.GetComponent<Move>();
            move.speed = new Vector2(shot_speed, shot_speed);

            if (move != null)
            {
                move.direction = this.transform.right; // towards in 2D space is the right of the sprite
            }
        }
    }

    // Create a new projectile if possible, shooting Left
    public void AttackLeft()
    {
        if (CanAttack)
        {
            shootCooldown = fire_rate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Assign position
            shotTransform.position = transform.position + new Vector3(-2f, 0f, 0f);

            // add values to shot object
            Shot shot = shotTransform.gameObject.GetComponent<Shot>();
            shot.dmg = damage;
            shot.range = range;
            shot.shot_speed = shot_speed;

            // add movement to shot
            Move move = shotTransform.gameObject.GetComponent<Move>();
            move.speed = new Vector2(shot_speed, shot_speed);

            if (move != null)
            {
                move.direction = -(this.transform.right); // towards in 2D space is the right of the sprite
            }
        }
    }

    
}

