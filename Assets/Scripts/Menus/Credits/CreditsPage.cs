using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPage : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject credits;

    public void BackButton()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }
}
