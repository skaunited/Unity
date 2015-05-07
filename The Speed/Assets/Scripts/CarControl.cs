using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CarControl : MonoBehaviour {

	#region Attributes
	[SerializeField] private Button handBrakeButton;
	[SerializeField] private Button speedButton;
	[SerializeField] private Button brakeButton;
	[SerializeField] private EventSystem ev;
	private float vertical = 0.0f;
	private Vector3 dir;

	public WheelCollider WheelFL;
	public WheelCollider WheelFR;
	public WheelCollider WheelRL;
	public WheelCollider WheelRR;

	public Transform WheelFLTrans;
	public Transform WheelFRTrans;
	public Transform WheelRLTrans;
	public Transform WheelRRTrans;

	public GameObject BackLightObject;
	public Material IdleLightMaterial;
	public Material BrakeLightMaterial;
	public Material ReverseLightMaterial;

	public float currentSpeed;
	public float topSpeed = 150f;
	public float maxTorque = 50f;

	public int[] gearRatio;

	private float lowestSteerAtSpeed = 50f;
	private float lowSpeedSteerAngle = 10f;
	private float highSpeedSteerAngle = 1f;
	private float decelerationSpeed = 30f;
	private float maxReverseSpeed = 50f;

	private bool braked = false;
	private float maxBrakeTorque = 100f;

	private float mySidewayFriction;
	private float myForwardFriction;
	private float slipSidewayFriction;
	private float slipForwardFriction;

	private float theMaxSpeed;
	private float oldTime;
	#endregion

	void Start () {
		rigidbody.centerOfMass = new Vector3(0, -0.9f, 0.5f);
		oldTime = Time.realtimeSinceStartup;
		theMaxSpeed = topSpeed;
		topSpeed = 0.0F;
		SetValues();
	}
	
	void FixedUpdate () {
		StartCoroutine ("waiting");
		Control();
		//HandBrake();
	}

	IEnumerator waiting() {
		if (Input.GetMouseButton(0))
		{
			if (ev.currentSelectedGameObject != null)
			{
				if (ev.currentSelectedGameObject.name == speedButton.name)
					vertical = 1;
				else if (ev.currentSelectedGameObject.name == brakeButton.name)
					vertical = -1;
				else
					vertical = 0;
			}
			yield return new WaitForSeconds (1f);
		}
		else
			vertical = 0.0f;
	}

	void LateUpdate() {
		dir = Vector3.zero;
		dir.x = -Input.acceleration.y;
		//print ("lateupdate vertical: " + vertical);
	}

	void Update() {
		WheelFLTrans.Rotate(WheelFL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
		WheelFRTrans.Rotate(WheelFR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
		WheelRLTrans.Rotate(WheelRL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
		WheelRRTrans.Rotate(WheelRR.rpm / 60 * 360 * Time.deltaTime, 0, 0);

		Vector3 calc = WheelFLTrans.localEulerAngles;
		calc.y = (2*WheelFL.steerAngle) - WheelFLTrans.localEulerAngles.z;
		WheelFLTrans.localEulerAngles = calc;

		calc = WheelFRTrans.localEulerAngles;
		calc.y = (2*WheelFR.steerAngle) - WheelFRTrans.localEulerAngles.z;
		WheelFRTrans.localEulerAngles = calc;

		if (BackLightObject != null)
			BackLight();
		WheelPosition();
		EngineSound();
	}

	#region Control
	void Control() {
		currentSpeed = 2 * Mathf.PI * WheelRL.radius * WheelRL.rpm * 6/100;// 60 / 1000
		currentSpeed = Mathf.Round(currentSpeed);

		if (currentSpeed < topSpeed && currentSpeed > -maxReverseSpeed && !braked)
		{
			WheelRR.motorTorque = maxTorque * 1;//Input.GetAxis("Vertical");
			WheelRL.motorTorque = maxTorque * 1;//Input.GetAxis("Vertical");
		}
		else
		{
			WheelRR.motorTorque = 0;
			WheelRL.motorTorque = 0;
		}

		//if (Input.GetButton("Vertical") == false)
	/*	if (!Input.GetMouseButton(0))
		{
			WheelRR.brakeTorque = decelerationSpeed;
			WheelRL.brakeTorque = decelerationSpeed;
		}
		else
		{
			WheelRL.brakeTorque = 0;
			WheelRR.brakeTorque = 0;
		}*/
		
		float speedFactor = rigidbody.velocity.magnitude / lowestSteerAtSpeed;
		float currentSpeedAngle = Mathf.Lerp(lowSpeedSteerAngle, highSpeedSteerAngle, speedFactor);
		//currentSpeedAngle *= Input.GetAxis ("Horizontal");
		currentSpeedAngle *= dir.x;
		
		WheelFL.steerAngle = currentSpeedAngle;
		WheelFR.steerAngle = currentSpeedAngle;
	}
	#endregion

	void BackLight() {
		//if (currentSpeed > 0 && Input.GetAxis ("Vertical") < 0 && !braked)
		if (currentSpeed > 0 && vertical < 0 && !braked)
			BackLightObject.renderer.material = BrakeLightMaterial;
		//else if (currentSpeed < 0 && Input.GetAxis ("Vertical") > 0 && !braked)
		else if (currentSpeed < 0 && vertical > 0 && !braked)
			BackLightObject.renderer.material = BrakeLightMaterial;
		//else if (currentSpeed < 0 && Input.GetAxis ("Vertical") < 0 && !braked)
		else if (currentSpeed < 0 && vertical < 0 && !braked)
			BackLightObject.renderer.material = ReverseLightMaterial;
		else if (!braked)
			BackLightObject.renderer.material = IdleLightMaterial;
	}

	void WheelPosition() {
		RaycastHit hit;
		Vector3 wheelPos;

		if (Physics.Raycast(WheelFL.transform.position, -WheelFL.transform.up, out hit, WheelFL.radius + WheelFL.suspensionDistance))
			wheelPos = hit.point + WheelFL.transform.up * WheelFL.radius;
		else
			wheelPos = WheelFL.transform.position - WheelFL.transform.up * WheelFL.suspensionDistance;
		WheelFLTrans.position = wheelPos;

		if (Physics.Raycast(WheelFR.transform.position, -WheelFR.transform.up, out hit, WheelFR.radius + WheelFR.suspensionDistance))
			wheelPos = hit.point + WheelFR.transform.up * WheelFR.radius;
		else
			wheelPos = WheelFR.transform.position - WheelFR.transform.up * WheelFR.suspensionDistance;
		WheelFRTrans.position = wheelPos;

		if (Physics.Raycast(WheelRL.transform.position, -WheelRL.transform.up, out hit, WheelRL.radius + WheelRL.suspensionDistance))
			wheelPos = hit.point + WheelRL.transform.up * WheelRL.radius;
		else
			wheelPos = WheelRL.transform.position - WheelRL.transform.up * WheelRL.suspensionDistance;
		WheelRLTrans.position = wheelPos;

		if (Physics.Raycast(WheelRR.transform.position, -WheelRR.transform.up, out hit, WheelRR.radius + WheelRR.suspensionDistance))
			wheelPos = hit.point + WheelRR.transform.up * WheelRR.radius;
		else
			wheelPos = WheelRR.transform.position - WheelRR.transform.up * WheelRR.suspensionDistance;
		WheelRRTrans.position = wheelPos;
	}

	#region Drift
	void HandBrake() {
		//if (Input.GetButton ("Jump"))
		if (Input.GetMouseButton(0))
		{
			if (ev.currentSelectedGameObject != null)
			{
				if (ev.currentSelectedGameObject.name != handBrakeButton.name)
				{
					braked = true;
					print(ev.currentSelectedGameObject.name);
				}
			}
		}
		else
			braked = false;

		if (braked)
		{
			WheelFR.brakeTorque = maxBrakeTorque;
			WheelFL.brakeTorque = maxBrakeTorque;
			WheelRL.motorTorque = 0;
			WheelRR.motorTorque = 0;

			if (rigidbody.velocity.magnitude > 1)
				SetSlip(slipForwardFriction, slipSidewayFriction);
			else
				SetSlip(1, 1);
			if (currentSpeed < 1 && currentSpeed > -1)
				BackLightObject.renderer.material = IdleLightMaterial;
			else
				BackLightObject.renderer.material = BrakeLightMaterial;
		}
		else
		{
			WheelFR.brakeTorque = 0;
			WheelFL.brakeTorque = 0;
			SetSlip(myForwardFriction, mySidewayFriction);
		}
	}

	void SetValues() {
		myForwardFriction = WheelRR.forwardFriction.stiffness;
		mySidewayFriction = WheelRR.sidewaysFriction.stiffness;
		slipForwardFriction = 0.04f;
		slipSidewayFriction = 0.08f;
	}

	void SetSlip(float currentForwardFriction, float currentSidewayFriction) {
		var curve = WheelRR.forwardFriction;
		curve.stiffness = currentForwardFriction;
		WheelRR.forwardFriction = curve;

		curve = WheelRL.forwardFriction;
		curve.stiffness = currentForwardFriction;
		WheelRL.forwardFriction = curve;

		/* ------------------------------------------------------------------------------------------------ */
		curve = WheelRR.sidewaysFriction;
		curve.stiffness = currentSidewayFriction;
		WheelRR.sidewaysFriction = curve;

		curve = WheelRL.sidewaysFriction;
		curve.stiffness = currentSidewayFriction;
		WheelRL.sidewaysFriction = curve;
	}
	#endregion

	void EngineSound() {
		int i = 0;
		for (; i < gearRatio.Length; i++)
		{
			if (gearRatio[i] > currentSpeed)
				break;
		}
		float gearMinValue;
		float gearMaxValue = gearRatio[i];
		if (i == 0)
			gearMinValue = 0.0f;
		else
			gearMinValue = gearRatio[i - 1];

		float enginePitch = (currentSpeed - gearMinValue) / (gearMaxValue - gearMinValue);
		if (currentSpeed < 5 && currentSpeed > -5)
			audio.pitch = 0.5f;
		else
			audio.pitch = enginePitch + 1f;
	}

	void OnGUI() {
		if (topSpeed == 0)
		{
			GUIStyle style = new GUIStyle ();
			style.fontSize = 20;
			style.normal.textColor = Color.yellow;
			int yet = 3 - (int) (Time.realtimeSinceStartup - oldTime);
			if (yet < 1)
				topSpeed = theMaxSpeed;
			else
			{
				GUI.Label(new Rect((Screen.width / 2) - 55, (Screen.height / 2) - 100, 50, 50), "Get Ready !", style);
				GUI.Label(new Rect((Screen.width / 2) - 15, (Screen.height / 2) - 55, 20, 50), yet.ToString(), style);
			}
		}
	}
}