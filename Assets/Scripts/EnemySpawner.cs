using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Refences")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemy = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScaler = 0.75f;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isIsSpawning = false;

    private void Start()
    {
        StartWave();
    }

    private void Update()
    {
        if (!isIsSpawning)
        {
            return;
        }
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
    }

    private void StartWave()
    {
        isIsSpawning=true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void SpawnEnemy()
    {
        GameObject prefabsToSpawn = enemyPrefabs[0];
        Instantiate(prefabsToSpawn, LevelManager.Main.startPoint.position, Quaternion.identity);

    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemy * Mathf.Pow(currentWave, difficultyScaler));
    }

}
