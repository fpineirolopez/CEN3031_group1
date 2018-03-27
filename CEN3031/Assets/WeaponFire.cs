using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour {
    
    // Projectile prefab for shooting
    public Transform shotPrefab;

    // Cooldown in seconds between two shots
    public float shootingRate = 0.15f;

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

    // Shooting from another script...
    // Create a new projectile if possible, shooting Up
    public void AttackUp(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Assign position
            shotTransform.position = transform.position;

            // The is enemy property
            Shot shot = shotTransform.gameObject.GetComponent<Shot>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // Make the weapon shot always towards it
            Move move = shotTransform.gameObject.GetComponent<Move>();
            if (move != null)
            {
                move.direction = this.transform.up; // towards in 2D space is the right of the sprite
            }
        }
    }

    // Create a new projectile if possible, shooting Down
    public void AttackDown(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Assign position
            shotTransform.position = transform.position;

            // The is enemy property
            Shot shot = shotTransform.gameObject.GetComponent<Shot>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // Make the weapon shot always towards it
            Move move = shotTransform.gameObject.GetComponent<Move>();
            if (move != null)
            {
                move.direction = -(this.transform.up); // towards in 2D space is the right of the sprite
            }
        }
    }

    // Create a new projectile if possible, shooting Right
    public void AttackRight(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Assign position
            shotTransform.position = transform.position;

            // The is enemy property
            Shot shot = shotTransform.gameObject.GetComponent<Shot>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // Make the weapon shot always towards it
            Move move = shotTransform.gameObject.GetComponent<Move>();
            if (move != null)
            {
                move.direction = this.transform.right; // towards in 2D space is the right of the sprite
            }
        }
    }

    // Create a new projectile if possible, shooting Left
    public void AttackLeft(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            // Create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Assign position
            shotTransform.position = transform.position;

            // The is enemy property
            Shot shot = shotTransform.gameObject.GetComponent<Shot>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // Make the weapon shot always towards it
            Move move = shotTransform.gameObject.GetComponent<Move>();
            if (move != null)
            {
                move.direction = -(this.transform.right); // towards in 2D space is the right of the sprite
            }
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
}

