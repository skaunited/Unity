using UnityEngine;
using System.Collections;

public class LittleMapDrawer : MonoBehaviour {

	public Texture background;
	public Texture foodTex;
	public Texture snakeTex;
	public Texture reverseTex;

	void OnGUI() {
		GameObject food = (GameObject.Find("Food") != null)? GameObject.Find("Food").gameObject : null;
		GameObject snake = GameObject.Find("Snake/Head").gameObject;
		GameObject reverse = GameObject.Find("ReverseSnake/Head").gameObject;

		float fX = 0, fZ = 0;
		if (food != null)
		{
			fX = food.transform.position.x;
			fZ = Screen.height - food.transform.position.z - 5;
		}

		float sX = snake.transform.position.x;
		float sZ = Screen.height - snake.transform.position.z - 5;

		float rX = reverse.transform.position.x;
		float rZ = Screen.height - reverse.transform.position.z - 5;

		GUI.DrawTexture(new Rect(0, Screen.height - 55, 55, 55), background);
		GUI.DrawTexture(new Rect(fX, fZ, 5, 5), foodTex);
		GUI.DrawTexture(new Rect(sX, sZ, 5, 5), snakeTex);
		GUI.DrawTexture(new Rect(rX, rZ, 5, 5), reverseTex);
	}
}