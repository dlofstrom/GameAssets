using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearObjects : MonoBehaviour {

    public SphereCollider sphereCollider;
    public float testForce;
    private List<Communication> near = new List<Communication>();


    // Use this for initialization
    void Start () {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Part")
        {
            Debug.Log(other.tag + " collided");
            Rigidbody otherRigidBody = other.gameObject.GetComponent<Rigidbody>();
            otherRigidBody.AddForce(transform.up * testForce);
            near.Add(other.gameObject.GetComponent<Communication>());

            //Highlight object on
            Outline outline = other.gameObject.GetComponent<Outline>();
            outline.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Part")
        {
            Debug.Log(other.tag + " stopped colliding");
            near.Remove(other.gameObject.GetComponent<Communication>());

            //Highlight object off
            Outline outline = other.gameObject.GetComponent<Outline>();
            outline.enabled = false;
        }
    }

    public IList<Communication> getNear() {
        return near.AsReadOnly();
    }
}
