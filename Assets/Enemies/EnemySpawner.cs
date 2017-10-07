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

    GameObject player;

    private void Awake()
    {
        TimeManager.Instance.tick.AddListener(TimedUpdate);
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
        Vector3 pos = GetSpawnPosition();
        Instantiate(enemy, pos, Quaternion.identity);
        numberOfEnemies++;
        UpdateSpawnTime();
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
