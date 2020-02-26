using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public int numberOfEnemies;
    private float spawnRadius = 5f;
    private Vector3 spawnPosition;
    void Start()
    {
        SpawnObject();
    }

    void SpawnObject()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            Vector3 adjustedSpawnPosition = new Vector3(spawnPosition.x, 1.0f, spawnPosition.z);
            Instantiate(objectToSpawn, adjustedSpawnPosition, Quaternion.identity);
        }
    }
}
