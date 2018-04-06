using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false; //boolean to check if game is paused

    public GameObject PauseMenuUI; //used for bringing up UI

    public UnityEngine.UI.Button resumeButton; //used  for highlighting first button in pause menu and be able to navigate it with the keys

    //This is called when this script is loaded.
    void Start(){
		PauseMenuUI.SetActive (false); 
	}

    //Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) //when pressing 'Esc', it checks if the game is paused or not
        {
            if (GameIsPaused) //if paused, resume
            {
                Resume();
            }
            else //else, Pause
            {
                Pause();
            }
        }
    }

    //Resumes the game and time
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    //Pauses the game and stops time.
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        resumeButton.OnSelect(null); // highlights Resume button when opening pause menu
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //Exits the game. Displays a console message so that we know it is working within Unity
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

}
