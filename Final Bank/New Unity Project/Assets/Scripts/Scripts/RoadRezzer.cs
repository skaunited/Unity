using UnityEngine;
using System.Collections;

public class RoadRezzer : MonoBehaviour
{
	public GameObject Road;
	public float Inter;
	public float speed;

	void Start ()
	{
		StartCoroutine("Rez");
	}

	IEnumerator Rez()
	{
		while (true)
		{
			GameObject car = Instantiate(Road) as GameObject;
			car.AddComponent("RoadMove");
			car.transform.position = transform.position;
			car.GetComponent<RoadMove>().Launch(speed);
			yield return new WaitForSeconds(Inter);
		}
	}
}
