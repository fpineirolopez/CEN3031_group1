using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_num : MonoBehaviour
{
    public Text score; //initialize score

    void Start()
    {
        score.text = "0"; //start at 0
    }

    public void set_score(int points)
    {
        int total = int.Parse(score.text); //convert score to int
        total += points; //increase score
        score.text = total.ToString(); //convert new score back to string
    }
 
    public void Reset_score()
    {
        score.text = "0"; //reset score
    }
}
