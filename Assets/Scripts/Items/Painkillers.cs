using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painkillers : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Transform monsters = SpawnManager.instance.transform;
            int numMonsters = SpawnManager.instance.transform.childCount;
            if (numMonsters > 0)
            {
                foreach(Transform monster in monsters)
                {
                    Destroy(monster.gameObject);
                }
            }
            Destroy(gameObject);
        }
    }
}
