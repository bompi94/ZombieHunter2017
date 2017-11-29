using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : TimedMonoBehaviour
{

    float spawnTime = 0;
    float timer;

    [SerializeField]
    GameObject[] enemies;

    [SerializeField]
    int initialEnemyNumber = 4;

    int numberOfEnemies;

    [SerializeField]
    Transform[] spawnPositions;

    bool initialSpawnDone;

    GameObject player;

    protected void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
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

    protected override void TimeManagerUpdate()
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
        Vector3 pos = GetSpawnPosition();
        GameObject enemy = ChooseEnemy(); 
        Instantiate(enemy, pos, Quaternion.identity);
        numberOfEnemies++;
        UpdateSpawnTime();
    }

    GameObject ChooseEnemy()
    {
        GameObject enemy;
        enemy = enemies[Random.Range(0, enemies.Length)]; 
        return enemy; 
    }

    Vector3 GetSpawnPosition()
    {
        const float minDistanceToPlayer = 5;
        Vector3 pos;
        int index = Random.Range(0, spawnPositions.Length);
        pos = spawnPositions[index].position;

        while (player && Vector3.Distance(pos, player.transform.position) < minDistanceToPlayer)
        {
            index = Random.Range(0, spawnPositions.Length);
            pos = spawnPositions[index].position;
        }

        return pos;
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
