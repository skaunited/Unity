using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class HomeScript : MonoBehaviour {

	[SerializeField] private Text _money;
	[SerializeField] private Text _date;
	[SerializeField] private PopUpManager poop;
	[SerializeField] private GameObject resultPrefab;
	[SerializeField] private GameObject eventPrefab;

	private string _month;
	private string _year;

	public float Money {
		set { _money.text = string.Format("{0} €", value.ToString());}
	}

	 public DateTime Date {
		set {
			_month = value.ToString("MM");
			_year = value.ToString("yyyy");
			_date.text = string.Format("{0} / {1}", _month, _year);
		}
	}

	void Start () {
		Date = DateTime.Now;
		if (GameManager.Instance.MiniGameEnded)
		{
			MonthUp();
			GameManager.Instance.MiniGameEnded = false;
			MonthDigest();
		}
	}
	
	void MonthUp () {
		GameManager.Instance.setLastSolde ();
		GameManager.Instance.Months += 1;
		GameManager.Instance.ChangeValues ("Achat de biens", 0);
		GameManager.Instance.ChangeValues ("Assurances", GameManager.Instance.values["Assurances"]);
		GameManager.Instance.ChangeValues ("Credit Immobilier", GameManager.Instance.values["Credit Immobilier"]);
		GameManager.Instance.ChangeValues ("Autres Gains", 0);
		int month = Convert.ToInt32 (_month) + GameManager.Instance.Months;
		int year = Convert.ToInt32 (_year);
		while (month > 12)
		{
			month -= 12;
			year++;
		}
		_month = month.ToString();
		_year = year.ToString();
		_date.text = string.Format("{0} / {1}", _month, _year);

		GameManager.Instance.ChangeValues("Depenses quotidiennes", -500);
		int salaire = PlayerPrefs.GetInt ("salaire");
		if (GameManager.Instance.values["Assurances"] < 0)
			GameManager.Instance.ChangeValues("Remboursements Assurances", (2000 - salaire) / 2);
		GameManager.Instance.ChangeValues("Salaires", salaire);
		
	}

	public string getMonth() {return _month;}

	void Update() {
		Money = GameManager.Instance.values["Total"];
	}

	void MonthDigest() {
		GameObject popUp = GameObject.Instantiate (resultPrefab) as GameObject;
		poop.PushPopUp (popUp.GetComponent<PopUpScript>());
		if (GameManager.Instance.Months == 1)
		{
			popUp = GameObject.Instantiate (eventPrefab) as GameObject;
			popUp.GetComponent<EventsScript> ().Init ("Assurance Voiture",
										"Car_sprite_8",
										"",
										"Vous avez aquis une voiture il y a peu. Votre banque vous propose une assurance auto." +
			                            "D'après vos informations, voici l'offre qui vous correspond le mieux:\nTOUS RIQUES INITIAL, a <color=green> 120 euros</color>.",
			                                          () => {GameManager.Instance.ChangeValues("Assurances", -120);}
			);
			poop.PushPopUp (popUp.GetComponent<PopUpScript> ());
		}
		poop.DisplayPopUp ();
	}
}
