using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowingView : MonoBehaviour {

    public Animator Animator;

	float duration = 0.0f;
    private const float AnimationLength = 1.292f;
	// Use this for initialization
	public void StartRowing () {
		ResetTrigger(); 
		Animator.SetTrigger("StartRowing");
		
		StartCoroutine( Co_WaitForAnimationFinish() );
	}
	
	// Update is called once per frame
	public void StopRowing () {
		ResetTrigger(); 
        Animator.SetTrigger("StopRowing");
	}

    public void SetAnimationDuration(float duration)
    {
		this.duration = duration;

		Animator.speed =  AnimationLength / duration ;
    }

	private IEnumerator Co_WaitForAnimationFinish () {
		yield return new WaitForSeconds( this.duration );
		StopRowing();
	}

	private void ResetTrigger() {
		Animator.ResetTrigger("StopRowing");
		Animator.ResetTrigger("StartRowing");
	}
}
