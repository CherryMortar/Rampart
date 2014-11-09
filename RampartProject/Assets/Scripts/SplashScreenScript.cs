using UnityEngine;
using System.Collections;

public class SplashScreenScript : MonoBehaviour {

	public Texture2D logo;
	public Font font;
	public int fontSize;
	public int paddingTop;
	public MainScript mainScript;
	//public MovieTexture video;

	// Use this for initialization
	void Start ()
	{
		this.gameObject.GetComponent<InterfaceScript> ().enabled = false;
		mainScript = this.gameObject.GetComponent<MainScript>();
	}
	
	void OnGUI()
	{
		GUI.skin.font = font;
		//GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), video);
		
		if(GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "Click to start", style()))
		{
			this.gameObject.GetComponent<InterfaceScript> ().enabled = true;
			mainScript.GameState = RampartGameState.BuildingPhase;
			enabled = false;
		}
	}
	
	private GUIStyle style() 
	{
		GUIStyle style = new GUIStyle();
		style.fontSize = fontSize;
		style.normal.textColor = Color.white;
		style.hover.textColor = Color.white;
		style.active.textColor = Color.white;
		style.normal.background = logo;
		style.hover.background = logo;
		style.active.background = logo;
		style.alignment = TextAnchor.MiddleCenter;
		style.padding.top = paddingTop;
		return style;
	}
}
