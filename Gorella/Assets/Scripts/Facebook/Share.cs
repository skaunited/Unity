using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using Parse;
using Eppy;

public class Share : MonoBehaviour {

	#region Attributes
	public Texture2D shareButton;
	public FacebookManagerScript fb;
	public FriendsList friend;

	private List<Tuple<ParseObject, ParseFile,Texture>> FbFriends = new List<Tuple<ParseObject, ParseFile, Texture>>();
	private int current;
	private int h = Screen.height;
	private int w = Screen.width;
	private bool check = false;
	
	void Update() {
		GameObject net = GameObject.Find("internet");
		if (net != null)
		{
			InternetChecker connection = net.GetComponent<InternetChecker>();
			if (connection.internet && !check)
			{
				h -= (AdSize.Leaderboard.Height + 25);
				check = true;
			}
			if (!connection.internet)
			{
				h = Screen.height;
				check = false;
			}
		}
	}

	void Start() {
		GameObject empty = GameObject.Find("Facebook").gameObject;
		if (empty != null)
			fb = empty.GetComponent<FacebookManagerScript>();
	}
	#endregion
	
	/*#region check beaten friend
	void Awake() {
		if (FB.IsLoggedIn)
		{
			current = PlayerPrefs.GetInt("current");
			int myScore = PlayerPrefs.GetInt("HighScore");

			if (current < myScore)
			{
				GameObject empty = GameObject.Find("Friends");
				if (empty != null)
				{
					friend = empty.GetComponent<FriendsList>();
					FbFriends = friend.FbFriends;
					foreach (Tuple<ParseObject, ParseFile, Texture> user in FbFriends)
					{
						int score = Convert.ToInt32(user.Item1["Score"]);
						if (myScore > score)
						{
							if (current <= score)
							{
								fb.ShareOnFB(user.Item1["UserID"].ToString(), "Jump", "Description test", "");
								break;
							}
						}
					}
				}
			}
		}
	}
	#endregion*/

	#region the share
	void OnGUI() {
		if (FB.IsLoggedIn)
		{
			if (GUI.Button(new Rect(0, (h - (h / 2)), w, (h / 8)), shareButton))
				fb.ShareOnFB(FB.UserId.ToString(), "Jump", "Description test", "http://www.alpedhuez.com/sitra/images/SOMMAIRE_OFFRE_JANVIER_200px.jpg");
		}
	}
	#endregion
}