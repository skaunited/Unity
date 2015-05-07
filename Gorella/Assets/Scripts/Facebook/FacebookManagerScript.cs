using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using Facebook.MiniJSON;
using System.Collections.Generic;
using Parse;
using System.Threading.Tasks;

public class FacebookManagerScript : MonoBehaviour
{
	#region Attributes
	public string KEY_FB_CAPTION = "FB.caption";
	private static readonly string FB_API_REQUEST_INFO = "/me?fields=id,name,friends.fields(first_name,id)";

	/// <summary>
	/// profile picture texture
	/// </summary>
	public Texture UserPicture { get; private set; }
	/// <summary>
	/// profile name text
	/// </summary>
	public string Username { get; private set; }
	/// <summary>
	/// profile json object
	/// </summary>
	public string jason { get; private set; }

//		private event EventHandler pictureLoadedHandler;
	private static Dictionary<string, string> profile = null;

	delegate void LoadPictureCallback(Texture texture);
	#endregion

	#region MonoBehaviour
	void Awake()
	{
	    enabled = false;
	    FB.Init(SetInit, OnHideUnity);
	}

	private void SetInit()
	{
	    enabled = true; // "enabled" is a property inherited from MonoBehaviour           
	    if (FB.IsLoggedIn)
	    {
	        OnLoggedIn();
	    }
	}

	private void OnHideUnity(bool isGameShown)
	{
	    if (!isGameShown)
	    {
	        // pause the game - we will need to hide
	        Time.timeScale = 0;
	    }
	    else
	    {
	        // start the game back up - we're getting focus again
	        Time.timeScale = 1;
	    }
	}
	#endregion

	#region FB
	/// <summary>
	/// Login on facebook
	/// </summary>
	public void Login()
	{
		FB.Login("user_friends", LoginCallback);
	}

	/// <summary>
	/// Logout from facebook
	/// </summary>
	public void Logout()
	{
	    FB.Logout();
	    ParseUser.LogOut();
	}

	void LoginCallback(FBResult result)
	{
		if (FB.IsLoggedIn)
			OnLoggedIn();
		else
			Debug.Log ("FBLoginCallback: User canceled login");
	}

	/// <summary>
	/// Reqest player info and profile picture
	/// </summary>
	void OnLoggedIn()
	{
	    FB.API(FB_API_REQUEST_INFO, Facebook.HttpMethod.GET, APICallback);
	    LoadPicture(GetPictureURL("me", 200, 200), MyPictureCallback);
	}

	/// <summary>
	/// Request player facebook infos Callback
	/// </summary>
	void APICallback(FBResult result)
	{
	    if (result.Error != null)
	    {
	        // Let's just try again
	        FB.API(FB_API_REQUEST_INFO, Facebook.HttpMethod.GET, APICallback);
	        return;
	    }
	    profile = DeserializeJSONProfile(result.Text);
		jason = result.Text;
		Username = profile["name"];
	}

	/// <summary>
	/// Getting user name infos
	/// </summary>        
	public static Dictionary<string, string> DeserializeJSONProfile(string response)
	{
	    var responseObject = Json.Deserialize(response) as Dictionary<string, object>;
	    object nameH;
	    var profile = new Dictionary<string, string>();
	    if (responseObject.TryGetValue("name", out nameH))
	    {
	        profile["name"] = (string)nameH;
	    }
	    return profile;
	}

	/// <summary>
	/// Getting Friends infos
	/// </summary>      
	public List<object> DeserializeJSONFriends(string response)
	{
		var responseObject = Json.Deserialize(response) as Dictionary<string, object>;
		object friendsH;
		var friends = new List<object>();
		if (responseObject.TryGetValue("invitable_friends", out friendsH))
		{
			friends = (List<object>)(((Dictionary<string, object>)friendsH)["data"]);
		}
		if (responseObject.TryGetValue("friends", out friendsH))
		{
			friends.AddRange((List<object>)(((Dictionary<string, object>)friendsH)["data"]));
		}
		return friends;
	}
	
	/// <summary>
	/// Request profile picture URL
	/// </summary>
	public string GetPictureURL(string facebookID, int? width = null, int? height = null, string type = null)
	{
	    string url = string.Format("/{0}/picture", facebookID);
	    string query = width != null ? "&width=" + width.ToString() : "";
	    query += height != null ? "&height=" + height.ToString() : "";
	    query += type != null ? "&type=" + type : "";
	    query += "&redirect=false";
	    if (query != "") url += ("?g" + query);
	    return url;
	}

	/// <summary>
	/// Called when player profile picture is loaded and save the picture in player prefs
	/// </summary>
	void MyPictureCallback(Texture texture)
	{
	    if (texture == null)
	    {
	        // Let's just try again
	        LoadPicture(GetPictureURL("me", 200, 200), MyPictureCallback);
	        return;
	    }
	    UserPicture = texture;
	}

	/// <summary>
	/// Loading picture coroutine
	/// </summary>
	IEnumerator LoadPictureEnumerator(string url, LoadPictureCallback callback)
	{
	    WWW www = new WWW(url);
	    yield return www;
	    callback(www.texture);
	}

	/// <summary>
	/// Deserializing Picture URL String
	/// </summary>
	public static string DeserializePictureURLString(string response)
	{
	    return DeserializePictureURLObject(Json.Deserialize(response));
	}

	/// <summary>
	/// Requesting profile picture
	/// </summary>
	void LoadPicture(string url, LoadPictureCallback callback)
	{
	    FB.API(url, Facebook.HttpMethod.GET, result =>
	    {
	        if (result.Error != null)
	        {
	            Debug.LogError(result.Error);
	            return;
	        }

	        var imageUrl = DeserializePictureURLString(result.Text);

	        StartCoroutine(LoadPictureEnumerator(imageUrl, callback));
	    });
	}

	/// <summary>
	/// Deserializing Picture URL Object
	/// </summary>
	public static string DeserializePictureURLObject(object pictureObj)
	{
	    var picture = (Dictionary<string, object>)(((Dictionary<string, object>)pictureObj)["data"]);
	    object urlH = null;
	    if (picture.TryGetValue("url", out urlH))
	    {
	        return (string)urlH;
	    }
	    return null;
	}

	/// <summary>
	/// Share on facebook
	/// </summary>
	public void ShareOnFB(string toID, string linkName, string linkDescription, string picture)
	{
	    FB.Feed(
			toId : toID,
			link: "http://www.google.fr/",
	        linkName: linkName,
	        linkCaption: "",
	        linkDescription: linkDescription,
	        picture: picture
	        );
	}
	#endregion
}