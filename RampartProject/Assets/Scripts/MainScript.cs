using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MainScript : MonoBehaviour
{
    public List<GameObject> playField;
    public PlayFieldSpawner playFieldSpawner;
    public PathfindingManager pathfindingManager;
    public InterfaceScript interfaceScript;
    public TerrainGenerator terrainGenerator;

    public GameObject citadel;

    public RampartGameState gameState = RampartGameState.Splash;

    public RampartGameState GameState { get { return gameState; } set { gameState = value; } }
    
    public static int money;

    protected bool waveHappening = false;

    // Use this for initialization
    void Start()
    {
        pathfindingManager.Initialize();

        playField = playFieldSpawner.GenerateJagtangularPlayField();
        pathfindingManager.InitGraph();

		money = 12210;
        interfaceScript.Initialize(this);
        terrainGenerator.Initialize(this);

        terrainGenerator.GenerateTerrain();

        for (int i = 0; i < playField.Count; i++)
        {
            if(!playField[i].activeSelf)
                pathfindingManager.SetWalkable(i, false);
        }
        Vector3 pos = playFieldSpawner.GetTileCenter(new Vector2(playFieldSpawner.fieldSize.x / 2, playFieldSpawner.fieldSize.y / 2));

        playField[playField.Count/2] = (GameObject)Instantiate(citadel, pos, Quaternion.identity);
        pathfindingManager.SetWalkable(playField.Count / 2, false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (waveHappening && GameObject.FindGameObjectWithTag("Enemy") == null)
            onWaveOver();
    }

    public void StartWave()
    {
        gameState = RampartGameState.WavePhase;
        SpawnersCreator spawner = gameObject.AddComponent<SpawnersCreator>();
        spawner.CreateSpawners(playFieldSpawner, onSpawnOver);
    }

    public void onSpawnOver()
    {
        Debug.Log("Spawn over");
        waveHappening = true;
    }

    public void onWaveOver()
    {
        Debug.Log("Wave over");

        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

        foreach (GameObject building in buildings)
        {
            SelectTowerScript sts = building.GetComponent<SelectTowerScript>();
            if (sts != null)
                sts.enabled = true;
        }

        gameState = RampartGameState.BuildingPhase;
        waveHappening = false;
    }
}

public enum RampartGameState
{
    Splash, BuildingPhase, WavePhase, Loss
}
