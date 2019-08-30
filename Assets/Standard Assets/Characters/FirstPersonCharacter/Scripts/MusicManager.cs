using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager M;

    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject game1;
    public GameObject game2;

    private AudioSource mainMenuBGM;
    private AudioSource pauseMenuBGM;
    private AudioSource game1BGM;
    private AudioSource game2BGM;

    private bool isPaused;


    // Use this for initialization
    void Start()
    {
        // assign the singleton
        M = this;

        mainMenuBGM = mainMenu.GetComponent<AudioSource>();
        pauseMenuBGM = pauseMenu.GetComponent<AudioSource>();
        game1BGM = game1BGM.GetComponent<AudioSource>();
        game2BGM = game2BGM.GetComponent<AudioSource>();

        isPaused = false;
    }

    //list of commands
    /*
    MusicManager.M.PlayMainMenu();
    MusicManager.M.StopMainMenu();
      
    MusicManager.M.PlayGame1();
    MusicManager.M.PauseGame1();
    MusicManager.M.StopGame1();

    MusicManager.M.PlayGame1();
    MusicManager.M.PauseGame1();
    MusicManager.M.StopGame1();
    */


    //Main Menu
    public void PlayMainMenu()
    {
        if (!mainMenuBGM.isPlaying)
        {
            mainMenuBGM.Play();
        }
    }

    public void StopMainMenu()
    {
        mainMenuBGM.Stop();
    }


    //BGM 1
    public void PlayGame1()
    {
        if (!game1BGM.isPlaying)
        {
            game1BGM.Play();
        }
    }

    public void PauseGame1()
    {
        if (!isPaused)
        {
            game1BGM.Pause();
            pauseMenuBGM.Play();
            isPaused = true;
        }
        else if (isPaused)
        {
            pauseMenuBGM.Stop();
            game1BGM.UnPause();
            isPaused = false;
        }
    }

    public void StopGame1()
    {
        game1BGM.Stop();
    }


    //BGM 2
    public void PlayGame2()
    {
        if (!game2BGM.isPlaying)
        {
            game2BGM.Play();
        }
    }

    public void PauseGame2()
    {
        if (!isPaused)
        {
            game2BGM.Pause();
            pauseMenuBGM.Play();
            isPaused = true;
        }
        else if (isPaused)
        {
            pauseMenuBGM.Stop();
            game2BGM.UnPause();
            isPaused = false;
        }
    }

    public void StopGame2()
    {
        game2BGM.Stop();
    }
}
