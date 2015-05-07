using UnityEngine;
using System.Collections;

public class DecoPopup : MonoBehaviour
{
	public Transform dest;
	public Transform orig;
	public float speed;

	private bool _switching = true;

	private Transform _pos;

	public int carMin;
	public int carMax;
	
	public float rot = 0f;
	
	public float minT = 7f;
	public float maxT = 20f;
	public float seconds;
	public string carss;
	
	/*
	public float x;
	public float y;
	public float z;
	*/
	
	private int rand;

	void Start()
	{
		_pos = this.transform;
		StartCoroutine("WaitForLunch");
	}

	void Update()
	{
		if (_pos.position == dest.position)
			_switching = true;
		else if (_pos.position == orig.position)
			_switching = false;
		if (_switching)
			_pos.position = Vector3.MoveTowards(_pos.position, orig.position, speed * Time.deltaTime);
		else if (!_switching)
			_pos.position = Vector3.MoveTowards(_pos.position, dest.position, speed * Time.deltaTime);
	}

	IEnumerator WaitForLunch()
	{
		while (true)
		{
			seconds = Random.Range (minT, maxT);
			yield return new WaitForSeconds(seconds);
			rand = Random.Range(carMin, carMax);
			carss = "Deco/Deco" + rand;
			GameObject car = Instantiate(Resources.Load<GameObject>(carss)) as GameObject;
			car.AddComponent("DestroyPub");
			car.transform.position = transform.position;
			//car.transform.localScale = new Vector3(x, y, z);
			car.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}
}
