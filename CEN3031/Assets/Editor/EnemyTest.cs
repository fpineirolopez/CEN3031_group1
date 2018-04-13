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

        




	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator EnemyTestWithEnumeratorPasses() {

        var player = new GameObject("Player");
        var enemy = new  GameObject("Enemy");
        Vector2 player_pos = player.transform.position;
        Vector2 enemy_pos = enemy.transform.position;
        Vector2 curr_pos = Vector2.up;
        for(int i = 0; i < 100; i++)
        {
            curr_pos = player_pos - enemy_pos;
            yield return null;
        }
        float x = curr_pos.magnitude;
        Assert.Less(x, 1);

		yield return null;

        var enemy1 = new GameObject("Enemy_Ranged");
        Vector2 enemy_pos1 = enemy.transform.position;
        Vector2 curr_pos1 = Vector2.up;
        for (int i = 0; i < 100; i++)
        {
            curr_pos = player_pos - enemy_pos;
            yield return null;
        }

        float x1 = curr_pos.magnitude;
        Assert.Less(x1, 1);

    }
}
