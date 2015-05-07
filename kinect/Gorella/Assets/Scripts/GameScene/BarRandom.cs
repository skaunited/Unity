using UnityEngine;
using System.Collections;

public class BarRandom : MonoBehaviour {

	#region Attributes
	public GameObject wood;
	public GameObject safe;
	public GameObject WoodOnFire;

	private float distance = 2.0f;
	private static int j = 0;
	private int i;
	private int lastType = 0;
	private int y = -5;
	private int n = 0;
	private int maxRand = 6;
	private GameObject vide;
	private bool isSafe = true;
	#endregion

	#region Wood
	void Start () {
		vide = new GameObject("Bars");
		i = Random.Range(1, 2);
	}
 
	void Update () {
		if (n >= 100 && maxRand == 6)
			maxRand--;
		else if (n >= 250 && maxRand == 5)
			maxRand--;

		if (vide.transform.childCount < 10)
		{
			if (j >= distance)
			{
				int type = Random.Range(1, maxRand);
				GameObject cube;

				if (lastType == 3 || type != 3 || n < 15)
				{
					if (lastType != 3 || n < 15 || isSafe == true)
						cube = Instantiate(wood) as GameObject;
					else
					{
						cube = Instantiate(safe) as GameObject;//GameObject.CreatePrimitive(PrimitiveType.Cube);
						isSafe = true;
					}
				}
				else
				{
					cube = Instantiate(WoodOnFire) as GameObject;
					isSafe = false;
				}

				lastType = type;
				cube.AddComponent("BarDel");
				cube.name = "cube " + n++;
				//cube.transform.localScale = new Vector3(3.0f, 0.5f, 3.0f);
				float x = (i == 1)? 1.6f : -1.6f;
				cube.transform.position = new Vector3(x, y, -1f);
				distance = Random.Range(1.7f, 2.5f);
				cube.transform.parent = vide.transform;
			
				if (i == 2)
					cube.transform.rotation = Quaternion.Euler(0, 180f, 0);

				i = (i == 1)? 2 : 1;
				j = 0;
			}
			y++;
			j++;
		}
	}
	#endregion

}