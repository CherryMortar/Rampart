using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildScript : MonoBehaviour {

	public int buttonWidth;
	public int buttonHeight;
	private bool buildNormalTower;
	private bool buildSplashTower;
	private bool buildSlowTower;
	private List<GameObject> towers; 

	// Use this for initialization
	void Start()
	{
		towers = LoadTowersFromResources ();
	}

	void OnGUI ()
	{
		if(GUI.Button (new Rect(20, 40, buttonWidth, buttonHeight), "Normal Tower"))
		{
			Instantiate(this.towers[0]);
		}

		if(GUI.Button (new Rect(20, 70, buttonWidth, buttonHeight), "Splash Tower")) 
		{
			Instantiate(this.towers[1]);
		}

		if (GUI.Button (new Rect(20, 100, buttonWidth, buttonHeight), "SlowTower"))
		{
			Instantiate(this.towers[2]);
		}
	}

	private List<GameObject> LoadTowersFromResources()
	{
		List<GameObject> towers = new List<GameObject>();
		towers.Add((GameObject)Resources.Load("Towers/NormalTower", typeof(GameObject)));
		towers.Add((GameObject)Resources.Load("Towers/SplashTower", typeof(GameObject)));
		towers.Add((GameObject)Resources.Load("Towers/SlowTower", typeof(GameObject)));
		return towers;
	}
}
