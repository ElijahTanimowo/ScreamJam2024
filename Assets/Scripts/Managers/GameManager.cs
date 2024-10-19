using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Transform player;
    [SerializeField] float spawnCooldown = 5f;
    [SerializeField] bool onCooldown = false;

    private void Awake()
    {
        //Game Manager in world, destory
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get Player transform
        if (player != null)
        {
            player = PlayerManager.instance.player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SpawnMonstersTimer());
    }

    IEnumerator SpawnMonstersTimer()
    {
        while (true)
        {
            if (!onCooldown)
            {
                SpawnMonsters();
                onCooldown = true;
                yield return new WaitForSeconds(spawnCooldown);
                onCooldown = false;
            }
            yield return null;
        }
    }

    void SpawnMonsters()
    {
        SpawnManager.instance.CanSpawn(player);
    }
}
