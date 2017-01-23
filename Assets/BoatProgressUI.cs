using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatProgressUI : MonoBehaviour {

	public RectTransform firstBoatTransform;
	public RectTransform secondBoatTransform;

	public Boat firstBoat;
	public Boat secondBoat;
	public Transform goalTransform;

	Vector3 firstBoatStartPosition;
	Vector3 secondBoatStartPosition;
	Vector3 goalPosition;

	void Start () {
		this.goalPosition = goalTransform.position;
		this.firstBoatStartPosition = firstBoat.transform.position;
		this.secondBoatStartPosition = secondBoat.transform.position;
	}

	void Update () {
		float firstBoatX = 1.0f - ( firstBoat.transform.position.z - goalPosition.z ) / ( firstBoatStartPosition.z - goalPosition.z );
		this.firstBoatTransform.anchorMin = new Vector2( firstBoatX, this.firstBoatTransform.anchorMin.y );
		this.firstBoatTransform.anchorMax = new Vector2( firstBoatX, this.firstBoatTransform.anchorMin.y );

		float secondBoatX = 1.0f - (secondBoat.transform.position.z - goalPosition.z) / (secondBoatStartPosition.z - goalPosition.z);
		this.secondBoatTransform.anchorMin = new Vector2( secondBoatX, this.secondBoatTransform.anchorMin.y );
		this.secondBoatTransform.anchorMax = new Vector2( secondBoatX, this.secondBoatTransform.anchorMin.y );
	}
}
