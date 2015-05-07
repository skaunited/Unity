using UnityEngine;
using System.Collections;

public class BoxObj : MonoBehaviour {

	public GameObject target;
	public GameObject gift;
	public GameObject anim;

	private GameObject box;
	private bool write = false;
	private bool put = true;
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject == target)
			write = true;
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject == target)
			write = false;
	}

	void Start () {
		box = transform.Find("BlockPickUp").gameObject;
	}

	void OnGUI() {
		if (write)
		{
			string button = (put)? "E" : "X";
			GUI.TextField(new Rect(Screen.width / 2, Screen.height / 2, 15, 20), button);
			
			if (Input.GetKeyUp(KeyCode.E) && put)
			{
				GameObject clone = (GameObject)Instantiate(anim);
				clone.transform.position = transform.position;

				GameObject key = (GameObject)Instantiate(gift);
				key.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
				key.transform.parent = transform;

				Destroy(clone, 1f);
				Destroy(box);

				put = false;
			}
			else if (Input.GetKeyUp(KeyCode.X) && !put)
			{
				Destroy(transform.gameObject, 0.2f);
				Player.key = true;
			}
		}
	}
}