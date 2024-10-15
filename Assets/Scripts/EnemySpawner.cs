using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
    public class EnemyGroup
    {
        public GameObject[] enemies;  // List of enemies in this group
        public float spawnInterval = 30;   // Time between spawning the next group (in seconds)
    }
    [System.Serializable]
    public class Wave
    {
        public List<EnemyGroup> groups;  // List of groups in each wave
    }
public class EnemySpawner : MonoBehaviour
{

    float randomY;
    float randomX;
    Vector2 goToPos;

    float rightSideOfScreenInWorld;

    public List<Wave> waves;            // List of all waves
    public Transform spawnPoint;        // Spawn location (usually above the screen)
    public float waveDelay = 5f;        // Delay between waves (in seconds)

    private int currentWaveIndex = -1;  // Keep track of the current wave
    private int currentGroupIndex = 0;  // Keep track of the current group in the wave
    private bool isSpawning = false;    // Is currently spawning enemies

    void Start()
    {
        rightSideOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        StartNextWave();  // Start the first wave
    }

    void Update()
    {
        if (!isSpawning && AllEnemiesDefeated())
        {
            StartCoroutine(StartNextWaveAfterDelay());
        }
    }

    // Start the next wave after a delay
    IEnumerator StartNextWaveAfterDelay()
    {
        yield return new WaitForSeconds(waveDelay);
        StartNextWave();
    }

    // Start the next wave
    void StartNextWave()
    {
        currentWaveIndex++;
        if (currentWaveIndex >= waves.Count)
        {
            Debug.Log("All waves completed!");
            return;
        }

        currentGroupIndex = 0;
        isSpawning = true;
        StartCoroutine(SpawnGroup());
    }

    // Spawn the current group of enemies
    IEnumerator SpawnGroup()
    {
        Wave currentWave = waves[currentWaveIndex];
        if (currentGroupIndex < currentWave.groups.Count)
        {
            EnemyGroup group = currentWave.groups[currentGroupIndex];
            foreach (GameObject enemy in group.enemies)
            {
                randomX = Random.Range(-11f, rightSideOfScreenInWorld);
                goToPos = new Vector2(randomX, 10.32f);

                Instantiate(enemy, goToPos, Quaternion.identity);  // Spawn enemies at the spawn point
            }

            currentGroupIndex++;  // Move to the next group

            if (currentGroupIndex < currentWave.groups.Count)
            {
                yield return new WaitForSeconds(group.spawnInterval);  // Wait for the next group to spawn
                StartCoroutine(SpawnGroup());  // Spawn the next group
            }
            else
            {
                isSpawning = false;  // Finished spawning, wait for enemies to be defeated
            }
        }
    }

    // Check if all enemies are defeated
    bool AllEnemiesDefeated()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;  // Check if no enemies remain
    }
}
