using UnityEngine;
using System.Collections;

public class SnakeBehaviour : MonoBehaviour {

	public int Score = 0;

	void OnGUI() {
		if (transform.name == "Snake")
			GUI.Label(new Rect(10, 10, 35, 20), Score.ToString());
		if (transform.name == "ReverseSnake")
			GUI.Label(new Rect(Screen.width - 40, 10, 35, 20), Score.ToString());
	}
}