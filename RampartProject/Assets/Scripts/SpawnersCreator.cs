using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpawnersCreator : MonoBehaviour
{

    public int level = 0;
    private int countRabitsPerWave;
    private int countCavemenPerWave;
    private int countHarpiesPerWave;
    private int countReptilesPerWave;
    private const int MAX_ENEMIES_IN_WAVE_BY_TYPE = 30;
    private const int MAX_LEVEL = 5;
    private Queue<GameObject> enemiesInWave;
    private string CAVEMAN_NAME = "caveman";
    private string RABBIT_NAME = "rabbit";
    private string HARPY_NAME = "Harpy";
    private string REPTILE_NAME = "Reptile";
    private const string ENEMIES_FOLDER = "Enemies/";

    private string[] enemiesFileNames = new string[] { "Reptile", "caveman", "Harpy" };
    public ushort spawnersCount = 3;
    private Vector3[] spawnersPosition = new Vector3[] { new Vector3(10, 5, 80), new Vector3(60, 5, 10), new Vector3(20, 5, 180) };

    private GameObject spawnerPrefab;
    private float spawnDelayForSpawners;

    public void CreateSpawners(PlayFieldSpawner playFieldSpawner)
    {
        spawnerPrefab = Resources.Load<GameObject>("Spawner") as GameObject;
        Debug.Log("IN");
        spawnDelayForSpawners = MAX_LEVEL - level;
        countRabitsPerWave = 0;
        countCavemenPerWave = Random.Range(0, MAX_ENEMIES_IN_WAVE_BY_TYPE);
        countHarpiesPerWave = Random.Range(0, MAX_ENEMIES_IN_WAVE_BY_TYPE);
        countReptilesPerWave = Random.Range(0, MAX_ENEMIES_IN_WAVE_BY_TYPE);
        if (spawnersPosition.Length == spawnersCount)
        {
            enemiesInWave = CreateWave();
            for (uint i = 0; i < spawnersCount; i++)
            {
                Vector3 position = new Vector3(4 * playFieldSpawner.tileSize.x, 0, 2 * playFieldSpawner.tileSize.y);
                GameObject spawner = (GameObject)Instantiate(spawnerPrefab, spawnersPosition[i], Quaternion.identity);
                SpawnScript spawnerScript = spawner.GetComponent<SpawnScript>();
                spawnerScript.wave = enemiesInWave;
                spawnerScript.spawnDelay = spawnDelayForSpawners;
            }
        }
    }


    private Queue<GameObject> CreateWave()
    {
        List<GameObject> enemies = new List<GameObject>();
        if (countCavemenPerWave > 0)
        {
            AddItemsToQueueByName(enemies, CAVEMAN_NAME, countCavemenPerWave);
        }
        if (countHarpiesPerWave > 0)
        {
            AddItemsToQueueByName(enemies, HARPY_NAME, countHarpiesPerWave);
        }
        if (countRabitsPerWave > 0)
        {
            AddItemsToQueueByName(enemies, RABBIT_NAME, countRabitsPerWave);
        }
        if (countReptilesPerWave > 0)
        {
            AddItemsToQueueByName(enemies, REPTILE_NAME, countReptilesPerWave);
        }

        List<GameObject> shuffledList = new List<GameObject>();

        while (enemies.Count > 0)
        {
            int randpos = Random.Range(0, enemies.Count - 1);
            shuffledList.Add(enemies[randpos]);
            enemies.RemoveAt(randpos);
        }

        Queue<GameObject> enemyQueue = new Queue<GameObject>(shuffledList);
        return enemyQueue;
    }

    private void AddItemsToQueueByName(List<GameObject> enemies, string enemyName, int count)
    {
        GameObject enemy = Resources.Load<GameObject>(ENEMIES_FOLDER + enemyName) as GameObject;
        for (int i = 0; i < count; i++)
        {
            enemies.Add(enemy);
        }
    }
}
