using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;
using System;
using Parse;
using Eppy;

public class NextFriend : MonoBehaviour {

	public FriendsList fb;

	private List<Tuple<ParseObject, ParseFile,Texture>> FbFriends = new List<Tuple<ParseObject, ParseFile, Texture>>();
	private int score = -1;
	private int h = Screen.height;
	private int w = Screen.width;

	void Awake() {
		if (FB.IsLoggedIn)
		{
			GameObject empty = GameObject.Find("Friends");
			if (empty != null)
			{
				fb = empty.GetComponent<FriendsList>();
				FbFriends = fb.FbFriends;
				FbFriends.Reverse();
			}
		}
	}

	void OnGUI() {
		GameObject empty = GameObject.Find("player");
		if (empty != null)
		{
			PlayerControl control = empty.GetComponent<PlayerControl>();
			score = control.score;
		}

		if (score != -1)
		{
			foreach (Tuple<ParseObject, ParseFile, Texture> user in FbFriends)
			{
				ParseObject info = user.Item1;
				Texture pic = user.Item3;

				int friendScore = Convert.ToInt32(info["Score"]);

				if (score <= friendScore)
				{
					GUIStyle style = new GUIStyle();
					style.fontSize = (int)(Screen.dpi / 10);
					style.normal.textColor = Color.white;
					style.hover.textColor = Color.white;

					GUI.DrawTexture(new Rect((h / 8), 15, (h / 12), (h / 12)), pic);
					if (info["UserID"].ToString() == FB.UserId.ToString())
						GUI.Label(new Rect((h / 4.6f), (h / 28), (w / 4.48f), (h / 12)), "You", style);
					else
						GUI.Label(new Rect((h / 4.6f), (h / 28), (w / 4.48f), (h / 12)), info["First"].ToString(), style);
					GUI.Label(new Rect((w / 2.4f), (h / 16), (h / 12), (h / 12)), (friendScore - score).ToString(), style);
					break;
				}
			}
		}
	}
}