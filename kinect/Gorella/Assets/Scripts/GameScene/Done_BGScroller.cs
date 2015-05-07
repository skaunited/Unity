using UnityEngine;
using System.Collections;

public class Done_BGScroller : MonoBehaviour
{
	private bool go = false;

	IEnumerator WaitForLunch() {
		yield return new WaitForSeconds(10f);
		go = true;
	}

	void Start() {
		StartCoroutine("WaitForLunch");
	}

	void FixedUpdate ()
	{
		if (go && transform.localPosition.y > -12.8f)
			transform.Translate(Vector3.down * Time.deltaTime * 0.01f, Space.World);
	}
}