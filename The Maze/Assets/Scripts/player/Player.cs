using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static int health = 100;
	public static bool key = false;
	public static bool torche = false;

	public Texture Key;

	private Rect TexturePos;
	private Color guiColor;

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.tag == "Bullet")
			health -= 20;
	}

	void Start () {
		TexturePos = new Rect(10, 10, 50, 50);
		guiColor = Color.white;
		guiColor.a = 0.2f;
		//Object.DontDestroyOnLoad(transform.gameObject);
	}

	void OnGUI () {
		if (!key)
			GUI.color = guiColor;
		GUI.DrawTexture(TexturePos, Key, ScaleMode.StretchToFill, true, 0);
		GUI.color = Color.white;
	}

	void Update () {
		if (health <= 0)
			Application.LoadLevel(2);
	}
}
