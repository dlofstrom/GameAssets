﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Communication : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string debug() {
        Debug.Log("I AM AN OBJECT!");
        return "And this is my return value";
    }
}
