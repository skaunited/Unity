using UnityEngine;
using System.Collections;

public class ReverseIA : MonoBehaviour {
	
	protected GameObject target = null;

	private float horizontal;
	private float vertical;

	void Start() {
		horizontal = 0.0f;
		vertical = 0.0f;
	}

	void Update () {
		if (GameObject.Find("Food"))
			target = GameObject.Find("Food").gameObject;
		else
			target = null;

		if (target != null)
		{
			Vector3 delta = target.transform.position - transform.position;

			horizontal = delta.x;
			vertical = delta.z;
		}
	}

	public float GetHorizontal() {return horizontal;}
	public float GetVertical() {return vertical;}
}