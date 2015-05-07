using UnityEngine;
using Facebook.MiniJSON;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class MainMenu : MonoBehaviour {

	public Texture NewGame;
	public Texture HighScores;
	public Texture Credits;
	public Texture Quit;
	public Texture FaceOut;

	public FacebookManagerScript fb;

	private int w = Screen.width;
	private int h = Screen.height;
	private bool check = false;

	void Update() {
		//PlayerPrefs.DeleteAll();
		GameObject net = GameObject.Find("internet");
		if (net != null)
		{
			InternetChecker connection = net.GetComponent<InternetChecker>();
			if (connection.internet && !check)
			{
				h -= (AdSize.Leaderboard.Height + 25);
				check = true;
			}
			if (!connection.internet)
			{
				h = Screen.height;
				check = false;
			}
		}
	}

	void OnGUI() {
		if (FB.IsLoggedIn)
		{
			GameObject test = GameObject.Find("Facebook");
			fb = test.GetComponent<FacebookManagerScript>();

			GUIStyle style = new GUIStyle();
			style.normal.textColor = Color.white;
			style.hover.textColor = Color.white;
			style.fontSize = 20;

			if (Screen.dpi > 400)
				style.fontSize = (int)(Screen.dpi / 10);

			GUI.Label(new Rect(w / 3, 15, (w / 2f), (h / 12)), "Welcome " + fb.Username, style);
			GUI.DrawTexture(new Rect(w - (w / 2.5f), (h / 15), (w / 3.4f), (h / 6)), fb.UserPicture);

			if (GUI.Button(new Rect(w - (w / 2.3f), ((4 * h) / 17), (w / 2.7f), (h / 12)), FaceOut))
			{
				fb.Logout();
				GameObject empty = GameObject.Find("Friends");
				if (empty != null)
					Destroy(empty);
			}
		}

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
