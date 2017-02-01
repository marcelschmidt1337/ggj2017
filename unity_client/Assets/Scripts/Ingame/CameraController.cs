using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject boats;
	public float minPos;
	public float maxPos;
    Vector3 offset;


	// Use this for initialization
	void Start ()
    {
        offset = transform.position - boats.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
		Vector3 targetPosition = (boats.transform.position + offset);
		targetPosition.x = Mathf.Clamp( targetPosition.x, minPos, maxPos );
		transform.position = targetPosition;
	}
}
