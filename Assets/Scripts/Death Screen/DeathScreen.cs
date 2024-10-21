using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public LevelLoader levelLoader;
    public void QuitGame()
    {
        Time.timeScale = 1f;
        levelLoader.ExitToMainMenu();
    }

    public void RestartLevel()
    {
        levelLoader.RestartLevel();
    }
}
