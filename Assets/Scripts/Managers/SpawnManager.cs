using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [Header("Monster Spawn Info")]
    [SerializeField] GameObject[] monsters;
    [SerializeField] GameObject[] spawnAreaCollider;
    [SerializeField] Vector2 spawnCheckSize = new Vector2(1, 1);
    [SerializeField] int maxAttemptToSpawn = 3;

    [Header("Player body Spawn Info")]
    [SerializeField] Collider2D playerBodySpawn;

    [Header("Items Info")]
    [SerializeField] GameObject[] items;
    public Transform[] placesToSpawn;

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

    public void CanSpawn()
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
                    //Debug.Log(spawnArea.gameObject.name);
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
            GameObject monster = Instantiate(monsters[index], randomSpawnPoint, Quaternion.identity);
            monster.transform.SetParent(this.transform);
            Destroy(monster, monster.GetComponent<MonsterBase>().lifeTime);

        }

    }

    public void CanSpawnPlayerBody(GameObject _playerBodyPrefab)
    {


        Vector2 randomSpawnPoint = Vector2.zero;

        //Get the size of the spawn
        Bounds spawnBound = playerBodySpawn.bounds;

        while (true)
        {
            //Get Random spawn
            randomSpawnPoint = new Vector2(
                Random.Range(spawnBound.min.x, spawnBound.max.x),
                Random.Range(spawnBound.min.y, spawnBound.max.y)
                );

            //Check can spawn
            if (CheckCanSpawn(randomSpawnPoint))
            {
                GameManager.instance.playerBodySpawned = true;
                break;
            }
        }
        Instantiate(_playerBodyPrefab, randomSpawnPoint, Quaternion.identity);
    }

    /// <summary>
    /// Clear the Monster in Spawn Manager
    /// </summary>
    public void ClearMonsters()
    {
        foreach (Transform monster in this.transform)
        {
            Destroy(monster.gameObject);
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
