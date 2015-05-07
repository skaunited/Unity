using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	#region variables
    public GUISkin myskin;
	public bool paused = false;
	public static bool vibration = true;

	public Texture PauseTexture;
	public Texture vibraOn;
	public Texture vibraOff;
	public Texture resume;
	public Texture options;
	public Texture newGame;
	public Texture quit;
	public Texture MainMenu;
	public Texture mute;
	public Texture muted;

	private Rect windowRect;
	private Rect windowVibra;
	private Rect waitRect;
	private Rect rect;
	private int path = 0;
	private float now;

	private int h = Screen.height;
	private int w = Screen.width;
	#endregion

	#region Initialisation
    private void Start() {
		rect  = new Rect(10, 10, (h / 12), (h / 12));
		waitRect = new Rect(Screen.width / 2 - 30, Screen.height / 2 - 30, 100, 75);
        windowRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 320);
		//windowVibra = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 290);
	}

	private void OnGUI () {
		if (!paused)
		{
			if (GUI.Button(rect, PauseTexture))
				paused = true;
		}
		if (paused)
		{
			if (path == 0)
	            windowRect = GUI.Window(0, windowRect, windowFunc, "Pause Menu");
			else if (path == 1)
				windowRect = GUI.Window(0, windowRect, windowOptions, "Options");
			else
				windowWait();
		}
	}
	#endregion

	#region Menus
	private void windowFunc(int id) {
		if (GUILayout.Button(resume))
		{
			now = Time.realtimeSinceStartup;
			path = 2;
		}
		if (GUILayout.Button(newGame))
			Application.LoadLevel(1);
		if (GUILayout.Button(options))
			path = 1;
		if (GUILayout.Button(this.MainMenu))
			Application.LoadLevel(0);
		if (GUILayout.Button(quit))
			Application.Quit();
	}

	private void windowOptions(int id) {
		if (vibration == true)
		{
			if (GUILayout.Button(vibraOn, GUILayout.Height(100)))
				vibration = (vibration) ? false : true;
		}
		else
		{
			if (GUILayout.Button(vibraOff, GUILayout.Height(100)))
				vibration = (vibration) ? false : true;
		}
		if (audio.mute)
		{
			if (GUILayout.Button(muted))
				audio.mute = (audio.mute) ? false : true;
		}
		else
		{
			if (GUILayout.Button(mute))
				audio.mute = (audio.mute) ? false : true;
		}
		if (GUILayout.Button("Back", GUILayout.Height(50)))
			path = 0;
	}

	private void windowWait() {
		GUIStyle style = new GUIStyle();
		style.fontSize = 15;
		style.normal.textColor = Color.white;
		style.hover.textColor = Color.white;

		GUI.Label(waitRect, "Get Ready !", style);
		int yet = 3 - (int) (Time.realtimeSinceStartup - now);
		if (yet < 1)
		{
			paused = false;
			path = 0;
		}
		GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 90, 50), yet + "", style);
	}
	#endregion
}