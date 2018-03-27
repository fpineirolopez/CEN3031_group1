using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    // Object speed
    public Vector2 speed = new Vector2(10, 10);

    // Moving direction
    public Vector2 direction = new Vector2(1, 0);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    void Update()
    {
        //Movement
        movement = new Vector2(
          speed.x * direction.x,
          speed.y * direction.y);
    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        // Apply movement to the rigidbody
        rigidbodyComponent.velocity = movement;
    }
}