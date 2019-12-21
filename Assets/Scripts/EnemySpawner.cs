using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Configuration
    [SerializeField] List<WaveConfig> waveConfigs;
    int firstWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        WaveConfig currentWave = waveConfigs[firstWave];
        StartCoroutine(SpawnEnemiesInWave(currentWave));
    }

    IEnumerator SpawnEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetAmountOfEnemies(); enemyCount++)
        {
            Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }   
    }
}
