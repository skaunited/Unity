using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Holoville.HOTween;

public class PopUpManager : MonoBehaviour {

	public int PopNb;

	private List<PopUpScript> _popups = new List<PopUpScript>();

	public void PushPopUp(PopUpScript popUp) {
		popUp.transform.SetParent(transform, false);
		popUp.transform.localPosition = new Vector2(0f, -2000f);
		_popups.Add(popUp);
	}

	public void PushPopUp(GameObject prefab) {
		GameObject popUp = GameObject.Instantiate (prefab) as GameObject;
		PushPopUp (popUp.GetComponent<PopUpScript>());
	}

	public void DisplayPopUp() {
		if (_popups.Count != 0)
		{
			_popups[0].PopUpClosed += HidePopup;
			TweenParms p = new TweenParms().Prop("localPosition", new Vector3 (0f, 0f, 0f));
			HOTween.To(_popups[0].transform, 0.3f, p);
		}
	}

	private void HidePopup() {
		TweenParms p = new TweenParms().Prop ("localPosition", new Vector3 (0f, -2000f, 0f));
		p.OnComplete (CloseAnimEnded);
		HOTween.To(_popups[0].transform, 0.3f, p);
	}

	private void CloseAnimEnded(TweenEvent e) {
		PopUpScript tmp = _popups[0];
		_popups.Remove (_popups [0]);
		Destroy (tmp.gameObject);
		DisplayPopUp ();
	}

	void Start () {
		HOTween.Init(true, true, true);
	}

}
