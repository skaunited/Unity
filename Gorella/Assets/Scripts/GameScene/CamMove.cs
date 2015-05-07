using UnityEngine;
using System.Collections;

public class CamMove : MonoBehaviour {

	public float speed = 0f;

	private bool go = false;
	private PauseMenu pause;

	void Start() {
		pause = transform.GetComponent<PauseMenu>();
		Time.timeScale = 0.7F;
	}

	void Update () {
		bool paused = pause.paused;

		if (paused)
			Time.timeScale = 0;
		else
			Time.timeScale = 0.7f;

		if (go && !paused)
		{
			transform.Translate(Vector3.up * Time.deltaTime * speed, Space.World);
			if (speed < 5)
				speed += 0.01f;
			else if (speed < 12)
				speed += 0.00001f;
		}
		else if (!paused)
		{
			Player pl = transform.GetComponent("Player") as Player;
			if (pl.start == true)
				go = true;
		}
	}
}