using UnityEngine;
using System.Collections;

public class BarDel : MonoBehaviour {

	private GameObject child;

	void Start () {
		child = Camera.main.transform.Find("laser").gameObject;
	}

	void Update () {
		if (transform.position.y < child.transform.position.y)
			Destroy(transform.gameObject);
	}
}
