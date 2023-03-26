using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 1f;
    float spawnRadius;
    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        spawnRadius = Random.Range(-9f, 9f);
        Vector2 spawnPosition = new Vector2(spawnRadius, transform.position.y);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}