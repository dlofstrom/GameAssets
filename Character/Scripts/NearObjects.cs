using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearObjects : MonoBehaviour {

    public SphereCollider sphereCollider;
    public float testForce;
    private List<GameObject> near = new List<GameObject>();
    private ComPorts comPorts;

    // Use this for initialization
    void Start () {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        comPorts = GetComponent<ComPorts>();
	}
	
	// Update is called once per frame
	void Update () {
	}

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

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Part")
        {
            ComPort comPort = other.gameObject.GetComponent<ComPort>();
            if (comPort == null) return;

            if (comPorts != null)
            {
                if (!comPorts.IsConnected(comPort))
                {
                    Debug.Log(other.tag + " stopped colliding");
                    near.Remove(other.gameObject);

                    //Highlight object off
                    Outline outline = other.gameObject.GetComponent<Outline>();
                    outline.enabled = false;
                }
            }
        }
    }

    public void ConnectNear() {
        if (near.Count > 0 && comPorts != null)
        {
            ComPort cp = near[0].GetComponent<ComPort>();
            Outline outline = near[0].GetComponent<Outline>();
            if (cp != null)
            {
                comPorts.ConnectPort(cp);
                outline.OutlineColor = Color.green;
                near.RemoveAt(0);
            }
        }
    }
}
