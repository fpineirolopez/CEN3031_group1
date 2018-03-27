using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    //bullet speed
    public Vector2 speed = new Vector2(15, 15);

    // bullet directions    
    public Vector2 direction = new Vector2(1, 0);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

	void Update ()
    {
        //Movement
        movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
       
    }

    private void FixedUpdate()
    {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        //apply movement to the rigidbody
        rigidbodyComponent.velocity = movement;
    }
}
