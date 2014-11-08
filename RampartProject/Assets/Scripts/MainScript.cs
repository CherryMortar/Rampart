using UnityEngine;
using Pathfinding;
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
