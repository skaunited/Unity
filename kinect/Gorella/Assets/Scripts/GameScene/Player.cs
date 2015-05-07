using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	#region Attributes
	public GameObject player;
	public bool start = false;

	public Sprite idle;
	public Sprite jump;
	public Sprite jumpUp;
	#endregion

	#region PlayerChildren
	void Update () {
		GameObject vide = GameObject.Find("Bars");

		if (vide != null)
		{
			if (!start && vide.transform.childCount == 10)
			{
				start = true;
				GameObject player = createPlayer(vide);
				giveColliders(player);
			}
		}
	}

	static public ArrayList listChildren(GameObject obj) {
		ArrayList gs = new ArrayList();
		Transform ts = obj.transform;

		if (ts == null)
			return gs;
		foreach (Transform t in ts) {
			if (t != null && t.gameObject != null)
				gs.Add(t.gameObject);
		}
		return gs;
	}

	static public int IndexOfName(ArrayList array, string name) {
		int i = 0;
		foreach (GameObject tab in array)
		{
			if (tab.name == name)
				break;
			i++;
		}
		if (i >= array.Count)
			i = -1;
		return (i);
	}
	#endregion

	#region playerInfos

	private void giveColliders(GameObject jill) {
		GameObject sphereUp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphereUp.transform.parent = jill.transform;
		sphereUp.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		sphereUp.transform.localPosition = new Vector3(0.1f, 0.5f, 0.055f);
		sphereUp.renderer.material.color = Color.green;
		sphereUp.name = "Sphere Up";

		GameObject sphereDown = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphereDown.transform.parent = jill.transform;
		sphereDown.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		sphereDown.transform.localPosition = new Vector3(0.01788807f, -0.6f, 0.0520371f);
		sphereDown.renderer.material.color = Color.green;
		sphereDown.name = "Sphere Down";

		GameObject sphereMiddle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphereMiddle.transform.parent = jill.transform;
		sphereMiddle.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		sphereMiddle.transform.localPosition = new Vector3(0.01788807f, -0.2f, 0.0520371f);
		sphereMiddle.renderer.material.color = Color.green;
		sphereMiddle.name = "Sphere Middle";

	}

	private GameObject createPlayer(GameObject bars) {
		GameObject jill = null;
		GameObject cube = listChildren(bars)[0] as GameObject;
		
		jill = Instantiate(player) as GameObject;
		float x, y, z = 1.55f;
		
		if (cube.transform.position.x == 1.6f)
		{
			x = 0.6f;
			y = 180f;
		}
		else
		{
			x = 0f;
			y = 0f;
		}
		jill.transform.position = new Vector2(cube.transform.position.x + x, cube.transform.position.y + z);
		jill.transform.rotation = Quaternion.Euler(0, y, 0);
		
		jill.AddComponent("PlayerControl");
		jill.name = "player";
		
		return (jill);
	}
	#endregion
}