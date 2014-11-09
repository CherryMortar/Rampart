using UnityEngine;
using System.Collections;

public class LossScreenScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void onGUI()
	{
		GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 50, 400, 100), "YOU LOSE!!!", loseScreenStyle());
	}
	
	private GUIStyle loseScreenStyle()
	{
		GUIStyle style = new GUIStyle();
		style.fontSize = 300;
		style.alignment = TextAnchor.MiddleCenter;
		return style;
	}
}
