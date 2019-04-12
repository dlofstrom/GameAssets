using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitCable : MonoBehaviour {
		GameObject start;
		GameObject end;
		ConfigurableJoint startJoint;
		ConfigurableJoint endJoint;
		public float jointAngle;
		
	// Use this for initialization
	void Start () {
		if (start == null) start = transform.Find("Start").gameObject;
		if (end == null) end = transform.Find("End").gameObject;

		//Temporary disable rotation of start and end
		Vector3 startRotation = start.transform.eulerAngles;
		Vector3 endRotation = end.transform.eulerAngles;
		start.transform.eulerAngles = Vector3.zero;
		end.transform.eulerAngles = Vector3.right*180f;

		//TODO: Hardcoded values
		//Get Armature
		GameObject armature = transform.Find("Armature").gameObject;
		//Get first bone connect to parent, connect parent to anchor 1
		Transform boneTransform = armature.transform.Find("Bone");
		GameObject bone = boneTransform.gameObject;
		GameObject lastBone;
		Rigidbody lastBoneRB;
		//Init bone root
		Rigidbody boneRB = bone.AddComponent<Rigidbody>();
		boneRB.inertiaTensor = new Vector3(1,1,1);
		boneRB.mass = 0.1f;
		boneRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
		CapsuleCollider boneCC = bone.AddComponent<CapsuleCollider>();
		boneCC.radius = 0.0005f;
		boneCC.height = 0.002f;
		boneCC.center = Vector3.up*0.002f/2;
		ConfigurableJoint boneJoint = bone.AddComponent<ConfigurableJoint>();
		boneJoint.anchor = Vector3.zero;
		boneJoint.autoConfigureConnectedAnchor = false;
		boneJoint.connectedBody = start.GetComponent<Rigidbody>();
		boneJoint.connectedAnchor = Vector3.up*0.004f;
		boneJoint.xMotion = ConfigurableJointMotion.Limited;
		boneJoint.yMotion = ConfigurableJointMotion.Limited;
		boneJoint.zMotion = ConfigurableJointMotion.Limited;
		boneJoint.angularXMotion = ConfigurableJointMotion.Limited;
		boneJoint.angularYMotion = ConfigurableJointMotion.Locked;
		boneJoint.angularZMotion = ConfigurableJointMotion.Limited;
		SoftJointLimit l = boneJoint.lowAngularXLimit;
		l.limit = -jointAngle;
		boneJoint.lowAngularXLimit = l;
		l = boneJoint.highAngularXLimit;
		l.limit = jointAngle;
		boneJoint.highAngularXLimit = l;
		l = boneJoint.angularZLimit;
		l.limit = jointAngle;
		boneJoint.angularZLimit = l;
		startJoint = boneJoint;

		//for (int temp = 0; temp < 3; temp++)
		while(boneTransform != null)
		{
			lastBone = bone;
			lastBoneRB = boneRB;
			for (int i = 0; i <= 120; i++)
			{
				boneTransform = bone.transform.Find("Bone."+i.ToString().PadLeft(3,'0'));
				//Get child bone
				if (boneTransform != null)
				{
					bone = boneTransform.gameObject;
					//Set parent
					bone.transform.parent = armature.transform;
					//Debug.Log("Found bone: " + "Bone."+i.ToString().PadLeft(3,'0'));
					//TOOD: init bone
					boneRB = bone.AddComponent<Rigidbody>();
					boneRB.inertiaTensor = new Vector3(1,1,1);
					boneRB.mass = 0.1f;
					boneRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
					boneCC = bone.AddComponent<CapsuleCollider>();
					boneCC.radius = 0.0005f;
					boneCC.height = 0.002f;
					boneCC.center = Vector3.up*0.002f/2;
					//Addjoint to last bone (hierarchy)
					boneJoint = bone.AddComponent<ConfigurableJoint>();
					boneJoint.anchor = Vector3.zero;
					boneJoint.autoConfigureConnectedAnchor = false;
					boneJoint.connectedBody = lastBoneRB;
					boneJoint.connectedAnchor = Vector3.up*0.004f;
					boneJoint.xMotion = ConfigurableJointMotion.Limited;
					boneJoint.yMotion = ConfigurableJointMotion.Limited;
					boneJoint.zMotion = ConfigurableJointMotion.Limited;
					boneJoint.angularXMotion = ConfigurableJointMotion.Limited;
					boneJoint.angularYMotion = ConfigurableJointMotion.Locked;
					boneJoint.angularZMotion = ConfigurableJointMotion.Limited;
					l = boneJoint.lowAngularXLimit;
					l.limit = -jointAngle;
					boneJoint.lowAngularXLimit = l;
					l = boneJoint.highAngularXLimit;
					l.limit = jointAngle;
					boneJoint.highAngularXLimit = l;
					l = boneJoint.angularZLimit;
					l.limit = jointAngle;
					boneJoint.angularZLimit = l;
					break;
				}
			}
		}
		//TODO:connect last bone to anchor 2
		boneJoint = end.AddComponent<ConfigurableJoint>();
		boneJoint.anchor = Vector3.zero;
		boneJoint.autoConfigureConnectedAnchor = false;
		boneJoint.connectedBody = boneRB;
		boneJoint.connectedAnchor = Vector3.up*0.004f;
		boneJoint.xMotion = ConfigurableJointMotion.Limited;
		boneJoint.yMotion = ConfigurableJointMotion.Limited;
		boneJoint.zMotion = ConfigurableJointMotion.Limited;
		boneJoint.angularXMotion = ConfigurableJointMotion.Limited;
		boneJoint.angularYMotion = ConfigurableJointMotion.Locked;
		boneJoint.angularZMotion = ConfigurableJointMotion.Limited;
		l = boneJoint.lowAngularXLimit;
		l.limit = -jointAngle;
		boneJoint.lowAngularXLimit = l;
		l = boneJoint.highAngularXLimit;
		l.limit = jointAngle;
		boneJoint.highAngularXLimit = l;
		l = boneJoint.angularZLimit;
		l.limit = jointAngle;
		boneJoint.angularZLimit = l;
		endJoint = boneJoint;

		//Reset rotation of start and end
		start.transform.eulerAngles = startRotation;
		end.transform.eulerAngles = endRotation;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Connect(GameObject s, GameObject e)
	{
		if (start == null) start = transform.Find("Start").gameObject;
		if (end == null) end = transform.Find("End").gameObject;
		Transform t = s.transform;
		if (t.Find("Connector") != null) t = t.Find("Connector");
		start.transform.parent = t;
		start.transform.localPosition = Vector3.zero;
		start.transform.localRotation = Quaternion.identity;
		t = e.transform;
		if (t.Find("Connector") != null) t = t.Find("Connector");
		end.transform.parent = t;
		end.transform.localPosition = Vector3.zero;
		end.transform.localRotation = Quaternion.identity;
	}

	void OnDestroy()
    {
        Destroy(start);
		Destroy(end);
    }
}
