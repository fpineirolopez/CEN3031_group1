using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /*Function triggers playing the game and calls the next scene in the queue when clicking the Play button.
    To adjust what the next scene in the queue is, go to File > Build Settings, and then drag the scenes in 
    the order you want them in the queue*/
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*Function triggers quitting the game and exits the application. I added the Debug message to make sure that
    it is working while inside of Unity*/
    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

}
