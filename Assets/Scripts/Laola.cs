using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laola : MonoBehaviour {

	public List<LaolaParticipant> Participants;
	private int nextOne = 0;
	private bool isStarting = true;
	private bool startAgain = false; 
	
	// Update is called once per frame
	void Start () {
		StartCoroutine(Do()); 
	}

	private IEnumerator Do()
	{
		yield return new WaitForSeconds(0.8f);
		Participants[nextOne].StartLaola();
		if (nextOne < Participants.Count - 1 && isStarting) {
			nextOne++;
			StartCoroutine(Do());
		}
		else {
			isStarting = false;
		}
		
	}

	void Update() {
		if (!isStarting) 
		{
			isStarting = false; 
			StartCoroutine(DontDo()); 
		}
		if (startAgain) {
			nextOne = 0; 
			isStarting = true;
			startAgain = false;
			StartCoroutine(Do()); 
		}
	}
	private IEnumerator DontDo()
	{
		yield return new WaitForSeconds(0.8f);
		Participants[nextOne].StopLaola();
		if (nextOne >0) {
			nextOne--;
			StartCoroutine(DontDo());
		}
		else {
			startAgain = true;
		}

	}
}
