using UnityEngine;
using System.Collections;

public class SplashScreenScript : MonoBehaviour {

	public Texture2D logo;
	public Font font;
	public int fontSize;
	public int paddingTop;

	// Use this for initialization
	void Start ()
	{
	}
	
	void OnGUI()
	{
		GUI.skin.font = font;
		
		if(GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "Click to start", style()))
		{
			enabled = false;
		}
	}
	
	GUIStyle style() 
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
