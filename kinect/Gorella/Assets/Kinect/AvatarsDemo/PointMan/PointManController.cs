using UnityEngine;
using System;
using System.Collections;

public class PointManController : MonoBehaviour 
{
    #region Attributes
	public bool MoveVertically = false;
	public bool MirroredMovement = false;

	//public GameObject debugText;
	
	public GameObject Hip_Center;
	public GameObject Spine;
	public GameObject Neck;
	public GameObject Head;
	public GameObject Shoulder_Left;
	public GameObject Elbow_Left;
	public GameObject Wrist_Left;
	public GameObject Hand_Left;
	public GameObject Shoulder_Right;
	public GameObject Elbow_Right;
	public GameObject Wrist_Right;
	public GameObject Hand_Right;
	public GameObject Hip_Left;
	public GameObject Knee_Left;
	public GameObject Ankle_Left;
	public GameObject Foot_Left;
	public GameObject Hip_Right;
	public GameObject Knee_Right;
	public GameObject Ankle_Right;
	public GameObject Foot_Right;
	public GameObject Spine_Shoulder;
    public GameObject Hand_Tip_Left;
    public GameObject Thumb_Left;
    public GameObject Hand_Tip_Right;
    public GameObject Thumb_Right;
	
	public LineRenderer LinePrefab;
	
	private GameObject[] bones;
	private LineRenderer[] lines;
	
	private Vector3 initialPosition;
	private Quaternion initialRotation;
	private Vector3 initialPosOffset = Vector3.zero;
	private Int64 initialPosUserID = 0;

    private float minMovementDist = 0.05f;
    private float maxMovementTime = 0.1f;
    private float MovementStartTime = 0.0f;
    private Vector3 MovementStartPos = Vector3.zero;
    private Vector3 oldPos = Vector3.zero;

    public static int State = 0;

   #endregion

    #region Initialisation
    void Start () 
	{
        oldPos = transform.position;
		//store bones in a list for easier access
		bones = new GameObject[] {
			Hip_Center,
            Spine,
            Neck,
            Head,
            Shoulder_Left,
            Elbow_Left,
            Wrist_Left,
            Hand_Left,
            Shoulder_Right,
            Elbow_Right,
            Wrist_Right,
            Hand_Right,
            Hip_Left,
            Knee_Left,
            Ankle_Left,
            Foot_Left,
            Hip_Right,
            Knee_Right,
            Ankle_Right,
            Foot_Right,
            Spine_Shoulder,
            Hand_Tip_Left,
            Thumb_Left,
            Hand_Tip_Right,
            Thumb_Right
		};
		
		// array holding the skeleton lines
		lines = new LineRenderer[bones.Length];
        GameObject Skeleton = new GameObject("Skeleton");
		if(LinePrefab)
		{
			for(int i = 0; i < lines.Length; i++)
			{
				lines[i] = Instantiate(LinePrefab) as LineRenderer;
                lines[i].name = "SkeletonLine["+i+"]";
                lines[i].transform.parent = Skeleton.transform;
            }
		}
		
		initialPosition = transform.position;
		initialRotation = transform.rotation;
	}
    #endregion

    IEnumerator WaitForLunch()
    {
        MovementStartTime = Time.time;
        MovementStartPos = transform.position;
        yield return new WaitForSeconds(0.1f);
        float gestureTime = Time.time - MovementStartTime;
        float gestureDist = (transform.position - MovementStartPos).magnitude;
        if (gestureTime < maxMovementTime && gestureDist > minMovementDist)
        {
            Vector2 direction = transform.position - MovementStartPos;
            Vector2 MovementType = Vector2.zero;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                MovementType = Vector2.right * Mathf.Sign(direction.x);
            else
                MovementType = Vector2.up * Mathf.Sign(direction.y);

                if (MovementType.x != 0)
                {
                    if (MovementType.x != 0.0f)
                    {
                        if (MovementType.x > 0.0f)
                        {
                            State = 1;
                            print("RIGHT");
                        }
                        else
                        {
                            print("LEFT");
                            State = 2;
                        }
                    }
                }
                else if (MovementType.y > 0.0f)
                {
                    State = 3;
                    print("Y");
                }
                else
                    State = 0;
        }
        oldPos = transform.position;
    }

    #region Body
    void Update () 
	{

        if (transform.position != oldPos)
            StartCoroutine("WaitForLunch");

		KinectManager manager = KinectManager.Instance;
		
		// get 1st player
		Int64 userID = manager ? manager.GetPrimaryUser() : 0;
		
		if(userID <= 0)
		{
			// reset the pointman position and rotation
			if(transform.position != initialPosition)
				transform.position = initialPosition;
			
			if(transform.rotation != initialRotation)
				transform.rotation = initialRotation;

			for(int i = 0; i < bones.Length; i++) 
			{
				bones[i].gameObject.SetActive(true);

				bones[i].transform.localPosition = Vector3.zero;
				bones[i].transform.localRotation = Quaternion.identity;
				
			    if(LinePrefab)
				{
					lines[i].gameObject.SetActive(false);
				}
			}
			
			return;
		}
		
		// set the position in space
		Vector3 posPointMan = manager.GetUserPosition(userID);
		posPointMan.z = !MirroredMovement ? -posPointMan.z : posPointMan.z;
		
		// store the initial position
		if(initialPosUserID != userID)
		{
			initialPosUserID = userID;
			initialPosOffset = transform.position - (MoveVertically ? posPointMan : new Vector3(posPointMan.x, 0, posPointMan.z));
		}
		
		transform.position = initialPosOffset + (MoveVertically ? posPointMan : new Vector3(posPointMan.x, 0, posPointMan.z));
		
		// update the local positions of the bones
		for(int i = 0; i < bones.Length; i++) 
		{
			if(bones[i] != null)
			{
				int joint = !MirroredMovement ? i : (int)KinectWrapper.GetMirrorJoint((KinectWrapper.JointType)i);
				
				if(manager.IsJointTracked(userID, joint))
				{
					bones[i].gameObject.SetActive(true);
					
					Vector3 posJoint = manager.GetJointPosition(userID, joint);
					posJoint.z = !MirroredMovement ? -posJoint.z : posJoint.z;
					
					Quaternion rotJoint = manager.GetJointOrientation(userID, joint, !MirroredMovement);
					
					posJoint -= posPointMan;
					
					if(MirroredMovement)
					{
						posJoint.x = -posJoint.x;
						posJoint.z = -posJoint.z;
					}

					bones[i].transform.localPosition = posJoint;
					bones[i].transform.localRotation = rotJoint;
					
					if(LinePrefab)
					{
						lines[i].gameObject.SetActive(true);
						Vector3 posJoint2 = bones[i].transform.position;
						
						Vector3 dirFromParent = manager.GetJointDirection(userID, joint);
						dirFromParent.z = !MirroredMovement ? -dirFromParent.z : dirFromParent.z;
						Vector3 posParent = posJoint2 - dirFromParent;
						
						//lines[i].SetVertexCount(2);
						lines[i].SetPosition(0, posParent);
						lines[i].SetPosition(1, posJoint2);
					}
				}
				else
				{
					bones[i].gameObject.SetActive(false);
					
					if(LinePrefab)
					{
						lines[i].gameObject.SetActive(false);
					}
				}
			}	
		}
    }
    #endregion

}