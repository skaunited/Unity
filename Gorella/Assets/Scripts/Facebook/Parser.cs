using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using Facebook.MiniJSON;
using System;
using Parse;

public class Parser : MonoBehaviour {

	public FacebookManagerScript fb;

	private bool log = false;

	#region profile
	void Start() {
		GameObject empty = GameObject.Find("Facebook");
		if (empty != null && empty != transform.gameObject)
			Destroy(empty);
		UnityEngine.Object.DontDestroyOnLoad(transform.gameObject);
	}
	
	void Update() {
		if (log && !FB.IsLoggedIn)
			log = false;

		if (FB.IsLoggedIn && !log)
		{
			if (fb.UserPicture != null)
			{
				ParseObject player = new ParseObject("player");

				Texture text = fb.UserPicture;
				Texture2D bla = text as Texture2D ;

				byte[] data = bla.EncodeToPNG();
				ParseFile file = new ParseFile("user.png", data);

				player["Score"] = PlayerPrefs.GetInt("HighScore");
				player["Name"] = fb.Username;
				player["UserID"] = FB.UserId;
				player["Picture"] = file;

				Parsing(player);
				log = true;
			}
		}
	}

	void Parsing(ParseObject player) {
		var query = ParseObject.GetQuery("player").WhereEqualTo("UserID", player["UserID"]);
		int count = 0;
		query.CountAsync().ContinueWith(t =>
		{
			Task saveTask;
			count = t.Result;
			if (count == 0)
			{
				saveTask = player.SaveAsync();
				if (!saveTask.IsCompleted)
					print("error with adding new player");
			}
			else
			{
				var quest = ParseObject.GetQuery("player").WhereEqualTo("UserID", FB.UserId);
				quest.FirstAsync().ContinueWith(tt =>
				{
					var result = tt.Result;
					result.SaveAsync().ContinueWith(ttt =>
					{
						result["Picture"] = player["Picture"];
						if (Convert.ToInt32(player["Score"]) > Convert.ToInt32(result["Score"]))
							result["Score"] = player["Score"];
						result.SaveAsync();
					});
				});
			}
		});
	}
	#endregion
}