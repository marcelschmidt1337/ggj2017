using UnityEngine;

[RequireComponent(typeof(BoatMovement))]
public class KeyboardController : MonoBehaviour
{
	public KeyCode RowRightKey = KeyCode.L;
	public KeyCode RowLeftKey = KeyCode.A;

	void FixedUpdate () {
		/*
		if (Input.GetKeyDown( this.RowLeftKey )) {
			GetComponent<BoatMovement>().RowLeft();
		}

		if (Input.GetKeyDown(this.RowRightKey)) {
			GetComponent<BoatMovement>().RowRight();
		}*/
	}
}
