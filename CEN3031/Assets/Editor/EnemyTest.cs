using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EnemyTest {

	[Test]
	public void EnemyTestSimplePasses() {
        Enemy_Health eh = new Enemy_Health();

        for(int i = 0; i < 5; i++)
        {
            eh.hp--;
        }
        Assert.AreEqual(eh.hp, 0);

        Enemy_AI test_pos = new Enemy_AI();

        //test_pos.playerPos;




	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator EnemyTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
