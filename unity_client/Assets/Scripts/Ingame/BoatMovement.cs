using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Boat))]
public class BoatMovement : MonoBehaviour
{
	public List<float> boatDirection;
	public float angularVelocityMultiplier = 0.01f;
	public float constantStream = 1.0f;
	public float streamRotationStrength = 0.0001f;


	public void RowLeft(float force) {
		GetComponent<Rigidbody>().AddForce( transform.forward * force );
		GetComponent<Rigidbody>().angularVelocity -= new Vector3( 0.0f, force * angularVelocityMultiplier, 0.0f );
	}

	public void RowRight(float force) {
		GetComponent<Rigidbody>().AddForce( transform.forward * force );
		GetComponent<Rigidbody>().angularVelocity += new Vector3( 0.0f, force * angularVelocityMultiplier, 0.0f );
	}

	void FixedUpdate () {
		var rigidBody = GetComponent<Rigidbody>();
		rigidBody.AddForce( 0.0f, 0.0f, constantStream );
		Vector3 angularVelocity = rigidBody.angularVelocity;
		transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.identity, streamRotationStrength );
	}
}
