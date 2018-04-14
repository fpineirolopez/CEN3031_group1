using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    /*Function triggers returning to the main menu*/
    public void ReturntoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /*Function triggers quitting the game and exits the application. I added the Debug message to make sure that
    it is working while inside of Unity*/
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

}
