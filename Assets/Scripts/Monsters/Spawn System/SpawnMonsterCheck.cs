using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsterCheck : MonoBehaviour
{
    public bool playerInArea = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            playerInArea = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>()) 
        {
            playerInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>()) 
        {
            playerInArea = false;
        }
    }
}
