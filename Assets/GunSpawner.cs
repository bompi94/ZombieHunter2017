using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : MonoBehaviour {

    [SerializeField]
    float spawnTime;

    [SerializeField]
    float randomSpawnTimeModifier; 

    [SerializeField]
    GameObject[] availableGuns;

    [SerializeField]
    Transform spawnPos; 

    private void Start()
    {
        StartCoroutine(SpawnCoroutine()); 
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime + Random.Range(0, randomSpawnTimeModifier));
            Instantiate(GetGunToSpawn(), GetSpawnPosition(), Quaternion.identity);
        }
    }

    Vector2 GetSpawnPosition()
    {
        return spawnPos.position;  
    }

    GameObject GetGunToSpawn()
    {
        int index = Random.Range(0, availableGuns.Length); 
        return availableGuns[index]; 
    }
}
