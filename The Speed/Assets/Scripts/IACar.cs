using UnityEngine;
using System.Collections;

public class IACar : MonoBehaviour {

	void Update () {
		RaycastHit hit;
		//Vector3 fwd = transform.TransformDirection(Vector3.forward);
		Ray ray = new Ray(transform.position, Vector3.forward);
		if (Physics.Raycast(ray, out hit, 10))
			print(hit.distance);
	}
}
