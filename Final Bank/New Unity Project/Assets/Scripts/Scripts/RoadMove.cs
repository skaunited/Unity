using UnityEngine;
using System.Collections;

public class RoadMove : MonoBehaviour
{
	public float destroyAfter = 10f;

	public static RoadMove current;
	
	private float timer;
	public float speed;
	public bool launched = false;
	
	void Start ()
	{
		current = this;
	}

	public void Launch(float _speed)
	{
		speed = _speed;
		launched = true;
	}

	void Update ()
	{
		if (launched)
		{
			transform.Translate(0, 0, -speed);
			Destroy(gameObject, 7f);
		}
	}
}
