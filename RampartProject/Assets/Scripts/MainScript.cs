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
    
    public int money;

    // Use this for initialization
    void Start()
    {
        pathfindingManager.Initialize();

        playField = playFieldSpawner.GenerateJagtangularPlayField();
        pathfindingManager.InitGraph();

		money = 0;
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
    void Update()
    {

    }

    public void StartWave()
    {
        gameState = RampartGameState.WavePhase;
        SpawnersCreator spawner = gameObject.AddComponent<SpawnersCreator>();
        spawner.CreateSpawners(playFieldSpawner);
    }
}

public enum RampartGameState
{
    Splash, BuildingPhase, WavePhase, Loss
}
