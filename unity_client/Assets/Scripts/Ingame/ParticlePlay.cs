using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlay : MonoBehaviour {

    public ParticleSystem waterRow;

    // Use this for initialization
    void Start ()
    {
        waterRow = GetComponent<ParticleSystem>();
        //waterRow = Pa
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider stream1)
    {
        if (stream1.tag == "Water")
            waterRow.Play();
    }

    void OnTriggerStay(Collider stream2)
    {
        if (stream2.tag == "Water")
        {
            waterRow.Play();
        }
    }

    void OnTriggerExit(Collider stream3)
    {
        if (stream3.tag == "Water")
            waterRow.Stop();
    }
}
