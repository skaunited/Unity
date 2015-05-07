using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture NewGame;
	public Texture HighScores;
	public Texture Credits;
	public Texture Quit;

	private int w = Screen.width;
	private int h = Screen.height;

	void Start() {
//		PlayerPrefs.DeleteAll();
	}

	void OnGUI() {
		if (GUI.Button(new Rect((w - (w / 1.85f)), (h / 2), (w / 1.85f), (h / 14)), NewGame, "button"))
			Application.LoadLevel(1);
		if (GUI.Button(new Rect((w - ((w / 1.85f) + (w / 13))), (h / 2) + (h / 8), (w / 1.85f), (h / 14)), HighScores))
			Application.LoadLevel(3);
		if (GUI.Button(new Rect((w - ((w / 1.85f) + (w / 13) * 2)), (h / 2) + (h / 8) * 2, (w / 1.85f), (h / 14)), Credits))
			Application.LoadLevel(4);
		if (GUI.Button(new Rect((w - ((w / 1.85f) + (w / 13) * 3)), (h / 2) + (h / 8) * 3, (w / 1.85f), (h / 14)), Quit))
			Application.Quit();
	}
}
