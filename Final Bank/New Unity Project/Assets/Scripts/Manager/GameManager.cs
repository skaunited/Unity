using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager {
	
	private static GameManager _instance = new GameManager();
	
	public static GameManager Instance {
		get {
			return (_instance);
		}
	}
	
	public bool									MiniGameEnded = false;
	public readonly Dictionary<string, float>	values = new Dictionary<string, float>();
	public int									Months = 0;

	public bool									garage = false;
	public bool									house = false;

	private GameManager () {
		values.Add ("Ancien Solde", 0f);
		values.Add ("Salaires", 0f);
		values.Add ("Autres Gains", 0f);
		values.Add ("Achat de biens", 0f);
		values.Add ("Credit Immobilier", 0f);
		values.Add ("Credit Auto", 0f);
		values.Add ("Autres Emprunts", 0f);
		values.Add ("Depenses quotidiennes", 0f);
		values.Add ("Assurances", 0f);
		values.Add ("Remboursements Assurances", 0f);
		values.Add ("Total", 500f);
	}

	public void setLastSolde() {values ["Ancien Solde"] = values ["Total"];}

	public void ChangeValues (string key, float value) {
		if (values.ContainsKey(key))
		{
			values[key] = value;
			values["Total"] += value;
		}
	}
}