using UnityEngine;
using System.Collections;

public class CarPopUp : MonoBehaviour {

	public int carMin;
	public int carMax;

	public float rot = 0f;

	public float minT = 7f;
	public float maxT = 20f;
	public float seconds;
	public string carss;

	public float x;
	public float y;
	public float z;

	private int rand;

	IEnumerator WaitForLunch() {
		while (true)
		{
			seconds = Random.Range (minT, maxT);
			yield return new WaitForSeconds(seconds);
			rand = Random.Range(carMin, carMax);
			carss = "cars/Car" + rand;
			GameObject car = Instantiate(Resources.Load<GameObject>(carss)) as GameObject;
			car.AddComponent("DestroyTimer");
			car.transform.position = transform.position;
			car.transform.localScale = new Vector3(x, y, z);
			car.transform.rotation = Quaternion.Euler(0, rot, 0);
		}
	}

	void Start() {
		StartCoroutine("WaitForLunch");
	}
}
