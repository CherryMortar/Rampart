using UnityEngine;
using System.Collections;

public class SelectTowerScript : MonoBehaviour {
	
	public Texture2D towerInfoTexture;
	public Texture2D upgradeButtonTexture;
	
	public static GameObject selected;
	public GameObject upgradePrefab;

	const int INFO_MENU_HEIGHT = 343;
	const int INFO_MENU_WIDTH = 240;
	
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
			GUI.Box(new Rect(0, Screen.height - INFO_MENU_HEIGHT, INFO_MENU_WIDTH, INFO_MENU_HEIGHT), "", style);
			GUI.Label(new Rect(60, Screen.height - 52, 100, 25), "INSERT CASH HERE", moneyStyle());
			if(upgradePrefab != null)
			{
				if(GUI.Button(new Rect(110, Screen.height - 52, 100, 25), "Upgrade", upgradeButtonStyle()))
				{
					Instantiate(upgradePrefab, gameObject.transform.position, Quaternion.identity);
					Destroy(gameObject);
				}
			}
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
		style.fontSize = 20;
		style.hover.textColor = Color.yellow;
		style.normal.textColor = Color.white;
		style.onHover.textColor = Color.magenta;
		style.active.textColor = Color.black;
		style.alignment = TextAnchor.MiddleCenter;
		return style;
	}
	
	private GUIStyle moneyStyle()
	{
		GUIStyle style = new GUIStyle();
		style.fontSize = 20;
		style.normal.textColor = Color.black;
		style.alignment = TextAnchor.MiddleCenter;
		return style;
	}
}
