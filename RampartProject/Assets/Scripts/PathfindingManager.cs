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
        
	}
	
	void Update () 
    {
	
	}

    public void Initialize()
    {
        playFieldSpawner = GetComponent<PlayFieldSpawner>();
        astarData = AstarPath.active.astarData;

        seekers = new List<Seeker>();
    }

    public void InitGraph()
    {
        Vector2 fieldSize = playFieldSpawner.fieldSize;
        Vector2 tileSize = playFieldSpawner.tileSize;

        gridGraph = astarData.AddGraph(typeof(GridGraph)) as GridGraph;
        gridGraph.width = (int)fieldSize.x;
        gridGraph.depth = (int)fieldSize.y;
        gridGraph.nodeSize = (int)playFieldSpawner.tileSize.x;
        gridGraph.collision.collisionCheck = false;
        gridGraph.collision.heightCheck = false;
        gridGraph.center = playFieldSpawner.GetTileCenter(new Vector2(fieldSize.x / 2, fieldSize.y / 2)) - new Vector3(tileSize.x/2, 0, tileSize.y/2);
        gridGraph.UpdateSizeFromWidthDepth();

        AstarPath.active.Scan();
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

            seekers.Add(this.gameObject.AddComponent<Seeker>());
            seekers[seekers.Count].StartPath(start, end, callback);
        }
    }
}
