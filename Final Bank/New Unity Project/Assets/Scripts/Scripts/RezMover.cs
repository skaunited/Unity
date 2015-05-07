using UnityEngine;
using System.Collections;

public class RezMover : MonoBehaviour
{

	public Transform origin;
	public Transform destination;
	public Transform destination2;

	public float minT = 7f;
	public float maxT = 20f;
	public float seconds;

	private Transform _myTransform;

	void Start ()
	{
		_myTransform = this.transform;
		StartCoroutine("Move");
	}

	IEnumerator Move()
	{
		while (true)
		{
			seconds = Random.Range (minT, maxT);
			yield return new WaitForSeconds(seconds);
			float rand = Random.Range(1,3);
			if (_myTransform.position == origin.position)
			{
				if (rand == 1)
					_myTransform.position = destination.position;
				else
					_myTransform.position = destination2.position;
			}
			else if (_myTransform.position == destination.position)
			{
				if (rand == 1)
					_myTransform.position = origin.position;
				else
					_myTransform.position = destination2.position;
			}
			else if (_myTransform.position == destination2.position)
			{
				if (rand == 1)
					_myTransform.position = destination.position;
				else
					_myTransform.position = origin.position;
			}
		}
	}
}
