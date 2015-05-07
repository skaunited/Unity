using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public float oldTime;
	public float lapTime;

	private float runTime;

	void Start() {
		runTime = 0.0f;
		lapTime = 0.0f;
		oldTime = 0.0f;
	}

	void Update() {
		if (oldTime != 0.0f)
		{
			lapTime += Time.realtimeSinceStartup - oldTime;
			runTime += Time.realtimeSinceStartup - oldTime;
			oldTime = Time.realtimeSinceStartup;
		}
		GUIText text = transform.GetComponent<GUIText>();
		text.text = runTime + " s";
	}
}