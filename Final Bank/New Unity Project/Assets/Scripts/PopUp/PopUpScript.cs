using UnityEngine;
using System.Collections;

public class PopUpScript : MonoBehaviour {
	public delegate void PopUpHandler();
	public event PopUpHandler PopUpClosed;

	public void OnPopUpClosed() {
		if (PopUpClosed != null)
			PopUpClosed();
	}
}