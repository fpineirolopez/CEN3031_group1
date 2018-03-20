using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public float speed;
    public Vector2 movement;
    public Player_control pctrl;

    // Use this for initialization
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        movement = new Vector2(0, 0);

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        movement = pctrl.movement_calculator(moveHorizontal, moveVertical);
        rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);

    }
}
