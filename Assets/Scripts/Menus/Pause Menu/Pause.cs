using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public void QuitGame()
    {
        GameManager.instance.SwitchScenes("MainMenu");
    }

    public void ReturnGame()
    {
        GameManager.instance.isPaused = false;
        GameManager.instance.ResumeGame();
    }
}
