using UnityEngine;
using Parse;
using Facebook.MiniJSON;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class World : MonoBehaviour {

	#region Attributes
	public static bool show = false;
	public Texture initPics;

	private ParseObject[] best5 = new ParseObject[5];
	private Texture[] pic = new Texture[5];
	private ParseFile[] imageFile = new ParseFile[5];

	private bool done = false;
	private bool request = false;
	private bool downloaded = false;
	private int bild = 0;
	private WWW wwwData;

	private ParseObject user = null;
	private ParseObject before = null;
	private ParseObject after = null;

	private Texture beforePic = new Texture();
	private Texture userPic = new Texture();
	private Texture afterPic = new Texture();

	private int pos = 0;
	private bool goBefore = false;
	private bool goAfter = false;
	private bool goUser = false;
	private bool others = false;

	private int w = Screen.width;
	private int h = Screen.height;
	#endregion

	#region Download
	void Reset() {
		bild = 0;
		pos = 0;

		others = false;
		done = false;
		downloaded = false;
		others = false;
		goBefore = false;
		goUser = false;
		goAfter = false;

		user = null;
		before = null;
		after = null;

		userPic = initPics;
		beforePic = initPics;
		afterPic = initPics;

		for (int i=0; i<5; i++)
		{
			best5[i] = null;
			pic[i] = initPics;
		}
	}

	void Download() {
		if (bild < 5)
		{
			if (!done)
			{
				StartCoroutine(WaitForDownload(this.imageFile[bild].Url.ToString(), bild));
				done = true;
			}
			if (downloaded)
			{
				downloaded = false;
				done = false;
				bild++;
				if (bild == 5)
					goBefore = true;
			}
		}
		else if (bild == 5 && downloaded)
		{
			downloaded = false;
			goUser = true;
			bild++;
		}
		else if (bild == 6 && downloaded)
		{
			downloaded = false;
			goAfter = true;
			bild++;
		}
	}
	
	private IEnumerator WaitForDownload(string sURL, int i)
	{
		wwwData = new WWW(sURL);
		yield return wwwData;
		downloaded = true;
		if (i < 5)
			pic[i] = wwwData.texture;
		else if (i == 5)
			beforePic = wwwData.texture;
		else if (i == 6)
			userPic = wwwData.texture;
		else
			afterPic = wwwData.texture;
	}
	#endregion

	#region GUI
	void OnGUI() {
		GUIStyle style = new GUIStyle();
		style.fontSize = (int)(Screen.dpi / 10);
		style.normal.textColor = Color.white;
		style.hover.textColor = Color.white;

		if (show)
		{
			GUI.color = Color.white;
			if (!request)
			{
				Reset();
				var query = ParseObject.GetQuery("player").OrderByDescending("Score").Limit(5);
				query.FindAsync().ContinueWith(t =>
				{
					int i = 0;
					IEnumerable<ParseObject> results = t.Result;
					
					foreach (ParseObject res in results)
					{
						best5[i] = res;
						imageFile[i] = res.Get<ParseFile>("Picture");
						i++;
					}
					request = true;
				});
			}
			if (request)
			{
				int j = (int)(h / 13);
				Download();
				for (int i=0; i<5; i++)
				{
					if (pos == 0)
					{
						if (best5[i]["UserID"].ToString() == FB.UserId.ToString())
						{
							GUI.color = Color.yellow;
							pos = -1;
						}
					}
					string name = best5[i]["Name"].ToString();
					if (name.Length > 23)
						name = name.Remove(23) + "...";

					GUI.Label(new Rect((w / 11), (w / 5) + j, (h / 30), (h / 12)), (i + 1).ToString(), style);
					GUI.DrawTexture(new Rect((w / 6), (w / 6) + j, (h / 12), (h / 12)), pic[i]);
					GUI.Label(new Rect((w / 2.8f), (w / 5) + j, (h / 4), (h / 12)), name, style);
					GUI.Label(new Rect((w / 1.2f), (w / 5) + j, (h / 12), (h / 12)), best5[i]["Score"].ToString(), style);
					GUI.color = Color.white;
					j += (int)(h / 12);
				}
				GUI.Label(new Rect((h / 4), (w / 5) + j, (h / 12), (h / 12)), "...", style);
				j += (int)(h / 12);
				if (pos != -1)
				{
					if (before != null)
					{
						GUI.Label(new Rect((w / 10), (w / 5) + j, (w / 17), (h / 12)), (pos - 1).ToString(), style);
						if (goBefore)
						{
							ParseFile imageFile = before.Get<ParseFile>("Picture");
							StartCoroutine(WaitForDownload(imageFile.Url.ToString(), bild));
							goBefore = false;
						}
						string name = before["Name"].ToString();
						if (name.Length > 23)
							name = name.Remove(23) + "...";

						GUI.DrawTexture(new Rect((w / 6), (w / 6) + j, (h / 12), (h / 12)), beforePic);
						GUI.Label(new Rect((w / 2.8f), (w / 5) + j, (h / 4), (h / 12)), name, style);
						GUI.Label(new Rect((w / 1.2f), (w / 5) + j, (h / 12), (h / 12)), before["Score"].ToString(), style);
						j += (int)(h / 12);
					}

					if (user == null)
						Profile(0);
					if (user != null)
					{
						if (pos == 0)
							Profile(1);
						else if (!others)
							GetOthers();

						GUI.DrawTexture(new Rect((w / 6), (w / 6) + j, (h / 12), (h / 12)), userPic);
						GUI.color = Color.yellow;
						GUI.Label(new Rect((w / 11), (w / 5) + j, (h / 30), (h / 12)), pos.ToString(), style);

						if (goUser)
						{
							ParseFile imageFile = user.Get<ParseFile>("Picture");
							StartCoroutine(WaitForDownload(imageFile.Url.ToString(), bild));
							goUser = false;
						}
						string name = user["Name"].ToString();
						if (name.Length > 23)
							name = name.Remove(23) + "...";

						GUI.Label(new Rect((w / 2.8f), (w / 5) + j, (h / 4), (h / 12)), name, style);
						GUI.Label(new Rect((w / 1.2f), (w / 5) + j, (h / 12), (h / 12)), user["Score"].ToString(), style);
						GUI.color = Color.white;
					}
					j += (int)(h / 12);

					if (after != null)
					{
						GUI.Label(new Rect((w / 11), (w / 5) + j, (h / 30), (h / 12)), (pos + 1).ToString(), style);
						if (goAfter)
						{
							ParseFile imageFile = after.Get<ParseFile>("Picture");
							StartCoroutine(WaitForDownload(imageFile.Url.ToString(), bild));
							goAfter = false;
						}
						string name = after["Name"].ToString();
						if (name.Length > 23)
							name = name.Remove(23) + "...";

						GUI.DrawTexture(new Rect((w / 6), (w / 6) + j, (h / 12), (h / 12)), afterPic);
						GUI.Label(new Rect((w / 2.8f), (w / 5) + j, (h / 4), (h / 12)), name, style);
						GUI.Label(new Rect((w / 1.2f), (w / 5) + j, (h / 12), (h / 12)), after["Score"].ToString(), style);
					}
				}
			}
		}
		else
			request = false;
	}
	#endregion

	#region Profiles
	void Profile(int quest) {
		if (quest == 0)
		{
			var query = ParseObject.GetQuery("player").WhereEqualTo("UserID", FB.UserId);
			query.FirstAsync().ContinueWith(t =>
			{
				ParseObject results = t.Result;
				user = results;
			});
		}

		if (quest == 1)
		{
			var query = ParseObject.GetQuery("player").WhereGreaterThan("Score", user["Score"]);
			query.CountAsync().ContinueWith(t =>
			{
				pos = t.Result + 1;
			});
		}
	}

	void GetOthers() {
		var query = ParseObject.GetQuery("player").OrderByDescending("Score").Skip(pos - 2);
		if (pos != 6)
		{
			query.FirstAsync().ContinueWith(t =>
			{
				ParseObject results = t.Result;
				before = results;
			});
		}

		query = ParseObject.GetQuery("player").OrderByDescending("Score").Skip(pos);
		query.FirstAsync().ContinueWith(t =>
		{
			ParseObject results = t.Result;
			after = results;
		});
		others = true;
	}
	#endregion
}