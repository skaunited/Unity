using UnityEngine;
using System.Collections;
using Facebook.MiniJSON;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using Parse;
using System;

public class Death : MonoBehaviour {

	#region Initialisation
	public Texture MainMenu;

	private string score;
	private int h = Screen.height;
	private int w = Screen.width;
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
		
	void Start () {
		score = "Your score is " + PlayerPrefs.GetInt("Score");
		checkScores();
	}

	void OnGUI() {
		GUIStyle style = new GUIStyle();
		style.fontSize = 25;
		style.normal.textColor = Color.red;
		style.hover.textColor = Color.red;

		GUI.Label(new Rect(((w / 2) - (score.Length * 6)), 150, 100, 100), score, style);
		if (GUI.Button(new Rect(((w / 2) - (h / 6)), (h - (h / 12)), (w / 1.5f), (h / 12)), MainMenu))
			Application.LoadLevel(0);
	}
	#endregion

	#region Check
	public static bool Contains(int[] array, int n) {
		foreach (int x in array)
		{
			if (x == n)
				return true;
		}
		return false;
	}

	private void checkScores() {
	
		if ((!PlayerPrefs.HasKey("HighScore")) || (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore")))
		{
			PlayerPrefs.SetInt("current", PlayerPrefs.GetInt("HighScore"));

			PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
			int score = PlayerPrefs.GetInt("Score");
			if (FB.IsLoggedIn)
			{
				var query = ParseObject.GetQuery("player").WhereEqualTo("UserID", FB.UserId);
				query.FirstAsync().ContinueWith(t =>
				{
					var player = t.Result;
					player.SaveAsync().ContinueWith(tt =>
					{
						player["Score"] = score;
						player.SaveAsync();
					});
				});
			}
		}
		/*	int[] hScores = new int[10];
		int sc = PlayerPrefs.GetInt("Score");
		int i;


		for (i = 0; i < 10; i++)
		{
			if(!PlayerPrefs.HasKey(i+"HScore"))
			{
				if (!Contains(hScores, sc))
				{
					hScores[i] = sc;
					i++;
				}
				break;
			}
			else
				hScores[i] = PlayerPrefs.GetInt(i+"HScore");
		}

		Array.Sort(hScores);
		Array.Reverse(hScores);
		if (i == 10)
		{
			if (sc > hScores[9])
			{
				hScores[9] = sc;
				Array.Sort(hScores);
				Array.Reverse(hScores);
			}
		}

		for (int j = 0; j < i; j++)
			PlayerPrefs.SetInt(j+"HScore", hScores[j]);*/
	}
	#endregion
}