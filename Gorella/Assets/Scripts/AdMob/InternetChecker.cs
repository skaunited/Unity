using UnityEngine;

public class InternetChecker : MonoBehaviour
{
	public bool internet = false;

	private const bool allowCarrierDataNetwork = false;
	private const string pingAddress = "8.8.8.8"; // Google Public DNS server
	private const float waitingTime = 2.0f;
	
	private Ping ping;
	private float pingStartTime;

	public void Start()
	{
		Object.DontDestroyOnLoad(transform.gameObject);

		bool internetPossiblyAvailable;
		switch (Application.internetReachability)
		{
		case NetworkReachability.ReachableViaLocalAreaNetwork:
			internetPossiblyAvailable = true;
			break;
		case NetworkReachability.ReachableViaCarrierDataNetwork:
			internetPossiblyAvailable = allowCarrierDataNetwork;
			break;
		default:
			internetPossiblyAvailable = false;
			break;
		}
		if (!internetPossiblyAvailable)
		{
			InternetIsNotAvailable();
			return;
		}
		ping = new Ping(pingAddress);
		pingStartTime = Time.time;
	}
	
	public void Update()
	{
		if (ping != null)
		{
			bool stopCheck = true;
			if (ping.isDone)
				InternetAvailable();
			else if (Time.time - pingStartTime < waitingTime)
				stopCheck = false;
			else
				InternetIsNotAvailable();
			if (stopCheck)
				ping = null;
		}
	}
	
	private void InternetIsNotAvailable()
	{
		internet = false;
		Debug.Log("No Internet :(");
	}
	
	private void InternetAvailable()
	{
		internet = true;
		Debug.Log("Internet is available! ;)");
	}
}