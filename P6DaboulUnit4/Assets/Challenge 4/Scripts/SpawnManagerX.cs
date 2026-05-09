using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    public float spawnRangeX = 10;
    public float spawnZMin = 15;
    public float spawnZMax = 25;

    public int enemyCount;
    public int waveCount = 1;

    // BONUS: Difficulty Tracking
    public float enemySpeed = 100.0f;

    void Update()
    {
        // FIX: Check enemy count correctly
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveCount);
        }
    }

    // FIX: Use enemiesToSpawn parameter in for-loop
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15);

        // If no powerups remain, spawn one
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0)
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

        waveCount++;
        // BONUS: Increase speed for the next wave
        enemySpeed += 25.0f;
    }

    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }
}
