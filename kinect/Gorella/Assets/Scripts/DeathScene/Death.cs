using UnityEngine;
using System.Collections;
using System;

public class Death : MonoBehaviour {

	#region Initialisation
	public Texture MainMenu;
	public Texture NewGame;

	private string score;
	private int h = Screen.height;
	private int w = Screen.width;
	
	#endregion

	#region GUI
	void Start () {
		score = "Your score is " + PlayerPrefs.GetInt("Score");
		checkScores();
	}

	void OnGUI() {
		GUIStyle style = new GUIStyle();
		style.fontSize = 25;
		style.normal.textColor = Color.white;
		style.hover.textColor = Color.white;

		if (Screen.dpi != 0)
			style.fontSize = (int)(Screen.dpi / 7);

		GUI.Label(new Rect(((w / 2) - (w / 4)), (h / 4), (h / 6), (h / 6)), score, style);

		if (GUI.Button(new Rect(0, (h - (h / 12)), (w / 2), (h / 12)), NewGame))
			Application.LoadLevel(1);
		if (GUI.Button(new Rect((w / 2), (h - (h / 12)), (w / 2), (h / 12)), MainMenu) || Input.GetKeyDown(KeyCode.Escape))
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
	
		/*if ((!PlayerPrefs.HasKey("HighScore")) || (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore")))
		{
			PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
			int score = PlayerPrefs.GetInt("Score");
		}*/

		int[] hScores = new int[10];
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
			PlayerPrefs.SetInt(j+"HScore", hScores[j]);
	}
	#endregion
}