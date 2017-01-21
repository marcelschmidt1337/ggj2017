using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float boatForce;
    public List<float> boatDirection;
    Rigidbody rb;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if (Input.GetKeyDown(KeyCode.L))
        {
            rb.AddForce(boatDirection[0], 0, 6 * boatForce);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(boatDirection[1], 0, 6 * boatForce);
        }
	}
}
