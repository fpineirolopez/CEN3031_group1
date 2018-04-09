using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools.Utils;

public class PlayerTest
{

    [Test]
    public void PlayerTestMovement()
    {
        //Initialize player controller object
        Player_control pctrl = new Player_control();

        //Create vector to mime inputs
        Vector2 inputs = new Vector2(0.0f, 0.0f);
        Vector2 movement = new Vector2(0.0f, 0.0f);

        //Testing for the movement vector calculator for all possible inputs
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                inputs = new Vector2(-1.0f + i, -1.0f + j);
                movement = pctrl.movement_calculator(inputs.x, inputs.y);
                inputs.Normalize();
                Assert.AreEqual(movement.x, inputs.x);
                Assert.AreEqual(movement.y, inputs.y);
            }
        }

    }

    [Test]
    public void PlayerTestRange()
    {
        Player_control pctrl = new Player_control();

        for (float i = 1; i < 20; i += .1f)
        {
            for (float j = 1; j < 20; j += .1f)
            {
                float time = i / j;
                Assert.AreEqual(time, pctrl.projectile_time_calculator(j, i));
            }
        }
    }

   

}

