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
		float firstBoatX = 1.0f - Vector3.Distance( firstBoat.transform.position, goalPosition ) / Vector3.Distance( firstBoatStartPosition, goalPosition );
		this.firstBoatTransform.anchorMin = new Vector2( firstBoatX, this.firstBoatTransform.anchorMin.y );
		this.firstBoatTransform.anchorMax = new Vector2( firstBoatX, this.firstBoatTransform.anchorMin.y );

		float secondBoatX = 1.0f - Vector3.Distance( secondBoat.transform.position, goalPosition ) / Vector3.Distance( secondBoatStartPosition, goalPosition );
		this.secondBoatTransform.anchorMin = new Vector2( secondBoatX, this.secondBoatTransform.anchorMin.y );
		this.secondBoatTransform.anchorMax = new Vector2( secondBoatX, this.secondBoatTransform.anchorMin.y );
	}
}
