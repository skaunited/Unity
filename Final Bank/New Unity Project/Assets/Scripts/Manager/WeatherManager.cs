using UnityEngine;
using System.Collections;
using System;

public class WeatherManager : MonoBehaviour {

	public GameObject rain2;
	public GameObject snow;

	private int month;
	private int lastMonth;

	private GameObject currentWeather = null;

	private HomeScript home;

	void Start() {
		home = GameObject.Find("Canvas/HomeMenu").GetComponent<HomeScript> ();
		lastMonth = 0;
	}

	void Update () {
		month = Convert.ToInt32(home.getMonth ());
		if (lastMonth != month)
		{
			if (currentWeather != null)
			{
				GameObject tmp = currentWeather;
				Destroy(tmp);
			}
			if (month == 12)
				currentWeather = Instantiate(rain2) as GameObject;
			else if (month == 1)
				currentWeather = Instantiate(snow) as GameObject;
			else if (month == 2)
				currentWeather = Instantiate(snow) as GameObject;
			else if (month == 3)
				currentWeather = Instantiate(rain2) as GameObject;
			lastMonth = month;
		}
	}
}
