using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartWall : MonoBehaviour {

	public GUIText text;

	private Timer timer;
	private int lap = 0;
	private List<float> lapTime = new List<float>();

	void Start() {
		timer = text.GetComponent<Timer>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "auto")
		{
			if (lap > 0)
				lapTime.Add(timer.lapTime);
			lap++;
			timer.lapTime = 0.0f;
			timer.oldTime = Time.realtimeSinceStartup;
		}
	}

	void OnGUI() {
		GUIStyle style = new GUIStyle ();
		style.fontSize = 20;
		style.normal.textColor = Color.green;
		GUI.Label(new Rect(Screen.width - 30, 5, 25, 25), lap.ToString(), style);

		for (int i=0; i<(lap-1); i++)
		{
			style.normal.textColor = Color.blue;
			style.fontSize = 15;
			GUI.Label(new Rect(Screen.width - 120, 25 * (i + 1), 100, 25), (i+1) + " :\t\t" + lapTime[i] + " s", style);
		}
	}
}