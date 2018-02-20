using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false; //boolean to check if game is paused

    public GameObject PauseMenuUI; //used for bringing up UI

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
        Time.timeScale = 0f;
        GameIsPaused = true;
    }


    //when we have a settings menu made, we link it here. Right now it only displays a console message
    public void LoadSettings()
    {
        Debug.Log("Make a settings Menu!");
    }

    //Exits the game. Displays a console message so that we know it is working within Unity
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

}
