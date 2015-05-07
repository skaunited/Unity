using UnityEngine;
using System.Collections;

public class Ppptest : MonoBehaviour {
	public GameObject prefab;
	public PopUpManager poop;

	public void ShowResults () {
		poop.PushPopUp (prefab);
		poop.DisplayPopUp ();
	}
}