using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float SpeedBullet;

	void Start () {
		rigidbody.AddForce(transform.forward * SpeedBullet);
	}

	void OnCollisionEnter(Collision other) {
		Destroy(gameObject);
	}
}
