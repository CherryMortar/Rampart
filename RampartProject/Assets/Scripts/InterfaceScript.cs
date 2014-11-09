using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterfaceScript : MonoBehaviour {

	public int buttonWidth;
	public int buttonHeight;
	public int fontSize;
	public Texture2D background;

	private List<GameObject> playField;

	private GameObject inHand;
	private GameObject[] towers;
	
	private GUIStyle style;
	private GUIStyle buttonStyle;
	private GUIStyle startWaveButtonStyle;
	private Color color;

	private MainScript mainScript;
	private PlayFieldSpawner playFieldSpawner;
	
	private RampartGameState gameState;

	private int tileIndex;

	const int MENU_WIDTH = 358;
	const int MENU_HEIGHT = 198;
	const int MARGIN_BOTTOM = 20;
	const int START_WAVE_BTN_WIDTH = 200;
	const int START_WAVE_BTN_HEIGHT = 30;

	public List<GameObject> towerPrefabs;

	// Use this for initialization
	void Start()
	{
		style = buildingMenuStyle();
		buttonStyle = buildingButtonStyle();
		startWaveButtonStyle = startWaveStyle();
	}
	
	public void Initialize(MainScript mainScript)
	{
		this.mainScript = mainScript;
		gameState = mainScript.GameState;
		playField = mainScript.playField;
		playFieldSpawner = mainScript.playFieldSpawner;
	}

	void GetTowerInHand (int index)
	{
		inHand = (GameObject)Instantiate (this.towerPrefabs [index]);
	}

	void OnGUI ()
	{
		gameState = mainScript.GameState;
		switch(gameState)
		{
		case RampartGameState.Splash:
			gameObject.GetComponent<SplashScreenScript>().enabled = true;
			break;
		case RampartGameState.BuildingPhase:
			if(GUI.Button(new Rect(Screen.width/2 - START_WAVE_BTN_WIDTH/2, START_WAVE_BTN_HEIGHT/2, START_WAVE_BTN_WIDTH, START_WAVE_BTN_HEIGHT), "Start wave", startWaveButtonStyle))
			{
				towers = GameObject.FindGameObjectsWithTag("Building");
				foreach(GameObject tower in towers)
				{
	                SelectTowerScript selectScript = tower.GetComponent<SelectTowerScript>();
	                if(selectScript != null)
					    selectScript.enabled = false;
				}
	            inHand = null;
	            mainScript.StartWave();
			}
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
			break;
		case RampartGameState.WavePhase:
			break;
		case RampartGameState.Loss:
			mainScript.GetComponent<LossScreenScript>().enabled = true;
			break;
		}
	}

	GameObject PlaceTower (Vector2 tile)
	{
		Vector3 pos = playFieldSpawner.GetTileCenter(tile);

		GameObject placedTower = (GameObject)Instantiate(inHand, pos, Quaternion.identity);
		mainScript.pathfindingManager.SetWalkable(tile, false);

		Destroy(inHand);

		inHand = null;

        return placedTower;
	}

	void FixedUpdate()
	{
		if(inHand != null)
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float distance = ray.origin.y / (-ray.direction.y);
			Vector3 point = ray.GetPoint (distance);
			Vector3 groundPosition = new Vector3 (point.x, 0, point.z);

			tileIndex = playFieldSpawner.GetTileIndex(playFieldSpawner.GetTileBelowPoint(groundPosition));
			inHand.transform.position = groundPosition;

			if(Input.GetMouseButtonDown(0) && tileIndex < playField.Count && !playField[tileIndex].activeSelf)
			{
				playField[tileIndex] = PlaceTower(playFieldSpawner.GetTileBelowPoint(groundPosition));
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
		style.normal.textColor = Color.white;
		style.hover.textColor = Color.white;
		style.active.textColor = Color.white;

		style.alignment = TextAnchor.MiddleCenter;
		return style;
	}

	private GUIStyle startWaveStyle() 
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
