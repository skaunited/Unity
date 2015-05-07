using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootSteps : MonoBehaviour {

	void Start() {
		audio.Stop();
	}

	void Update () {
		if (KeyDown())
			audio.Play();

		if (KeyUp())
			audio.Stop();
	}

	private bool KeyDown() {
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			return true;
		if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
			return true;
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
			return true;
		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
			return true;
		return false;
	}

	private bool KeyUp() {
		if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
			return true;
		if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
			return true;
		if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
			return true;
		if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
			return true;
		return false;
	}
}