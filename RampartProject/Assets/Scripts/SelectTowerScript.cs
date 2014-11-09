using UnityEngine;
using System.Collections;

public class SelectTowerScript : MonoBehaviour {
	
	public Texture2D towerInfoTexture;
	public Texture2D upgradeButtonTexture;
	
	public static GameObject selected;
	
	void Start()
	{
		selected = null;
	}

	void OnMouseDown()
	{
		if(selected == null)
			selected = this.gameObject;
		else
			selected = null;
	}
	
	void OnGUI()
	{	
		GUIStyle style;
		if(selected == this.gameObject)
		{	
			style = infoMenuStyle();
			GUI.Box(new Rect(0, Screen.height - 343, 240, 343), "", style);
			GUI.Button(new Rect(110, Screen.height - 50, 100, 25), "Upgrade", upgradeButtonStyle());
		}
	}
	
	private void showMenu()
	{
		
	}
	
	private GUIStyle infoMenuStyle()
	{
		GUIStyle style = new GUIStyle();
		style.normal.background = towerInfoTexture;
		return style;
	}
	
	private GUIStyle upgradeButtonStyle()
	{
		GUIStyle style = new GUIStyle();
		style.normal.background = upgradeButtonTexture;
		style.normal.textColor = Color.white;
		style.hover.textColor = Color.magenta;
		style.alignment = TextAnchor.MiddleCenter;
		return style;
	}
}
