using UnityEngine;
using System.Collections;

public class DestroyTimer : MonoBehaviour {

	public float destroyAfter = 2f;

	private float timer;

	void Update () {
		timer += Time.deltaTime;

		if (gameObject.name == "Mark")
		{
			if (destroyAfter <= timer)
				Destroy(gameObject);
		}
		else
		{
			if (destroyAfter <= timer)
				Destroy(gameObject);
		}
	}
}
