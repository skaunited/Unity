using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

	public GameObject target;

	IEnumerator wait(float time) {
		yield return new WaitForSeconds(time);
		Application.LoadLevel(1);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject == target)
			StartCoroutine(wait(0.5f));
	}
}
