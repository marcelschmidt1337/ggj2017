using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boat))]
public class Respawn : MonoBehaviour {

	public float resetPositionX;
	public float RespawnTime;
	float InactiveTime;

	public bool hackRespawn = false;

	bool IsInactive () {
		var angle = Vector3.Angle( transform.forward, Vector3.forward );
		return angle > 80.0f;
	}

	void FixedUpdate () {
		if (IsInactive()) {
			InactiveTime += Time.fixedDeltaTime;
			if(InactiveTime > RespawnTime) {
				DoRespawn();
			}
		} else {
			InactiveTime = 0.0f;
		}

		if (hackRespawn) {
			DoRespawn();
			hackRespawn = false;
		}
	}

	void DoRespawn() {
		var position = transform.position;
		position.x = resetPositionX;
		transform.position = position;
		transform.rotation = Quaternion.identity;
		var rigidbody = GetComponent<Rigidbody>();
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		InactiveTime = 0.0f;
	}
}
