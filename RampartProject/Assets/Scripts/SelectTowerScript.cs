using UnityEngine;
using System.Collections;

public class SelectTowerScript : MonoBehaviour {
	
	void OnMouseDown()
	{
		Debug.Log("Clicked");
	}
	
	void OnGUI()
	{
		
	}
	
	private GUI generateBox()
	{
			
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
