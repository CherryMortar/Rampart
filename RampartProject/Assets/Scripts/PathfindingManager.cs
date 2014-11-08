using UnityEngine;
using Pathfinding;
using System.Collections.Generic;

public class PathfindingManager : MonoBehaviour 
{
    protected AstarData astarData;
    protected GridGraph gridGraph;
    protected List<Seeker> seekers;
    protected PlayFieldSpawner playFieldSpawner;

    public int nodesPerTile = 1;

    void Start() { }

    void Update() { }

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
        gridGraph.width = (int)fieldSize.x*nodesPerTile;
        gridGraph.depth = (int)fieldSize.y*nodesPerTile;
        gridGraph.nodeSize = playFieldSpawner.tileSize.x / nodesPerTile;
        gridGraph.collision.collisionCheck = false;
        gridGraph.collision.heightCheck = false;
        gridGraph.center = playFieldSpawner.GetTileCenter(new Vector2(fieldSize.x / 2, fieldSize.y / 2)) - new Vector3(tileSize.x/2, 0, tileSize.y/2);
        gridGraph.UpdateSizeFromWidthDepth();

        AstarPath.active.Scan();
    }

    private List<int> GetNodeIndexesInTile(int tileIndex)
    {
        List<int> nodes = new List<int>();

        Vector2 fieldSize = playFieldSpawner.fieldSize;
        int prevLines = (int)(fieldSize.x * nodesPerTile * (tileIndex / (int)fieldSize.x) * nodesPerTile);

        for (int line = 0; line < nodesPerTile; line++)
        {
            for (int i = 0; i < nodesPerTile; i++)
            {
                nodes.Add((int)(prevLines + line * fieldSize.x * nodesPerTile + (tileIndex % (int)fieldSize.x) * nodesPerTile + i));
            }
        }

        return nodes;
    }

    public void SetWalkable(int tileIndex, bool walkable)
    {
        foreach (int i in GetNodeIndexesInTile(tileIndex))
            gridGraph.nodes[i].Walkable = walkable;
    }

    public void SetWalkable(Vector2 tile, bool walkable)
    {
        SetWalkable((int)(tile.x + tile.y * playFieldSpawner.fieldSize.x), walkable);
    }

    public bool IsWalakble(Vector2 tile)
    {
        return gridGraph.nodes[GetNodeIndexesInTile((int)(tile.x + tile.y * playFieldSpawner.fieldSize.x))[0]].Walkable;
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
        }

        seekers.Add(this.gameObject.AddComponent<Seeker>());
        seekers[seekers.Count-1].StartPath(start, end, callback);
    }
}
