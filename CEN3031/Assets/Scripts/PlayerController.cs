using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb2d;

	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveHorizontal * speed, moveVertical * speed);
        rb2d.velocity = movement;
	}


    public void TeleportByAmount(Vector2 movementAmount){
        Vector3 tempPos = transform.position;
        tempPos.x += movementAmount.x;
        tempPos.y += movementAmount.y;
        transform.position = tempPos;
    }
}
