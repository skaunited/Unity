using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;
using System;
using Parse;
using Eppy;

public class Friends : MonoBehaviour {

	#region Attributes
	public static bool show = false;
	public FriendsList fb;

	private List<Tuple<ParseObject, ParseFile,Texture>> FbFriends = new List<Tuple<ParseObject, ParseFile, Texture>>();
	private bool request = false;

	private int w = Screen.width;
	private int h = Screen.height;

	#endregion

	#region Show
	void Update() {
		if (show)
		{
			if (!request)
			{
				GameObject empty = GameObject.Find("Friends");
				if (empty != null)
				{
					fb = empty.GetComponent<FriendsList>();
					FbFriends = fb.FbFriends;
					request = true;
				}
			}
		}
		else
			request = false;
	}

	void OnGUI() {
		if (show)
		{
			GUI.color = Color.white;
			if (request)
			{
				GUIStyle style = new GUIStyle();
				style.fontSize = (int)(Screen.dpi / 10);
				style.normal.textColor = Color.white;
				style.hover.textColor = Color.white;

				int j = (int)(h / 13), i = 0;
				foreach (Tuple<ParseObject, ParseFile, Texture> user in FbFriends)
				{
					ParseObject info = user.Item1;
					Texture pic = user.Item3;

					GUI.DrawTexture(new Rect((w / 6), (w / 6) + j, (h / 12), (h / 12)), pic);
					if (info["UserID"].ToString() == FB.UserId.ToString())
						GUI.color = Color.yellow;

					string name = info["Name"].ToString();
					if (name.Length > 23)
						name = name.Remove(23) + "...";

					GUI.Label(new Rect((w / 11), (w / 5) + j, (h / 30), (h / 12)), (++i).ToString(), style);
					GUI.Label(new Rect((w / 2.8f), (w / 5) + j, (h / 4), (h / 12)), name, style);
					GUI.Label(new Rect((w / 1.2f), (w / 5) + j, (h / 12), (h / 12)), info["Score"].ToString(), style);
					GUI.color = Color.white;
					j += (int)(h / 12);
				}
			}
		}
	}
	#endregion
}