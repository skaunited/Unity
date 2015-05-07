using UnityEngine;
using System.Collections;

public class DestroyGame : MonoBehaviour
{
	public float destroyAfter = 10f;
	
	private float timer;
	private float speed;
	
	void Start () {
		speed = Random.Range(0.7f, 0.73f);
	}
	void Update () {
		transform.Translate(0, 0, -speed);
		Destroy(gameObject, destroyAfter);
	}
}