using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour {

	public GameObject Boat1;
	public GameObject Boat2;

	void Update() {
		if (Boat1.gameObject.transform.position.z < Boat2.gameObject.transform.position.z) {
			var pos = transform.position;
			pos.z = Boat1.gameObject.transform.position.z;
			transform.position = pos;
		}
		else {
			var pos = transform.position;
			pos.z = Boat2.gameObject.transform.position.z;
			transform.position = pos;
			
		}
	}
}
