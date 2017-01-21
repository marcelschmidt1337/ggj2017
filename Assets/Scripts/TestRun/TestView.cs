using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class TestView : NetworkManager {

	// Use this for initialization
	void Start () {
        client = StartClient();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
