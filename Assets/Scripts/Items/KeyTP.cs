using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyTP : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform[] placesTP = SpawnManager.instance.placesToSpawn;

        if(placesTP.Length > 0)
        {
            int getRandomPos = Random.Range(0, placesTP.Length);
            PlayerManager.instance.player.transform.position = placesTP[getRandomPos].transform.position;
            Destroy(gameObject);
        }

    }
}
