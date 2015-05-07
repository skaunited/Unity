using UnityEngine;
using Facebook.MiniJSON;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using Parse;

public class ShowScores : MonoBehaviour {

	#region Attributes
	public GUISkin skin;
	public Texture FaceIn;
	public FacebookManagerScript fb;

	private bool create = true;
	private int h = Screen.height;
	private int w = Screen.width;
	
	private bool check = false;
	#endregion
	
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

	#region GUI
	void OnGUI() {

		GUI.color = Color.white;
		GUIStyle button = new GUIStyle(GUI.skin.button);
		button.fontSize = (int)(Screen.dpi / 10);
		if (GUI.Button(new Rect(0, (h - (int)(h / 12)), w, (int)(h / 12)), "Back", button))
		{
			Friends.show = false;
			World.show = false;
			Application.LoadLevel(0);
		}

		if (!FB.IsLoggedIn)
		{
			GUIStyle style = new GUIStyle();
			style.fontSize = (int)(Screen.dpi / 5);
			style.normal.textColor = Color.white;
			style.hover.textColor = Color.white;

			if (PlayerPrefs.HasKey("HighScore"))
				GUI.Label(new Rect(((Screen.width / 2) - 50), 50, (int)(w / 3.4f), (int)(h / 24)), PlayerPrefs.GetInt("HighScore").ToString(), style);
			else
				GUI.Label(new Rect(((Screen.width / 2) - 50), 50, (int)(w / 3.4f), (int)(h / 24)), "0", style);

			style.fontSize = (int)(Screen.dpi / 10);

			if (FB.loading)
				GUI.Label(new Rect((Screen.width / 2) - 50, (Screen.height - 200), Screen.width, 75), "LOADING ...", style);
			else
			{
				if (FB.internet)
				{
					if (GUI.Button(new Rect(0, (h - (int)(h / 5)), Screen.width, (int)(h / 10)), FaceIn))
						fb.Login();
				}
				else
				{
					GUI.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
					GUI.Box(new Rect(0, (Screen.height - 200), Screen.width, 75), FaceIn);
					GUI.color = Color.white;
				}
			}
		}
		else
		{
			GUIStyle style = new GUIStyle(GUI.skin.button);
			style.fontSize = (int)(Screen.dpi / 10);

			if (create)
			{
				GameObject empty = GameObject.Find("Friends");
				if (empty != null)
					Destroy(empty);
				empty = new GameObject("Friends");
				empty.AddComponent<FriendsList>();
				create = false;
			}
			if (GUI.Button(new Rect((w / 2) - (int)(w / 3f), 20, (int)(w / 3.4f), (int)(h / 12f)), "Friends", style))
			{
				World.show = false;
				Friends.show = true;
			}
			if (GUI.Button(new Rect((w / 2), 20, (int)(w / 3.4f), (int)(h / 12f)), "World", style))
			{
				Friends.show = false;
				World.show = true;
			}
		}
	}
	#endregion

}