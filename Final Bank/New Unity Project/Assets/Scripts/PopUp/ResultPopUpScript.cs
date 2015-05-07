using UnityEngine;
using System.Collections;

public class ResultPopUpScript : MonoBehaviour {

	[SerializeField] private PopUpScript _pus;

	public void Hide() {
		_pus.OnPopUpClosed ();
	}

	void Start () {
	
	}
	
	void Update () {
	
	}
}
