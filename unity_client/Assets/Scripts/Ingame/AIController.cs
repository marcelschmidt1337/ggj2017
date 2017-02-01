using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( Boat ) )]
public class AIController : MonoBehaviour
{
	public Vector2 RowMinMaxTime;

	public int minPlayer = 1;
	public int maxPlayer = 4;
	public int group;

	class RowTiming
	{
		public Player Player;
		public float TimeUmtilNextRowing;
	}

	List<RowTiming> rowTimings = new List<RowTiming>();

	void Start () {
		Random.InitState( System.DateTime.UtcNow.Millisecond );
		Boat boat = GetComponent<Boat>();

		List<Player> state = new List<Player>();

		int leftPlayerCount = Random.Range( minPlayer, maxPlayer );
		int rightPlayerCount = Random.Range( minPlayer, maxPlayer );
		for (int i = 0; i < leftPlayerCount; i++) {
			var player = new Player {
				Id = i,
				Group = group,
				Side = 0
			};
			state.Add( player );
			boat.LeftSide.Players.Add( player );
			this.rowTimings.Add( new RowTiming {
				Player = player
			} );
		}

		for (int i = 0; i < rightPlayerCount; i++) {
			var player = new Player {
				Id = i + leftPlayerCount,
				Group = group,
				Side = 1
			};
			state.Add( player );
			boat.RightSide.Players.Add( player );
			this.rowTimings.Add( new RowTiming {
				Player = player
			} );
		}
		GameObject.FindObjectOfType<ViewManager>().AddAiGameState( state );
	}

	void FixedUpdate () {
		foreach(var rowTiming in this.rowTimings) {
			rowTiming.TimeUmtilNextRowing -= Time.fixedDeltaTime;
			if(rowTiming.TimeUmtilNextRowing < Mathf.Epsilon) {
				GetComponent<Boat>().Row( rowTiming.Player, Random.Range(0.8f, 2.0f) );
				rowTiming.TimeUmtilNextRowing += Random.Range( RowMinMaxTime.x, RowMinMaxTime.y );
			}
		}
	}
}
