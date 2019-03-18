using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour {

    public BoxCollider triggerCollider;
    Rigidbody rb;
    public float killVelocity;

	// Use this for initialization
	void Start () {
        triggerCollider.isTrigger = true;
        rb = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.tag + " collided");

            ToggleRagdoll ragdoll = other.gameObject.GetComponent<ToggleRagdoll>();
            Vector3 velocity = rb.velocity;
            CharacterController otherCharacterController = other.gameObject.GetComponent<CharacterController>();
            Vector3 otherVelocity = otherCharacterController.velocity;

            float relativeVelocity = (velocity - otherVelocity).magnitude;
            Debug.Log("Relative velocity " + relativeVelocity);
            if (ragdoll != null && !ragdoll.enableRagdoll)
            {
                if (relativeVelocity > killVelocity)
                {
                    ragdoll.enableRagdoll = true;
                }
            }

            //Rigidbody otherRigidBody = other.gameObject.GetComponent<Rigidbody>();
            //otherRigidBody.AddForce(transform.up * testForce);
            //near.Add(other.gameObject.GetComponent<Communication>());

            //Highlight object on
            //Outline outline = other.gameObject.GetComponent<Outline>();
            //outline.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.tag + " stopped colliding");
            //near.Remove(other.gameObject.GetComponent<Communication>());

            //Highlight object off
            //Outline outline = other.gameObject.GetComponent<Outline>();
            //outline.enabled = false;
        }
    }
}
