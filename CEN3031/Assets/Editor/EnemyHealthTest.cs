using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools.Utils;

public class EnemyHealthTest
{
    public Enemy_Health ehealth = new Enemy_Health(); // used to get max_hp to test calculations
    public Health health = new Health(); // for offloading calculations to a testable interface

    [Test]
    public void PlayerHealthTestPasses()
    {

        //Create int to mime inputs
        int max_health = ehealth.hp;
        int current_health = new int();
        int damage = new int();
        int calculated_health = new int();
        int inputs = new int();

        // Testing for the health int calculator for all health values where the damage is increased until 
        // it reaches the current health, and then current health is increased until it reaches the max health
        for (current_health = 0; current_health <= max_health; current_health++)
        {
            for (damage = 0; damage < current_health; damage++)
            {
                inputs = current_health - damage;
                calculated_health = health.health_calculator(current_health, damage);
                Assert.AreEqual(inputs, calculated_health);
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