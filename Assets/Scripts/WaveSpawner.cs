using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    private Wave currentWave;
    [HideInInspector]
    public int currentWaveIndex;
    private Transform player;

    [HideInInspector]
    public bool finishedSpawner;

    public GameObject boss;
    public GameObject bossSpawnPoint;

    public GameObject sacrificeUI;
    public GameObject waveFinishedUI;
    public GameObject healthBar;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));

        int initialEnemyCount = PlayerPrefs.GetInt("EnemyCount", 5);
        int enemyCount = initialEnemyCount;
        //if (PlayerPrefs.GetInt("LevelCount", 0) == 0)
        //{
        foreach (Wave wave in waves){
                wave.count = enemyCount;
                enemyCount++;
            }
        //}
        
    }

    private void Update()
    {
        if (DataHolder.player)
        {
            if (finishedSpawner == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                finishedSpawner = false;
                if (currentWaveIndex + 1 < waves.Length)
                {
                    currentWaveIndex++;
                    DataHolder.MenuShown = true;
                    StartCoroutine(WaitSacrifice());
                    StartCoroutine(StartNextWave(currentWaveIndex));
                }
                else
                {
                    Instantiate(boss, bossSpawnPoint.transform.position, bossSpawnPoint.transform.rotation);
                    healthBar.SetActive(true);
                }
            }
        }
    }

    public IEnumerator WaitSacrifice()
    {
        waveFinishedUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        waveFinishedUI.SetActive(false);
        sacrificeUI.SetActive(true);   
        sacrificeUI.GetComponent<SacrificeUI>().onShow();
    }

    public IEnumerator StartNextWave(int index)
    {
        Debug.Log("Wave "+currentWaveIndex);
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    public IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];
        for (int i = 0; i < currentWave.count; i++)
        {
            if (player == null)
            {
                yield break;
            }

            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, Quaternion.identity);

            if (i == currentWave.count - 1)
            {
                finishedSpawner = true;
            }
            else
            {
                finishedSpawner = false;
            }

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }

    public void hideSacrifice()
    {
        sacrificeUI.SetActive(false);
    }
}
