using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaolaParticipant : MonoBehaviour {

	public Animator Animator;
	public void StartLaola() {
		this.Animator = gameObject.GetComponent<Animator>(); 
		this.Animator.SetTrigger("StartRowing"); 
	}

	public void StopLaola() {
		this.Animator = gameObject.GetComponent<Animator>();
		this.Animator.SetTrigger("StopRowing");
	}

}
