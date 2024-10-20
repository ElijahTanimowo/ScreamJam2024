using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quit");
    }

    public void ReturnGame()
    {
        GameManager.instance.isPaused = false;
        GameManager.instance.ResumeGame();
    }
}
