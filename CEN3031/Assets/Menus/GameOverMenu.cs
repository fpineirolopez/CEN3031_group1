using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {

    public Image black;
    public Animator anim;

    /*Function triggers returning to the main menu*/
    public void ReturntoMainMenu()
    {
        StartCoroutine(Fading());
    }

    /*Function triggers quitting the game and exits the application. I added the Debug message to make sure that
    it is working while inside of Unity*/
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene("MainMenu");
    }

}
