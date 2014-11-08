using UnityEngine;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour {

    public List<GameObject> terrainTiles;
    public Vector2 terrainTileSize;
    public int terrainRings = 1;

    protected MainScript mainScript;

    public void Initialize(MainScript mainScript)
    {
        this.mainScript = mainScript;
    }

    public void GenerateTerrain()
    {
        Vector2 tileSize = mainScript.playFieldSpawner.tileSize;
        Vector2 fieldSize = mainScript.playFieldSpawner.fieldSize;

        for (int ring = 0; ring < terrainRings; ring++)
        {
            for (float x = -terrainTileSize.x / 2 - terrainTileSize.x * ring; x <= fieldSize.x * tileSize.x + terrainTileSize.x / 2 + terrainTileSize.x * ring; x += terrainTileSize.x)
            {
                Instantiate(terrainTiles[Random.Range(0, 4)], new Vector3(x, 0, fieldSize.y * tileSize.y + terrainTileSize.y / 2 + terrainTileSize.y * ring), Quaternion.identity);
                Instantiate(terrainTiles[Random.Range(0, 4)], new Vector3(x, 0, -terrainTileSize.y / 2 - terrainTileSize.y * ring), Quaternion.AngleAxis(180, new Vector3(0, 1, 0)));
            }

            for (float y = terrainTileSize.y / 2 - terrainTileSize.y * ring; y <= fieldSize.y * tileSize.y - terrainTileSize.y / 2 + terrainTileSize.y * ring; y += terrainTileSize.y)
            {
                Instantiate(terrainTiles[Random.Range(0, 4)], new Vector3(fieldSize.x * tileSize.x + terrainTileSize.x / 2 + terrainTileSize.x * ring, 0, y), Quaternion.AngleAxis(90, new Vector3(0, 1, 0)));
                Instantiate(terrainTiles[Random.Range(0, 4)], new Vector3(-terrainTileSize.x / 2 - terrainTileSize.x * ring, 0, y), Quaternion.AngleAxis(270, new Vector3(0, 1, 0)));
            }
        }
    }
}
