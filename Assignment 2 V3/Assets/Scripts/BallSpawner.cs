using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public int number;
    private float spawnRadius = 4f;
    private Vector3 spawnPosition;
    void Start()
    {
        SpawnObject();
    }

    void SpawnObject()
    {
        for (int i = 0; i < number; i++)
        {
            spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            Vector3 adjustedSpawnPosition = new Vector3(spawnPosition.x, 1.0f, spawnPosition.z);
            Instantiate(objectToSpawn, adjustedSpawnPosition, Quaternion.identity);
        }
    }
}
