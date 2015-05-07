using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	void OnGUI() {
		if (GUI.Button(new Rect(0, (Screen.height - 50), Screen.width, 50), "Back"))
			Application.LoadLevel(0);
	}
}