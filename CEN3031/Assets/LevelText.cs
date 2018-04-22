using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    public Text level_number; //initilaize

    void Start()
    {   
        level_number.text = "1"; //set it to 1 when starting
    }

    void Update()
    {
        int level = GameObject.Find("Level Generator").GetComponent<LevelGeneration>().Get_Level(); //get level number from level generation script
        level_number.text = level.ToString(); //change it to new level
    }
}
