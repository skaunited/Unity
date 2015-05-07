using UnityEngine;
using System.Collections;

public class SuperMarioIA : MonoBehaviour {

	private GameObject target1;
	private GameObject target2;

	protected GameObject target = null;

	private float horizontal;
	private float vertical;

	private bool set = false;
	private int box = 0;

	void Start() {
		horizontal = 0.0f;
		vertical = 0.0f;
	}

	void Update () {

		if (set)
			target = ChooseTarget();

		if (target != null)
		{
			Vector3 distance = transform.position - target.transform.position;

			horizontal = distance.x;
			vertical = distance.z;
		}
	}

	public void SetTarget(GameObject target1, GameObject target2) {
		this.target1 = target1;
		this.target2 = target2;
		set = true;
	}

	public float GetHorizontal() {return horizontal;}
	public float GetVertical() {return vertical;}

	GameObject ChooseTarget() {
		Targetting sc1 = target1.GetComponent<Targetting>();
		Targetting sc2 = target2.GetComponent<Targetting>();

		switch (box)
		{
			case 0 :
				box = 1;
				return (target1);
//				break;
			case 1 :
				if (sc1.select)
				{
					box = 2;
					return (target2);
				}
				return (target1);
//				break;
			case 2 :
				if (sc2.select)
				{
					box = 1;
					return (target1);
				}
				return (target2);	
//				break;
		}
		return (target1);
	}
}