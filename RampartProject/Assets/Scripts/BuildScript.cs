using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildScript : MonoBehaviour {

	public int buttonWidth;
	public int buttonHeight;
	public int fontSize;
	public Texture2D background;
	

	private List<GameObject> playField;

	private GameObject inHand;
	
	private GUIStyle style;

	private MainScript mainScript;
	private PlayFieldSpawner playFieldSpawner;

    public List<GameObject> towerPrefabs;

	// Use this for initialization
	void Start()
	{
		style = generateStyle();
	}
	
	public void Initialize(MainScript mainScript)
	{
		this.mainScript = mainScript;
		playField = mainScript.playField;
		playFieldSpawner = mainScript.playFieldSpawner;
		//towers = LoadTowersFromResources();
	}

	void GetTowerInHand (int index)
	{
		inHand = (GameObject)Instantiate (this.towerPrefabs [index]);
	}

	void OnGUI ()
	{	
		if(GUI.Button (new Rect(20, 40, buttonWidth, buttonHeight), "Normal Tower", style))
		{
			GetTowerInHand (0);
		}

		if(GUI.Button (new Rect(20, 100, buttonWidth, buttonHeight), "Splash Tower", style)) 
		{
			GetTowerInHand (1);
		}

		if (GUI.Button (new Rect(20, 160, buttonWidth, buttonHeight), "Strong Tower", style))
		{
			GetTowerInHand (2);
		}
	}

	void PlaceTower (Vector2 tile)
	{
        Vector3 pos = playFieldSpawner.GetTileCenter(tile);

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
			Vector3 snapPosition = new Vector3 (point.x, 0, point.z);
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
		towers.Add((GameObject)Resources.Load("Towers/Tower_Wood_Light", typeof(GameObject)));
        towers.Add((GameObject)Resources.Load("Towers/Tower_Wood_Splash", typeof(GameObject)));
        towers.Add((GameObject)Resources.Load("Towers/Tower_Wood_Heavy", typeof(GameObject)));
		return towers;
	}
	
	private GUIStyle generateStyle() 
	{
		GUIStyle style = new GUIStyle();
		style.fontSize = fontSize;
		style.normal.textColor = Color.white;
		style.hover.textColor = Color.white;
		style.active.textColor = Color.white;
		style.normal.background = background;
		style.hover.background = background;
		style.active.background = background;
		style.alignment = TextAnchor.MiddleCenter;
		return style;
	}
}
