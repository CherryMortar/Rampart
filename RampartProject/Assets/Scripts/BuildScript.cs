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

	// Use this for initialization
	void Start()
	{
	}
	
	public void Initialize(MainScript mainScript)
	{
		this.mainScript = mainScript;
		playField = mainScript.playField;
		playFieldSpawner = mainScript.playFieldSpawner;
		towers = LoadTowersFromResources ();
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

	void PlaceTower (float posY)
	{
		Vector2 tile = playFieldSpawner.GetTileBelowPoint (inHand.transform.position);
		Debug.Log (tile.x + " " + tile.y);
		inHand.transform.position = new Vector3 (tile.x * playFieldSpawner.tileSize.x + playFieldSpawner.tileSize.x / 2, posY, tile.y * playFieldSpawner.tileSize.y + playFieldSpawner.tileSize.y / 2);
		inHand = null;
	}

	void FixedUpdate()
	{
		if(inHand != null)
		{
			float posY = inHand.transform.localScale.y / 2;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float distance = ray.origin.y / (-ray.direction.y);
			Vector3 point = ray.GetPoint (distance);
			Vector3 snapPosition = new Vector3 (point.x, posY, point.z);
			inHand.transform.position = snapPosition;
			if(Input.GetMouseButtonDown(0))
			{
				PlaceTower (posY);
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
