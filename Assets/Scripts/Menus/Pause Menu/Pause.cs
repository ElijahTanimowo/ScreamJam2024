using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public LevelLoader levelLoader;

    public void QuitGame()
    {
        Time.timeScale = 1f;
        levelLoader.ExitToMainMenu();
    }

    public void ReturnGame()
    {
        GameManager.instance.isPaused = false;
        GameManager.instance.ResumeGame();
    }
}
