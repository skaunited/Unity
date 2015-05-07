using UnityEngine;
using System.Collections;

public class FoodGenerator : MonoBehaviour {

	public Material foodMat;

	public float length = 49.5F;
	public float width = 49.5F;
	
	void Update () {
		if (!GameObject.Find("Food"))
		{
			GameObject food = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			food.name = "Food";
			food.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			food.transform.position = new Vector3(UnityEngine.Random.Range(0.5f, width), 0.25f, UnityEngine.Random.Range(0.5f, length));
			food.renderer.material = foodMat;
			food.AddComponent<FoodBehaviour>();

			SphereCollider sphere = food.GetComponent<SphereCollider>();
			sphere.isTrigger = true;
		}
	}
}