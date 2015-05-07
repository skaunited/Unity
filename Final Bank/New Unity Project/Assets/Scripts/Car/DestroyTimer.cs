using UnityEngine;
using System.Collections;

public class DestroyTimer : MonoBehaviour {

	public float destroyAfter = 20f;

	private float timer;
	private float speed;

	void Start () {
		speed = Random.Range(0.9f, 1.2f);
	}
	void Update () {
		transform.Translate(0, 0, speed);
		Destroy(gameObject, destroyAfter);
	}
}
