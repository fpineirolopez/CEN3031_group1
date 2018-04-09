using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public int health_calculator(int current_health, int damage)
    {
        int health = current_health - damage;
        return health;
    }
}
