using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    
    private Rigidbody2D rb2d; 
    public float speed;
    public Vector2 movement = new Vector2(0, 0);

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

    }
}
