using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventsScript : MonoBehaviour {

	[SerializeField] private Text _title;
	[SerializeField] private Image _image;
	[SerializeField] private Text _content;
	[SerializeField] private Text _bank;
	[SerializeField] private Button _yes;
	[SerializeField] private Button _no;
	[SerializeField] private PopUpScript _pus;

	public void Init(string title, string image, string content, string bank, UnityEngine.Events.UnityAction led) {
		_title.text = title;
		_content.text = content;
		_bank.text = bank;
		_yes.onClick.AddListener(led);
		_yes.onClick.AddListener(_pus.OnPopUpClosed);
		_no.onClick.AddListener(_pus.OnPopUpClosed);
	}

	void Start () {
		
	}
	
	void Update () {
		
	}
}
