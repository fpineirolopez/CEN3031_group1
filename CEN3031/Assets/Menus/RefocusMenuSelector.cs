using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RefocusMenuSelector : MonoBehaviour {
 
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            Debug.Log("Reselecting first input");
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        }
    }
}

