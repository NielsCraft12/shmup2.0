using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;   // Array of enemy prefabs to randomly spawn
    public Transform spawnPoint;        // Spawn location (usually above the screen)
    public float waveDelay = 5f;        // Delay between waves (in seconds)

    public int baseEnemyCount = 3;      // Starting number of enemies per wave
    public int maxEnemyCount = 20;      // Maximum enemies allowed per wave
    public float spawnInterval = 1f;    // Interval between enemy spawns within a wave
    public int enemyIncrement = 1;      // Fixed number of additional enemies per wave

    private int currentWaveIndex = 0;   // Track the current wave
    private bool isSpawning = false;    // Is currently spawning enemies

    float rightSideOfScreenInWorld;

    void Start()
    {
        rightSideOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        StartNextWave();  // Start the first wave
    }

    void Update()
    {
        if (!isSpawning && AllEnemiesDefeated())
        {
            // Start the next wave only if no enemies are left and we're not already spawning
            StartCoroutine(StartNextWaveAfterDelay());
        }
    }

    // Start the next wave after a delay
    IEnumerator StartNextWaveAfterDelay()
    {
        isSpawning = true;  // Set to true here to prevent multiple coroutines
        yield return new WaitForSeconds(waveDelay);
        StartNextWave();
    }

    // Start the next wave
    void StartNextWave()
    {
        currentWaveIndex++;

        // Calculate enemy count based on the wave number, capped by maxEnemyCount
        int enemyCount = Mathf.Min(baseEnemyCount + (enemyIncrement * currentWaveIndex), maxEnemyCount);
        Debug.Log(enemyCount);
        StartCoroutine(SpawnEnemies(enemyCount));
    }

    // Spawn a specified number of random enemies
    IEnumerator SpawnEnemies(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // Randomly select an enemy type from the prefabs array
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            float randomX = Random.Range(-11f, rightSideOfScreenInWorld);
            Vector2 spawnPosition = new Vector2(randomX, 10.32f);

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);  // Spawn the enemy at the calculated position

            yield return new WaitForSeconds(spawnInterval);  // Wait before spawning the next enemy
        }

        isSpawning = false;  // Finished spawning this wave, now ready for next wave check
    }

    // Check if all enemies are defeated
    bool AllEnemiesDefeated()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;  // Check if no enemies remain
    }
}
