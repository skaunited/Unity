using UnityEngine;
using System.Collections;

public class Component : MonoBehaviour {

	public GameObject target;
	public Texture Torche;

	private bool write = false;
	private bool light = false;
	private Rect TexturePos;
	private Color guiColor;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject == target)
			write = true;
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject == target)
			write = false;
	}

	void Start () {
		TexturePos = new Rect(70, 10, 50, 50);
		guiColor = Color.white;
		guiColor.a = 0.2f;
	}

	void OnGUI() {
		if (Player.torche)
		{
			if (!light)
				GUI.color = guiColor;
			GUI.DrawTexture(TexturePos, Torche, ScaleMode.StretchToFill, true, 0);
			GUI.color = Color.white;
		}

		if (write)
		{
			GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2, 15, 20), "X");
			
			if (Input.GetKeyUp(KeyCode.X))
			{
				Player.torche = true;
				transform.parent = target.transform;

				BoxCollider box = transform.GetComponent<BoxCollider>();
				box.enabled = false;
				write = false;
			}
		}
	}

	void Update () {
		if (Player.torche)
		{
			if (Input.GetKeyUp(KeyCode.F))
			{
				if (this.light)
				{
					this.light = false;
					Light light = transform.GetComponent<Light>();
					light.enabled = false;
				}
				else
				{
					this.light = true;
					Light light = transform.GetComponent<Light>();
					light.enabled = true;
				}
			}
		}
	}
}