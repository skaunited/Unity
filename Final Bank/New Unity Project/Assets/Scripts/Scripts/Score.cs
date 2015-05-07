using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public static Score current;
	public float delay = 1f;
	public int Cash;
	public int minCash;

	public string Scene;

	public TextMesh normalT;
	public TextMesh shadowT;
	public TextMesh normalC;
	public TextMesh shadowC;

	private float _time = 0f;
	private float _score;

	void Start ()
	{
		current = this;
		_time = delay + 1;
		StartCoroutine("Scorer");
	}

	public void Ouch()
	{
		Cash -= minCash;
	}

	IEnumerator Scorer()
	{
		while (_time >= 0)
		{
			yield return new WaitForSeconds(0.1f);
			_time -= 0.1f;
			normalT.text = ((int)_time).ToString() + " s";
			shadowT.text = ((int)_time).ToString() + " s";

			normalC.text = Cash.ToString() + " €";
			shadowC.text = Cash.ToString() + " €";

			if (_time < 0f)
			{
				GameManager.Instance.MiniGameEnded = true;
				PlayerPrefs.SetInt("salaire", Cash);
				Application.LoadLevel(Scene);
			}
		}
	}
}
