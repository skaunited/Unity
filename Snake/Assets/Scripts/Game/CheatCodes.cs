using UnityEngine;
using System.Collections;

public class CheatCodes : MonoBehaviour {

	private bool[] ahmed = new bool[4]{false, false, false, false};
	private SnakeBehaviour behaviour;

	void Start() {
		GameObject snake = GameObject.Find("Snake").gameObject;
		behaviour = snake.GetComponent<SnakeBehaviour>();
	}

	void Update () {
		CheckAhmed();
	}

	void CheckAhmed() {
		if (Input.GetKeyDown(KeyCode.A))
		{
			ahmed = new bool[4]{true, false, false, false};
			return;
		}

		if (Input.anyKeyDown && ahmed[0])
		{
			if (!ahmed[1])
			{
				if (Input.GetKeyDown(KeyCode.H))
					ahmed[1] = true;
				else
				{
					ahmed = new bool[4]{false, false, false, false};
					return;
				}
			}
			else if (!ahmed[2])
			{
				if (Input.GetKeyDown(KeyCode.M))
					ahmed[2] = true;
				else
				{
					ahmed = new bool[4]{false, false, false, false};
					return;
				}
			}
			else if (!ahmed[3])
			{
				if (Input.GetKeyDown(KeyCode.E))
					ahmed[3] = true;
				else
				{
					ahmed = new bool[4]{false, false, false, false};
					return;
				}
			}
			else if (ahmed[3])
			{
				if (Input.GetKeyDown(KeyCode.D))
					behaviour.Score += 20;
				ahmed = new bool[4]{false, false, false, false};
				return;
			}
		}
	}
}