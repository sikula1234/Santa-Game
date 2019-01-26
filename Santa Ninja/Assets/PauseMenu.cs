using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool HelpIsShown = false;

    public GameObject pauseMenuUI;
    public GameObject helpScreenUI;

    public void ClickPauseButton()
    {
        if (!GameIsPaused && !HelpIsShown)
        {
            PauseGame();
        } else if (!GameIsPaused && HelpIsShown)
        {
            HideHelp();
            PauseGame();
        } else if (GameIsPaused && !HelpIsShown)
        {
            ResumeGame();
        } else
        {
            HideHelp();
            ResumeGame();
        }
    }

    public void ClickHelpButton()
    {
        if (!GameIsPaused && !HelpIsShown)
        {
            ShowHelp();
        }
        else if (!GameIsPaused && HelpIsShown)
        {
            HideHelp();
        }
        else if (GameIsPaused && !HelpIsShown)
        {
            ResumeGame();
            ShowHelp();
        }
        else
        {
            HideHelp();
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void ShowHelp()
    {
        helpScreenUI.SetActive(true);
        Time.timeScale = 0f;
        HelpIsShown = true;
    }

    public void HideHelp()
    {
        helpScreenUI.SetActive(false);
        Time.timeScale = 1f;
        HelpIsShown = false;
    }
}
