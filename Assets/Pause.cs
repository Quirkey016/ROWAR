using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deathScreen;
    public GameObject winScreen;
    public static bool IsPaused = false;

    private void Awake()
    {
        Time.timeScale = 1;
        IsPaused = false;
        winScreen.SetActive(false);
    }

    public void Win()
    {
        Time.timeScale = 0;
        IsPaused = true;
        winScreen.SetActive(true);
    }
    
    
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        switch (IsPaused)
        {
            case true:
                ResumeGame();
                break;
            case false:
                PauseGame();
                break;
        }
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        IsPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        IsPaused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        deathScreen.SetActive(false);
        SceneManager.LoadScene(0); //MainMenu
        
    }
    
    public void Retry()
    {
        Time.timeScale = 1;
        IsPaused = false;
        deathScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
