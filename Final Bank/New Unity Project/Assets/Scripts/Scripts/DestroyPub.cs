using UnityEngine;
using System.Collections;

public class DestroyPub : MonoBehaviour
{
	public float destroyAfter = 6f;
	
	private float timer;
	private float speed = 1.1f;

	void Update () {
		transform.Translate(0, 0, -speed);
		Destroy(gameObject, destroyAfter);
	}
}
