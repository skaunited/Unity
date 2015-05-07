using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UpgradeScript : MonoBehaviour {

	#region Attributes
	public Text _warning;
	public GameObject upgradedObject;
	public GameObject parent;
	public GameObject particule;

	[SerializeField] private PopUpManager poop;
	[SerializeField] private GameObject resultPrefab;
	[SerializeField] private GameObject eventPrefab;
	#endregion

	#region Haupt
	void Particles() {
		GameObject par = Instantiate(particule) as GameObject;
		par.transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z + 3);
		Destroy(par, 5.0f);
		StartCoroutine(waiting(0.5f));
	}

	private IEnumerator waiting(float sec)
	{
		yield return new WaitForSeconds(sec);
		_warning.text = string.Format("");
		if (sec != 2)
		{
			if (transform.name == "OldHouse")
				InstanceHouse();
			else if (transform.name == "MyCar")
				InstanceGarage();
		}
		else if (transform.name == "OldHouse" && GameManager.Instance.values["Credit Immobilier"] == 0)
			proposerPret();
	}

	void Start() {
		if (GameManager.Instance.house && transform.name == "OldHouse")
		{
			Destroy (gameObject);
			GameObject obj = Instantiate (upgradedObject) as GameObject;
			Vector3 pos = obj.transform.position;
			if (parent != null)
				obj.transform.parent = parent.transform;
			obj.transform.localPosition = pos;
			this.enabled = false;
		}
		if (GameManager.Instance.garage && transform.name == "MyCar")
		{
			GameObject obj = Instantiate (upgradedObject) as GameObject;
			if (parent != null)
				Destroy (parent.gameObject);
			obj.transform.position = new Vector3(40, 1, 38.5f);
			obj.transform.localScale = new Vector3 (4, 4, 4);
			transform.parent.transform.position = new Vector3 (42, 0, 44.5f);
			transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
			Destroy(this);

		}
	}

	void OnMouseDown() {

		if (transform.name == "OldHouse")
			ChangeHouse (10000f);
		else if (transform.name == "MyCar")
			getPark(350f);
	}
	#endregion

	#region others
	void ChangeHouse(float price) {
		if (!buy (price, "Achat de biens"))
			return;
	}

	bool buy(float price, string key) {
		if (GameManager.Instance.values["Total"] < price)
		{
			string text = String.Format("<color=red>Pas assez d'argent !</color>");
			_warning.text = string.Format("{0}", text);
			StartCoroutine(waiting(2f));
			return false;
		}
		else
		{
			GameManager.Instance.ChangeValues(key, -price);
			Particles ();
			return true;
		}
	}
	
	void getPark(float price) {
		if (!buy (price, "Achat de biens"))
			return;
	}
	#endregion

	#region Instance
	void InstanceGarage() {
		GameManager.Instance.garage = true;
		GameObject obj = Instantiate (upgradedObject) as GameObject;
		if (parent != null)
			Destroy (parent.gameObject);
		obj.transform.position = new Vector3(40, 1, 38.5f);
		obj.transform.localScale = new Vector3 (4, 4, 4);
		transform.parent.transform.position = new Vector3 (42, 0, 44.5f);
		transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
		Destroy(this);
	}

	void InstanceHouse() {
		GameManager.Instance.house = true;
		Destroy (gameObject);
		GameObject obj = Instantiate (upgradedObject) as GameObject;
		Vector3 pos = obj.transform.position;
		if (parent != null)
			obj.transform.parent = parent.transform;
		obj.transform.localPosition = pos;
		this.enabled = false;
	}
	#endregion

	void	proposerPret() {
		GameObject popUp = GameObject.Instantiate (eventPrefab) as GameObject;
		popUp.GetComponent<EventsScript> ().Init ("Pret Immobilier",
		                                          "Car_sprite_8",
		                                          "",
		                                          "Vous voulez devenir propriétaire d'une nouvelle résidence ?\n" +
		                                          "Decouvrez notre offres de pret pour vous accompagner dans votre projet immobilier.\n" +
		                                          "Plus d'informations sur www.credit-agricole.fr\n" +
		                                          "Accepter pret de <color=green>10000 euros</color>.",
		                                          () => {
			GameManager.Instance.ChangeValues("Autres Gains", 10000);
			GameManager.Instance.ChangeValues("Credit Immobilier", -800);
		}
		);
		poop.PushPopUp (popUp.GetComponent<PopUpScript> ());

		poop.DisplayPopUp ();
	}

}
