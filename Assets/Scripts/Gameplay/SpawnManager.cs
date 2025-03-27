using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Wave currentWave;
    public Wave[] waves;
    public Transform[] spawnPoint;
    public Transform bossSpawnPoint;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public GameObject wall;
    public int enemyInWave;
    public bool[] isActivated;

    private int waveCount;
    private int enemySpawned;

    private void Start()
    {
        if (wall != null)
        {
            wall.gameObject.SetActive(false);
        }
    }

    public void Initialize()
    {
        if (wall != null)
        {
            wall.gameObject.SetActive(true);
        }

        waveCount = -1;
        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        if (isActivated[waveCount] && enemyInWave <= 0)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    IEnumerator SpawnRoutine()
    {
        enemySpawned = 0;
        waveCount++;

        if (waveCount < waves.Length)
        {
            currentWave = waves[waveCount];
            Debug.Log($"Continue to Wave {waveCount + 1}");

            currentWave = waves[waveCount];
            Debug.Log($"Start Wave {waveCount + 1}");

            while (!isActivated[waveCount])
            {
                enemySpawned++;

                if (enemySpawned > currentWave.totalEnemySpawner)
                {
                    Debug.Log($"End spawned in : Wave {waveCount + 1}");
                    isActivated[waveCount] = true;
                }
                else
                {
                    Spawn();
                    yield return new WaitForSeconds(currentWave.spawnInterval);
                }
            }
        }
        else
        {
            if (bossPrefab != null)
            {
                Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
            }

            if (wall != null)
            {
                wall.gameObject.SetActive(false);
            }

            Debug.Log($"End all the {waveCount + 1} waves");
            yield break;
        }
    }

    private void Spawn()
    {
        int r = Random.Range(0, spawnPoint.Length);
        Instantiate(enemyPrefab, spawnPoint[r].position, Quaternion.identity);
        enemyInWave++;
    }
}
