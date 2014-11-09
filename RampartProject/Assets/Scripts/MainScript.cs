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
    public SpawnersCreator spawner;

    public RampartGameState gameState = RampartGameState.Splash;

    public RampartGameState GameState { get { return gameState; } set { gameState = value; } }

    // Use this for initialization
    void Start()
    {
        pathfindingManager.Initialize();

        playField = playFieldSpawner.GenerateJagtangularPlayField();
        pathfindingManager.InitGraph();

        interfaceScript.Initialize(this);
        terrainGenerator.Initialize(this);

        terrainGenerator.GenerateTerrain();

        for (int i = 0; i < playField.Count; i++)
        {
            if(!playField[i].activeSelf)
                pathfindingManager.SetWalkable(i, false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartWave()
    {
        gameState = RampartGameState.WavePhase;
        spawner = new SpawnersCreator();
    }
}

public enum RampartGameState
{
    Splash, BuildingPhase, WavePhase, Loss
}
