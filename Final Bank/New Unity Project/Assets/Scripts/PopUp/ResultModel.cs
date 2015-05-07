using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System;

public class ResultModel : MonoBehaviour {

	[SerializeField] private Text _canvText;

	void Start () {
		string color;
		string text = "";
		foreach (var item in GameManager.Instance.values)
		{
			color = (item.Value > 0)? "green" : "red";
			text += String.Format("{0} : <color={1}>{2} €</color>\n", item.Key, color, item.Value);
		}
		_canvText.text = string.Format("{0}", text);
	}
}
