using UnityEngine;
using System.Collections;

public class SelectTowerScript : MonoBehaviour {
	
	public Texture2D towerInfoTexture;
	public Texture2D upgradeButtonTexture;
	
	public static bool selected;
	
	void Start()
	{
		selected = false;
	}

	void OnMouseDown()
	{
		selected = !selected;
	}
	
	void OnGUI()
	{	
		if(selected)
			showMenu();
	}
	
	private void showMenu()
	{
		GUI.Box(new Rect(0, Screen.height / 2, 250, 300), "", infoMenuStyle());
		GUI.Button(new Rect(110, Screen.height / 2 + 252, 100, 25), "Upgrade", upgradeButtonStyle());
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
		style.alignment = TextAnchor.MiddleCenter;
		return style;
	}
}
