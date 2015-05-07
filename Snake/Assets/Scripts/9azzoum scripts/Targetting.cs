using UnityEngine;
using System.Collections;

public class Targetting : MonoBehaviour {

	public bool select = false;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "goober")
			select = true;
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.name == "goober")
			select = false;
	}
}