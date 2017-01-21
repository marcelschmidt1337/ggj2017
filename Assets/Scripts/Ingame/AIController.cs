using UnityEngine;

[RequireComponent( typeof( BoatMovement ) )]
public class AIController : MonoBehaviour
{
	public Vector2 RowLeftMinMaxTime;
	public Vector2 RowRightMinMaxTime;

	float TimeUntilNextLeftRow;
	float TimeUntilNextRightRow;

	void FixedUpdate () {

		TimeUntilNextLeftRow -= Time.fixedDeltaTime;
		TimeUntilNextRightRow -= Time.fixedDeltaTime;

		if(TimeUntilNextLeftRow < Mathf.Epsilon) {
			throw new System.NotImplementedException();
			TimeUntilNextLeftRow += Random.Range( RowLeftMinMaxTime.x, RowLeftMinMaxTime.y );
		}

		if (TimeUntilNextRightRow < Mathf.Epsilon) {
			throw new System.NotImplementedException();
			TimeUntilNextRightRow += Random.Range( RowRightMinMaxTime.x, RowRightMinMaxTime.y );
		}
	}
}
