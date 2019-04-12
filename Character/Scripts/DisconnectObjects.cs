using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisconnectObjects : MonoBehaviour {
    private ComPorts comPorts;

	// Use this for initialization
	void Start () {
        comPorts = GetComponentInParent<ComPorts>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	/*
	private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Part")
        {
            ComPort comPort = other.gameObject.GetComponent<ComPort>();
            if (comPort == null) return;

            if (comPorts != null)
            {
                if (!comPorts.IsConnected(comPort))
                {
                    //Debug.Log(other.tag + " collided");
                    //Rigidbody otherRigidBody = other.gameObject.GetComponent<Rigidbody>();
                    //otherRigidBody.AddForce(transform.up * testForce);
                    near.Add(other.gameObject);

                    //Highlight object on
                    Outline outline = other.gameObject.GetComponent<Outline>();
                    outline.OutlineColor = Color.blue;
                    outline.enabled = true;
                }
            }
        }
    }
	*/
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Part")
        {
            ComPort comPort = other.gameObject.GetComponent<ComPort>();
            if (comPort == null) return;

            if (comPorts != null)
            {
                if (comPorts.IsConnected(comPort))
                {
                    Debug.Log(other.tag + " cable broke");
                    comPorts.DisconnectPort(comPort);
					comPorts.DisconnectCable(comPort);

                    //Highlight object off
                    Outline outline = other.gameObject.GetComponent<Outline>();
                    outline.enabled = false;
                }
            }
        }
    }
}
