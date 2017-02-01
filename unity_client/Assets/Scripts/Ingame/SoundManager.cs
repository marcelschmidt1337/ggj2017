using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource sound;

	// Use this for initialization
	void Start () {
        sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider stream1)
    {
        if (stream1.tag == "Water")
            sound.Play();
    }

    /*void OnTriggerStay(Collider stream2)
    {
        if (stream2.tag == "Water")
            sound.Play();
    }*/
}
