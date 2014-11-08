using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnersCreator : MonoBehaviour
{

    public int countRabitsPerWave;
    public int countCavemenPerWave ;
    public int countHarpiesPerWave ;
    public int countReptilesPerWave ;
    private Queue<GameObject> enemiesInWave;
    public string CAVEMAN_NAME = "cavemen";
    public string RABBIT_NAME = "rabbit";
    public string HARPY_NAME = "Harpy";
    public string REPTILE_NAME = "reptile";
    private const string ENEMIES_FOLDER = "Enemies/";
    public ushort spawnersCount = 3;
    public Vector3[] spawnersPosition;
    public GameObject spawnerPrefab;
    // Use this for initialization
    void Start()
    {
        if (spawnersPosition.Length == spawnersCount)
        {
            enemiesInWave = CreateWave();
            for (uint i = 0; i < spawnersCount; i++)
            {
                GameObject spawner = (GameObject)Instantiate(spawnerPrefab, spawnersPosition[(int)i], Quaternion.identity);
                SpawnScript spawnerScript = spawner.GetComponent<SpawnScript>();
                spawnerScript.wave = enemiesInWave;
                spawnerScript.spawnDelay = 1f;
            }
        }
    }

    private Queue<GameObject> CreateWave()
    {
        Queue<GameObject> enemies = new Queue<GameObject>();
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
        Debug.Log(enemies.Count);
        return enemies;
    }

    private void AddItemsToQueueByName(Queue<GameObject> enemies, string enemyName, int count)
    {
        GameObject enemy = Resources.Load<GameObject>( ENEMIES_FOLDER+ enemyName) as GameObject;
        for (int i = 0; i < count; i++)
        {
            enemies.Enqueue(enemy);
        }
    }
}
