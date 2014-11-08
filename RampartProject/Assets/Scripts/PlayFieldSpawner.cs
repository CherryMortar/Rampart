using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayFieldSpawner : MonoBehaviour 
{

    public GameObject tile;
    public Vector2 tileSize = new Vector2(1, 1);
    public Vector2 fieldSize = new Vector2(8, 3);

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public List<GameObject> GeneratePlayField(int side)
    {
        List<GameObject> tiles = new List<GameObject>();

        for (int i = 0; i < side; i++)
            for (int j = 0; j < side; j++)
            {
                tiles.Add((GameObject)Instantiate(tile, GetTileCenter(new Vector2(i, j)), Quaternion.identity));
            }

        return tiles;
    }

    public List<GameObject> GenerateJagtangularPlayField()
    {
        return GenerateJagtangularPlayField((int)fieldSize.x, (int)fieldSize.y);
    }

    public List<GameObject> GenerateJagtangularPlayField(int w, int h)
    {
        List<GameObject> tiles = new List<GameObject>();

        for (int i = 0; i < w; i++)
            for (int j = 0; j < w; j++)
            {
                tiles.Add((GameObject)Instantiate(tile, GetTileCenter(new Vector2(i, j)), Quaternion.identity));

                if (Mathf.Abs(i - j) >= h)
                    tiles[tiles.Count - 1].SetActive(false);
            }

        return tiles;
    }

    public int GetTileIndex(Vector2 tile)
    {
        return (int)(tile.x + fieldSize.x*tile.y);
    }

    public Vector2 GetTileBelowPoint(Vector3 pt)
    {
        return new Vector2(Mathf.Round(pt.x / tileSize.x), Mathf.Round(pt.y / tileSize.y));
    }

    public Vector3 GetTileCenter(Vector2 tile)
    {
        return new Vector3(tile.x * tileSize.x + tileSize.x / 2, 0, tile.y * tileSize.y + tileSize.y / 2);
    }
}
