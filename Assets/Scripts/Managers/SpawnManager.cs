using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField] GameObject[] monsters;
    [SerializeField] GameObject[] spawnAreaCollider;
    [SerializeField] Vector2 spawnCheckSize = new Vector2(1, 1);
    [SerializeField] int maxAttemptToSpawn = 3;

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

    public void CanSpawn(Transform _player)
    {
        foreach (GameObject spawnArea in spawnAreaCollider) 
        {
            //get components
            SpawnMonsterCheck area = spawnArea.GetComponent<SpawnMonsterCheck>();
            BoxCollider2D areaCollider = area.GetComponent<BoxCollider2D>();

            //Check Component exist in object
            if (area && areaCollider) 
            {
                //Check Player is not in the area
                if (!area.playerInArea)
                {
                    //Spawn Monster in that area
                    SpawnEnemy(areaCollider);
                }
            }
        }
    }

    /// <summary>
    /// Spawn Enemies
    /// </summary>
    private void SpawnEnemy(BoxCollider2D _spawnAreaCollider)
    {
        
        Vector2 randomSpawnPoint = Vector2.zero;
        bool spawnPosFound = false;

        //Get the size of the spawn
        Bounds spawnBound = _spawnAreaCollider.bounds;

        //Keep trying to spawn monster
        for (int i = 0; i < maxAttemptToSpawn; i++) 
        {
            //Get Random spawn
            randomSpawnPoint = new Vector2(
                Random.Range(spawnBound.min.x, spawnBound.max.x),
                Random.Range(spawnBound.min.y, spawnBound.max.y)
                );

            //Check can spawn in area
            if (CheckCanSpawn(randomSpawnPoint))
            {
                spawnPosFound = true;
                break;
            }
        }

        //Check can spawn
        if (spawnPosFound) 
        {
            int index = Random.Range(0, monsters.Length);
            Instantiate(monsters[index], randomSpawnPoint, Quaternion.identity);
        }

    }

    /// <summary>
    /// Check monster can spawn in coords
    /// </summary>
    /// <param name="randomSpawnPoint"></param>
    /// <returns></returns>
    private bool CheckCanSpawn(Vector2 randomSpawnPoint)
    {
        //get all positions in box
        Collider2D[] spawnOpen = Physics2D.OverlapBoxAll(randomSpawnPoint, spawnCheckSize, 0f);

        //Check spawn point contains Obstacle
        foreach (Collider2D pos in spawnOpen)
        {
            if (pos.CompareTag("Obstacle"))
            {
                //Cannot spawn
                return false;
            }
        }
        //Can Spawn
        return true;
    }
}
