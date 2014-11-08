using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildScript : MonoBehaviour {

	public int buttonWidth;
	public int buttonHeight;

	private List<GameObject> towers;
	private List<GameObject> playField;

	private GameObject inHand;

	private MainScript mainScript;
	private PlayFieldSpawner playFieldSpawner;

    float posY = 5;

	// Use this for initialization
	void Start()
	{
	}
	
	public void Initialize(MainScript mainScript)
	{
		this.mainScript = mainScript;
		playField = mainScript.playField;
		playFieldSpawner = mainScript.playFieldSpawner;
		towers = LoadTowersFromResources();
	}

	void GetTowerInHand (int index)
	{
		inHand = (GameObject)Instantiate (this.towers [index]);
	}

	void OnGUI ()
	{
		if(GUI.Button (new Rect(20, 40, buttonWidth, buttonHeight), "Normal Tower"))
		{
			GetTowerInHand (0);
		}

		if(GUI.Button (new Rect(20, 70, buttonWidth, buttonHeight), "Splash Tower")) 
		{
			GetTowerInHand (1);
		}

		if (GUI.Button (new Rect(20, 100, buttonWidth, buttonHeight), "Strong Tower"))
		{
			GetTowerInHand (2);
		}
	}

	void PlaceTower (Vector2 tile)
	{
        Vector3 pos = playFieldSpawner.GetTileCenter(tile);
        pos.y = posY;

        Instantiate(inHand, pos, Quaternion.identity);
        mainScript.pathfindingManager.SetWalkable(tile, false);

        Destroy(inHand);

		inHand = null;
	}

	void FixedUpdate()
	{
		if(inHand != null)
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float distance = ray.origin.y / (-ray.direction.y);
			Vector3 point = ray.GetPoint (distance);
			Vector3 snapPosition = new Vector3 (point.x, posY, point.z);
			inHand.transform.position = snapPosition;

			if(Input.GetMouseButtonDown(0))
			{
				PlaceTower(playFieldSpawner.GetTileBelowPoint(snapPosition));
			}
		}
	}

	private List<GameObject> LoadTowersFromResources()
	{
		List<GameObject> towers = new List<GameObject>();
		towers.Add((GameObject)Resources.Load("Towers/NormalTower", typeof(GameObject)));
		towers.Add((GameObject)Resources.Load("Towers/SplashTower", typeof(GameObject)));
		towers.Add((GameObject)Resources.Load("Towers/StrongTower", typeof(GameObject)));
		return towers;
	}
}
