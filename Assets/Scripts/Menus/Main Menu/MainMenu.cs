using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public LevelLoader levelLoader;

    public void PlayGame()
    {
        levelLoader.LoadGame();
    }

    public void OpenOptions()
    {

    }

    public void RollCredits()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
