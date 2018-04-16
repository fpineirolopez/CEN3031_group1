using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomProgression
{

    // Input  - integer floor number ( 1 - XX)
    // Output - Enemy count 
    public int floor_number_to_enemy_count(int floor_number)
    {
        float median_value = 0;
        int random_value = 0;
        if (floor_number < 11)
        {
            median_value = (float)floor_number + 2;
            random_value = (int)Mathf.Ceil(Random.Range(median_value * (1.0f - (.05f * floor_number)) , median_value * (1.00f + (.05f * floor_number)) ) );
        }

        else
        {
            median_value = 13;
            random_value = (int)Mathf.Ceil(Random.Range(median_value * 0.50f, median_value * (1.5f)));
        }

        return random_value;
    }

    // Input  - integer floor number ( 1 - XX)
    // Output - room count 
    public int floor_number_to_room_count(int floor_number)
    {
        float median_value = 0;
        int random_value = 0;
        if (floor_number < 11)
        {
            median_value = (float)floor_number + 3;
            random_value = (int)Mathf.Ceil(Random.Range(median_value * (1.0f - (.025f * floor_number)), median_value * (1.00f + (.025f * floor_number))));
        }

        else
        {
            median_value = 14;
            random_value = (int)Mathf.Ceil(Random.Range(median_value * 0.75f, median_value * (1.25f)));
        }

        return random_value;
    }


}
