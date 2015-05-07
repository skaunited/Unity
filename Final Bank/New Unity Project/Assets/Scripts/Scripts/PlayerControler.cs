using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour
{
	public Camera cam;

	public Transform Left;
	public Transform Center;
	public Transform Right;

	private Vector3 _pos;
	private float _width;
	private Transform _myTransform;
	private bool _move = false;

	private Vector3 Dest;

	public float speed;

	void Start ()
	{
		_myTransform = this.transform;
	}
	void Update ()
	{
		if (_move)
		{
			float step = speed * Time.deltaTime;
			_myTransform.position = Vector3.MoveTowards(_myTransform.position, Dest, step);
			_myTransform.LookAt(new Vector3(Dest.x, Dest.y, Dest.z + 10f));
			if (_myTransform.position == Dest)
			{
				_myTransform.LookAt(new Vector3(_myTransform.position.x, _myTransform.position.y, _myTransform.position.z + 10f));
				_move = false;
			}
			return;
		}
		if (Input.GetMouseButtonUp(0))
		{
			_pos = Input.mousePosition;
			_width = Screen.width / 2;

			if (_pos.x < _width) // gauche
			{
				_move = true;
				if (_myTransform.position == Center.position)
					Dest = Left.position;
				else if (_myTransform.position == Right.position)
					Dest = Center.position;
				else if (_myTransform.position == Left.position)
					Dest = Center.position;
				else
					_move = false;
			}
			else if (_pos.x >= _width)// droite
			{
				_move = true;
				if (_myTransform.position == Center.position)
					Dest = Right.position;
				else if (_myTransform.position == Left.position)
					Dest = Center.position;
				else if (_myTransform.position == Right.position)
					Dest = Center.position;
				else
					_move = false;
			}
		}
	}
}
