using UnityEngine;
using System.Collections;

public class ColideDetector : MonoBehaviour
{
	public GameObject boom;

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("PCars"))
		{
			Score.current.Ouch();
			boom = Resources.Load<GameObject>("Explosion/Boom");
			GameObject explosion = Instantiate(boom, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z - 1f), transform.rotation) as GameObject;
			Destroy (explosion, 0.3f);
			Destroy(this.transform.parent.gameObject);
		}
	}
}
