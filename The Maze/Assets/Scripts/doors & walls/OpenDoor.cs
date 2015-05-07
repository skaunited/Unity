using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class OpenDoor : MonoBehaviour {

	public GameObject target;

	private bool write = false;
	private bool open = false;
	private int door = 0;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject == target)
			write = true;
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == target)
			write = false;
	}

	void OnGUI() {
		if (write)
		{
			GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2, 15, 20), "E");

			if (Input.GetKeyUp(KeyCode.E))
			{
				if (Player.key)
				{
					audio.Play();
					open = true;
				}
				else
					GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2, 75, 20), "Door locked");
			}
		}
	}

	void Update() {
		if (open && this.door++ < 200)
		{
			GameObject door = transform.parent.Find("door").gameObject;
			door.transform.Rotate(Vector3.up * 30f * Time.deltaTime, Space.World);	
		}
	}
}