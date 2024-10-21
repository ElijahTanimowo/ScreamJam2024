using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public void BackButton()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
