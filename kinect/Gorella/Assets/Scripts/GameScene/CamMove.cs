using UnityEngine;
using System.Collections;

public class CamMove : MonoBehaviour {

	public float speed = 0f;

	private bool go = false;
	private PauseMenu pause;

	void Start() {
		pause = transform.GetComponent<PauseMenu>();
		Time.timeScale = 0.6F;
	}

	void Update () {
		bool paused = pause.paused;

		if (paused)
			Time.timeScale = 0;
		else
			Time.timeScale = 0.6f;

		if (go && !paused)
		{
			transform.Translate(Vector3.up * Time.deltaTime * speed, Space.World);
			if (speed < 2)
				speed += 0.2f;
			else if (speed < 5)
				speed += 0.001f;
			else if (speed < 15)
				speed += 0.000001f;
		}
		else if (!paused)
		{
			Player pl = transform.GetComponent("Player") as Player;
			if (pl.start == true)
				go = true;
		}
	}
}