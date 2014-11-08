using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class MainScript : MonoBehaviour
{
    protected List<GameObject> playField;
    public PlayFieldSpawner playFieldSpawner;
    public PathfindingManager pathfindingManager;

    // Use this for initialization
    void Start()
    {
        pathfindingManager.Initialize();

        playField = playFieldSpawner.GenerateJagtangularPlayField();
        pathfindingManager.InitGraph();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
