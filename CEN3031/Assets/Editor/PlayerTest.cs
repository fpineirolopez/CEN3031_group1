using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools.Utils;

public class PlayerTest
{

    [Test]
    public void PlayerTestSimplePasses()
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

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator PlayerTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }
}

