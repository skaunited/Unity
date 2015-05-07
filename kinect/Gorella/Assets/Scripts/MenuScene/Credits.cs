using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	private int w = Screen.width;
	private int h = Screen.height;

	void OnGUI() {
		GUIStyle style = new GUIStyle(GUI.skin.button);
		style.hover.textColor = Color.cyan;
		style.active.textColor = Color.cyan;
		style.fontSize = (int)(Screen.dpi / 10);

		if (GUI.Button(new Rect(0, h - (h / 12), w, (h / 12)), "Back", style) || Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel(0);
	}
}