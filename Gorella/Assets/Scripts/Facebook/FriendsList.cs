using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;
using System;
using Parse;
using Eppy;

public class FriendsList : MonoBehaviour {

	#region Attributes
	public FacebookManagerScript fb;
	public Texture initPics;
	
	public List<object> friends = new List<object>();
	public List<Tuple<ParseObject, ParseFile,Texture>> FbFriends = new List<Tuple<ParseObject, ParseFile, Texture>>();

	private bool start = false;
	private bool request = false;
	private bool fill = false;
	private bool done = false;
	private bool downloaded = false;
	private int bild = 0;
	private WWW wwwData;

	private int me = -1;
	#endregion

	#region Initialisation
	public FriendsList() {}

	void Start() {
		GameObject test = GameObject.Find("Facebook");
		fb = test.GetComponent<FacebookManagerScript>();
		UnityEngine.Object.DontDestroyOnLoad(transform.gameObject);
		initPics = (Texture)Resources.Load("waiting-wheel-300x300");
	}

	private void Reset() {
		if (!fill)
		{
			friends = fb.DeserializeJSONFriends(fb.jason);
			fill = true;
		}
		request = false;
		done = false;
		downloaded = false;
		bild = 0;
		
		FbFriends = new List<Tuple<ParseObject, ParseFile, Texture>>();
	}
	
	protected Dictionary<string, string> FriendID(List<object> friends, int i)
	{
		var fd = ((Dictionary<string, object>)(friends[i]));
		var friend = new Dictionary<string, string>();
		friend["first_name"] = (string)fd["first_name"];
		friend["id"] = (string)fd["id"];
		Debug.Log(friend["first_name"]+"=====");
		return friend;
	}
	#endregion

	#region Download
	private void Download() {
		if (bild <= friends.Count)
		{
			if (!done)
			{
				StartCoroutine(WaitForDownload(FbFriends[bild].Item2.Url.ToString(), bild));
				done = true;
			}
			if (downloaded)
			{
				downloaded = false;
				done = false;
				bild++;
			}
		}
	}
	
	private IEnumerator WaitForDownload(string sURL, int i)
	{
		wwwData = new WWW(sURL);
		print("downloading " + i);
		yield return wwwData;
		downloaded = true;
		FbFriends[i].Item3 = wwwData.texture;
		print("done");
	}
	#endregion

	#region Profiles
	void FixedUpdate() {
		if (me != -1)
		{
			if ((PlayerPrefs.HasKey("HighScore") && PlayerPrefs.GetInt("HighScore") < me)
			    || !PlayerPrefs.HasKey("HighScore"))
				PlayerPrefs.SetInt("HighScore", me);
		}
	}

	private void Profile() {
		var query = ParseObject.GetQuery("player").WhereEqualTo("UserID", FB.UserId);
		query.FirstAsync().ContinueWith(t =>
		                                {
			me = Convert.ToInt32(t.Result["Score"]);
			FbFriends.Add(Tuple.Create(t.Result, t.Result.Get<ParseFile>("Picture"), initPics));
		
			FbFriends.Sort((x, y) =>
			               {
				return Convert.ToInt32(y.Item1["Score"]).CompareTo(Convert.ToInt32(x.Item1["Score"]));
			});
		});
		//FbFriends.Reverse();
	}
	#endregion

	#region Fill
	void Update() {
		if (start)
		{
			if (!request)
			{
				Reset();
				for (int i=0; i<friends.Count; i++)
				{
					var friend = FriendID(friends, i);
					var query = ParseObject.GetQuery("player").WhereEqualTo("UserID", friend["id"].ToString());
					query.FirstAsync().ContinueWith(t =>
					{
						ParseObject result = t.Result;
						result["First"] = friend["first_name"];
						FbFriends.Add(Tuple.Create(result, t.Result.Get<ParseFile>("Picture"), initPics));
					});
				}
				Profile();
				request = true;
			}
			if (request)
			{
				if (FbFriends.Count > 0)
					Download();
			}
		}
		else
		{
			if (fb.jason != null)
				start = true;
		}
	}
	#endregion
}