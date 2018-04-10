using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotHelper {

    // Test class for ensuring timer is correct
    public int dmg;
    public float range;
    public float shot_speed;

    public float getTimer() {
        return range / shot_speed;
    }
}
