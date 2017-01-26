using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoRoutineProvider : MonoBehaviour {

	public static CoRoutineProvider Instance { get; private set;  }
	void Awake() {
		Instance = this; 
	}

	void OnDestroy() {
		Instance = null; 
	}
}
