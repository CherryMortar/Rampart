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
	private GUIStyle buttonStyle;

	private MainScript mainScript;
	private PlayFieldSpawner playFieldSpawner;

	const int MENU_WIDTH = 358;
	const int MENU_HEIGHT = 198;
	const int MARGIN_BOTTOM = 20;

	public List<GameObject> towerPrefabs;

	// Use this for initialization
	void Start()
	{
		style = buildingMenuStyle();
		buttonStyle = buildingButtonStyle();
	}
	
	public void Initialize(MainScript mainScript)
	{
		this.mainScript = mainScript;
		playField = mainScript.playField;
		playFieldSpawner = mainScript.playFieldSpawner;
	}

	void GetTowerInHand (int index)
	{
		inHand = (GameObject)Instantiate (this.towerPrefabs [index]);
	}

	void OnGUI ()
	{	
		GUI.Box(new Rect (Screen.width / 2 - MENU_WIDTH / 2, Screen.height - MENU_HEIGHT, MENU_WIDTH, MENU_HEIGHT), "", style);
		if(GUI.Button (new Rect(Screen.width/2 - buttonWidth / 2, Screen.height - buttonHeight - MARGIN_BOTTOM, buttonWidth, buttonHeight), " ", buttonStyle))
		{
			GetTowerInHand (0);
		}

		if(GUI.Button (new Rect(Screen.width/2 + buttonWidth - 30, Screen.height - buttonHeight - MARGIN_BOTTOM, buttonWidth, buttonHeight), " ", buttonStyle)) 
		{
			GetTowerInHand (1);
		}

		if (GUI.Button (new Rect(Screen.width/2 - buttonWidth - 60, Screen.height - buttonHeight - MARGIN_BOTTOM, buttonWidth, buttonHeight), " ", buttonStyle))
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
	
	private GUIStyle buildingMenuStyle() 
	{
		GUIStyle style = new GUIStyle();
		style.normal.background = background;
		style.hover.background = background;
		style.active.background = background;
		return style;
	}
	
	private GUIStyle buildingButtonStyle() 
	{
		GUIStyle style = new GUIStyle();
		style.fontSize = fontSize;
		style.normal.textColor = Color.white;
		style.hover.textColor = Color.white;
		style.active.textColor = Color.white;

		style.alignment = TextAnchor.MiddleCenter;
		return style;
	}
}
