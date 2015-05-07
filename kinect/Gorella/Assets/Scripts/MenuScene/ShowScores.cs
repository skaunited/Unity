using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

public class ShowScores : MonoBehaviour {

	#region Attributes
	public GUISkin skin;

	private int h = Screen.height;
	private int w = Screen.width;
	
	#endregion
	
	#region GUI
	void OnGUI() {

		GUI.color = Color.white;
		GUIStyle button = new GUIStyle(GUI.skin.button);
		button.fontSize = (int)(Screen.dpi / 10);
		button.hover.textColor = Color.cyan;
		button.active.textColor = Color.cyan;

		if (GUI.Button(new Rect(0, (h - (int)(h / 12)), w, (int)(h / 12)), "Back", button) || Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel(0);

		GUIStyle style = new GUIStyle();
		style.fontSize = (int)(Screen.dpi / 10);
		style.normal.textColor = Color.white;

		/*string scoreText = "Your High Score is : ";
		if (PlayerPrefs.HasKey("HighScore"))
			scoreText += PlayerPrefs.GetInt("HighScore").ToString();
		else
			scoreText += "0";
		*/
        int i = 0;
        for (; i < 10; i++)
        {
            if (!PlayerPrefs.HasKey(i + "HScore"))
                break;
            string scoreText = (i+1) + "-   " + PlayerPrefs.GetInt(i+"HScore").ToString();
            GUI.Label(new Rect((w / 2) - (w / 4), (i * 20) + (h / 12), (int)(w / 3.4f), (int)(h / 24)), scoreText, style);
        }
        if (i == 0)
            GUI.Label(new Rect((w / 2) - (w / 4), (h / 12), (int)(w / 3.4f), (int)(h / 24)), "1-    0", style);

		style.fontSize = (int)(Screen.dpi / 10);
	}
	#endregion
}