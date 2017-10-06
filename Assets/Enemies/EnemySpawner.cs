using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    float spawnTime;
    float timer;

    [SerializeField]
    GameObject enemy;

    [SerializeField]
    Transform[] spawnPositions;

    private void Awake()
    {
        TimeManager.Instance.tick.AddListener(TimedUpdate);
    }

    void TimedUpdate()
    {
        timer += TimeManager.deltaTime;
        if (timer >= spawnTime)
        {
            Spawn();
            timer = 0;
        }
    }

    void Spawn()
    {
        //choose position to spawn
        Vector3 pos = GetSpawnPosition();
        Instantiate(enemy, pos, Quaternion.identity);
    }

    Vector3 GetSpawnPosition()
    {
        int index = Random.Range(0, spawnPositions.Length);
        return spawnPositions[index].position;
    }
}
