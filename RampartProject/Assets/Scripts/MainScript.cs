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
    public BuildScript buildScript;
    public TerrainGenerator terrainGenerator;

    // Use this for initialization
    void Start()
    {
        pathfindingManager.Initialize();

        playField = playFieldSpawner.GenerateJagtangularPlayField();
        pathfindingManager.InitGraph();

        buildScript.Initialize(this);
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
}
