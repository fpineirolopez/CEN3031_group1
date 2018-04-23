using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false; //boolean to check if game is paused

    public GameObject PauseMenuUI; //used for bringing up UI

    public UnityEngine.UI.Button resumeButton; //used  for highlighting first button in pause menu and be able to navigate it with the keys

    public Image black;
    public Animator anim;

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
        resumeButton.OnSelect(null);  // highlights Resume button when opening pause menu
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //Exits the game to main menu
    public void QuitGame()
    {
        Time.timeScale = 1f; //reset time
        Destroy(GameObject.FindGameObjectWithTag("Player")); // destroy player
        GameObject.Find("Level Generator").GetComponent<LevelGeneration>().Reset();//Reset the level gen.
        GameObject.Find("Score_num").GetComponent<Score_num>().Reset_score();//Reset the level gen
        FindObjectOfType<AudioManager>().Stop("BackgroundMusic");
        StartCoroutine(Fading()); //start fadeing before switching scene
    }

    //Fade animations bewteen scenes
    IEnumerator Fading()
    {
        anim.SetBool("Fade", true); //start fade
        yield return new WaitUntil(() => black.color.a == 1); //stop fade
        SceneManager.LoadScene("MainMenu"); //switch scene
    }

}
