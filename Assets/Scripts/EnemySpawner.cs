using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Configuration
    [SerializeField] int firstWave = 0; // Defines starting wave.
    [SerializeField] bool loopWaves = false; // Defines if waves are to be looped.

    // Cached References
    [SerializeField] List<WaveConfig> waveConfigs; // References wave configuration file.

    // Start is called before the first frame update.
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } 
        while (loopWaves); // Loops through waves if true.
    }

    // Coroutine that handles spawning enemy waves.
    IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = firstWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            WaveConfig currentWave = waveConfigs[waveIndex]; // Assigns wave from wave config based on index.
            yield return StartCoroutine(SpawnEnemiesInWave(currentWave)); // Yields to start coroutine that handles enemy spawning.
        }
    }

    // Coroutine that handles spawning enemies.
    IEnumerator SpawnEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetAmountOfEnemies(); enemyCount++)
        {
            // TODO: Instantiate new enemies as a child of a enemy group.
            // Instantiates a new enemy based on information given by wave config, including prefab and position.
            GameObject newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig); // Assigns path to new enemy.
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns()); // Yields to wait before next spawn based on info from wave config.
        }   
    }
}
