using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour {

	int i;
	int j;

	public event Action DoIt;

	void Update () {
		DoIt -= () => { Debug.Log( j ); j++; };
		DoIt += () => { Debug.Log( j ); j++; };
		DoIt();
		i++;
	}
}
