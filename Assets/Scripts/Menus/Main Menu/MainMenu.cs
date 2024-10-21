using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public LevelLoader levelLoader;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject credits;

    public void PlayGame()
    {
        levelLoader.LoadGame();
    }

    public void OpenOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void RollCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
