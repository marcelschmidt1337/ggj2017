using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowingView : MonoBehaviour {

    public Animator Animator;

	public int Id; 
    private const float AnimationLength = 1.292f;
	// Use this for initialization
	public void StartRowing () {
        Animator.SetTrigger("StartRowing");
	}
	
	// Update is called once per frame
	public void StopRowing () {
        Animator.SetTrigger("StopRowing");
	}

    public void SetAnimationDuration(float duration)
    {
        Animator.speed =  AnimationLength / duration ;
    }
}
