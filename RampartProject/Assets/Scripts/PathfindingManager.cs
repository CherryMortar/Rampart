using UnityEngine;
using Pathfinding;
using System.Collections.Generic;

public class PathfindingManager : MonoBehaviour 
{
    protected AstarData astarData;
    protected GridGraph gridGraph;
    protected List<Seeker> seekers;
    protected PlayFieldSpawner playFieldSpawner;

	void Start () 
    {
        playFieldSpawner = GetComponent<PlayFieldSpawner>();
        astarData = AstarPath.active.astarData;

        seekers = new List<Seeker>();
        seekers.Add(new Seeker());

        gridGraph.width = (int)playFieldSpawner.fieldSize.x;
	}
	
	void Update () 
    {
	
	}

    public void FindPath(Vector3 start, Vector3 end, OnPathDelegate callback)
    {
        for (int i = 0; i < seekers.Count; i++)
        {
            if (seekers[i].IsDone())
            {
                seekers[i].StartPath(start, end, callback);
                return;
            }

            seekers.Add(new Seeker());
            seekers[seekers.Count].StartPath(start, end, callback);
        }
    }
}
