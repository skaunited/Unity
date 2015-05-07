using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public int min = 2;
	public int sec = 30;

	private float begin;
	private int old = 0;

	private SnakeBehaviour snake;
	private SnakeBehaviour reverse;

	void Start() {
		begin = Time.realtimeSinceStartup;
		Time.timeScale = 1;
		snake = GameObject.Find("Snake").gameObject.GetComponent<SnakeBehaviour> ();
		reverse = GameObject.Find("ReverseSnake").gameObject.GetComponent<SnakeBehaviour> ();
	}
	
	void OnGUI() {
		int elapsed = old;

		if (min == 0 && sec == 0)
		{
			Time.timeScale = 0;
			string text = "";
			GUIStyle style = new GUIStyle();
			style.fontSize = 15;
			style.normal.textColor = Color.magenta;
			if (snake.Score == reverse.Score)
			{
				int draw = 3 - (Mathf.FloorToInt(Time.realtimeSinceStartup) - elapsed);
				if (draw < 1)
				{
					sec = 30;
					Time.timeScale = 1;
				}
				text = "Draw " + draw;
			}
			else if (snake.Score > reverse.Score)
				text = "You Won !";
			else
				text = "The reverse won !";
			GUI.Label(new Rect((Screen.width / 2) - 30, (Screen.height / 2) - 10, 40, 20), text, style);
			if (GUI.Button(new Rect((Screen.width / 2) - 25, (Screen.height / 2) + 30, 100, 30), "New Game"))
				Application.LoadLevel(0);
		}
		else
			elapsed = Mathf.FloorToInt(Time.realtimeSinceStartup - begin);

		if (elapsed != old)
		{
			old = elapsed;
			sec--;
		}

		if (sec < 0)
		{
			sec = 59;
			min--;
		}
		string counter = "";
		if (sec < 10)
			counter = min + ":0" + sec;
		else
			counter = min + ":" + sec;
		GUI.Label(new Rect((Screen.width / 2) - 15, 10, 30, 20), counter);
	}
}
