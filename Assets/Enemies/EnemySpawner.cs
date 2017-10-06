using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    float spawnTime = 0; 
    float timer;

    [SerializeField]
    GameObject enemy;

    [SerializeField]
    int initialEnemyNumber = 4;

    int numberOfEnemies; 

    [SerializeField]
    Transform[] spawnPositions;

    bool initialSpawnDone; 

    private void Awake()
    {
        TimeManager.Instance.tick.AddListener(TimedUpdate);
        InitialSpawn();
    }

    void InitialSpawn()
    {
        for (int i = 0; i < initialEnemyNumber; i++)
        {
            Spawn(); 
        }

        initialSpawnDone = true; 
    }

    void TimedUpdate()
    {
        if (initialSpawnDone)
        {
            timer += TimeManager.deltaTime;
            if (timer >= spawnTime)
            {
                Spawn();
                timer = 0;
            }
        }
    }

    void Spawn()
    {
        //choose position to spawn
        Vector3 pos = GetSpawnPosition();
        Instantiate(enemy, pos, Quaternion.identity);
        numberOfEnemies++;
        UpdateSpawnTime(); 
    }

    Vector3 GetSpawnPosition()
    {
        int index = Random.Range(0, spawnPositions.Length);
        return spawnPositions[index].position;
    }

    public void EnemyDead()
    {
        numberOfEnemies--;
        UpdateSpawnTime(); 
    }

    void UpdateSpawnTime()
    {
        spawnTime = numberOfEnemies; 
    }
}
