using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateSlider : MonoBehaviour {
	
    Slider slider;


    void Start(){
        slider = GetComponent<Slider>();
    }

	void Update () {
        try{
            float health = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>().current_hp;
            slider.value = health;
            slider.maxValue = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>().max_hp;
        }
        catch(System.Exception e){
            Debug.Log("Caught exception, player has been deleted from scene so slider does not work.");
        }
    }
}
