using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_control
{

    public Vector2 movement_calculator(float moveHorizontal, float moveVertical)
    {
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement.Normalize();
        return movement;
    }


}

