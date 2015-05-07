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

	private int h = Screen.height;
	private int w = Screen.width;
	private static bool isMute = false;

    private bool showMenu = false;
	private float now;
	private float begin;
	private float elapsed = 0f;

    private Vector3 pointManPos = Vector3.zero;
    private GameObject PointMan = null;
	#endregion

	#region Initialisation
    void Awake() {
        paused = true;
        showMenu = false;
        PointMan = GameObject.Find("PointManCtrl/PointMan");
        pointManPos = PointMan.transform.position;
    }

    private void Start() {
		rect  = new Rect(10, 10, (h / 12), (h / 12));
		waitRect = new Rect((w / 2) - (w / 11), (h / 2) - (w / 11), (h / 6), (w / 4.5f));
        windowRect = new Rect((w / 2) - (h / 6), (h / 2) - (h / 6), (h / 3), (h / 2));
		begin = Time.realtimeSinceStartup;
		elapsed = 0.0f;
		//windowVibra = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 290);
	}

    void Update() {
        if (paused && !showMenu)
        {
            if (pointManPos != PointMan.transform.position)
            {
                paused = false;
                showMenu = true;
            }
        }
    }

	private void OnGUI () {
		if (!paused && showMenu)
		{
			if (GUI.Button(rect, PauseTexture))
			{
				paused = true;
				elapsed += Time.realtimeSinceStartup - begin;
			}
		}
		if (paused && showMenu)
		{
			GUIStyle style = new GUIStyle(GUI.skin.window);
			style.fontSize = (int)(Screen.dpi / 20);

			if (path == 0)
				windowRect = GUI.Window(0, windowRect, windowFunc, "Pause Menu", style);
			else if (path == 1)
				windowRect = GUI.Window(1, windowRect, windowOptions, "Options", style);
			else
				windowWait();

			GUIStyle syle = new GUIStyle();
			syle.normal.textColor = Color.white;
			if (Screen.dpi != 0)
				syle.fontSize = (int)(Screen.dpi / 8);
			else
				syle.fontSize = 15;

			string elapsedTime = "";
			if (elapsed > 60f)
			{
				int min = (int)(elapsed / 60);
				float sec = (int)(elapsed - (min * 60));
				float yet = min + (sec / 100);
				elapsedTime = "Elapsed Time : " + yet + "m";
			}
			else
				elapsedTime = "Elapsed Time : " + elapsed + "s";

			if (path != 2)
				GUI.Label(new Rect((w / 2) - (h / 7), (h / 6), (h / 4), (h / 12)), elapsedTime, syle);
		}
	}
	#endregion

	#region Menus
	private void windowFunc(int id) {
		if (GUILayout.Button(resume, GUILayout.Height(h / 12)) || Input.GetKeyDown(KeyCode.Escape))
		{
			now = Time.realtimeSinceStartup;
			path = 2;
		}
		GUILayout.Space(Screen.dpi / 20);
		if (GUILayout.Button(newGame, GUILayout.Height(h / 12)))
			Application.LoadLevel(1);
		GUILayout.Space(Screen.dpi / 20);
		if (GUILayout.Button(options, GUILayout.Height(h / 12)))
			path = 1;
		GUILayout.Space(Screen.dpi / 20);
		if (GUILayout.Button(this.MainMenu, GUILayout.Height(h / 12)))
			Application.LoadLevel(0);
		GUILayout.Space(Screen.dpi / 20);
		if (GUILayout.Button(quit, GUILayout.Height(h / 12)))
			Application.Quit();
	}

	private void windowOptions(int id) {
		if (vibration == true)
		{
			if (GUILayout.Button(vibraOn, GUILayout.Height(h / 6)))
				vibration = (vibration) ? false : true;
		}
		else
		{
			if (GUILayout.Button(vibraOff, GUILayout.Height(h / 6)))
				vibration = (vibration) ? false : true;
		}
		GUILayout.Space(Screen.dpi / 20);
		if (isMute)
		{
			if (GUILayout.Button(muted, GUILayout.Height(h / 6)))
			{
				isMute = (isMute) ? false : true;
				audio.mute = isMute;
			}
		}
		else
		{
			if (GUILayout.Button(mute, GUILayout.Height(h / 6)))
			{
				isMute = (isMute) ? false : true;
				audio.mute = isMute;
			}
		}

		GUILayout.Space(Screen.dpi / 20);
		GUIStyle style = new GUIStyle(GUI.skin.button);
		style.fontSize = (int)(Screen.dpi / 10);
		style.normal.textColor = Color.white;
		style.hover.textColor = Color.cyan;
		style.active.textColor = Color.cyan;

		if (GUILayout.Button("Back", style, GUILayout.Height(h / 12)) || Input.GetKeyDown(KeyCode.Escape))
			path = 0;
	}

	private void windowWait() {
		GUIStyle style = new GUIStyle();
		style.fontSize = 15;
		style.normal.textColor = Color.white;
		style.hover.textColor = Color.white;

		if (Screen.dpi != 0)
			style.fontSize = (int)(Screen.dpi / 8);

		GUI.Label(waitRect, "Get Ready !", style);
		int yet = 3 - (int) (Time.realtimeSinceStartup - now);
		if (yet < 1)
		{
			begin = Time.realtimeSinceStartup;
			paused = false;
			path = 0;
		}
		GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 90, 50), yet + "", style);
	}
	#endregion
}