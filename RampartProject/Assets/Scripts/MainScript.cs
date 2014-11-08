using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MainScript : MonoBehaviour
{
    public List<GameObject> playField;
    public PlayFieldSpawner playFieldSpawner;
    public PathfindingManager pathfindingManager;
    public BuildScript buildScript;

    // Use this for initialization
    void Start()
    {
        pathfindingManager.Initialize();

        playField = playFieldSpawner.GenerateJagtangularPlayField();
        pathfindingManager.InitGraph();
        
        buildScript.Initialize(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
