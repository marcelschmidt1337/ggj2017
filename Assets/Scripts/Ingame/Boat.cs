using System;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {
	
	public class Side {

		string id;

		public Side(string id) {
			this.id = id;
		}

		public Action<float> ExecuteRow;

		class PlayerRowTiming {
			public Player Player;
			public float Time;
		}

		public List<Player> Players = new List<Player>();
		List<PlayerRowTiming> rowTimings = new List<PlayerRowTiming>();
		
		bool HasPlayerRowed(Player player) {
			return rowTimings.Find( (rowTiming) => rowTiming.Player.Id == player.Id ) != null;
		}

		bool HaveAllPlayersRowed () {
			foreach(Player player in Players) {
				if (!HasPlayerRowed( player )) {
					return false;
				}
			}
			return true;
		}

		void SavePlayerRowTiming(Player player) {
			rowTimings.Add( new PlayerRowTiming { Player = player, Time = Time.timeSinceLevelLoad } );
		}

		public void Row(Player player) {
			if (!HasPlayerRowed( player )) {
				SavePlayerRowTiming( player );
				if (HaveAllPlayersRowed()) {
					Debug.LogWarningFormat( "Side {0} All Players Rowed At {1}!", id, Time.timeSinceLevelLoad );
					RowNow();
				}
			}
		}

		bool HasTimedOut () {
			if(rowTimings.Count == 0) {
				return false;
			}
			float currentTime = Time.timeSinceLevelLoad;
			float timeOfFirstRowing = rowTimings[0].Time;
			float elapsedTime = currentTime - timeOfFirstRowing;
			return (elapsedTime >= Balancing.Instance.RowTimeOut);
		}

		public void OnFixedUpdate () {
			if (HasTimedOut()) {
				Debug.LogErrorFormat( "Side {0} Timed Out At {1}!", id, Time.timeSinceLevelLoad );
				RowNow();
			}
		}

		float GetAverageNeededTimeForRowing () {
			float totalTime = 0.0f;
			foreach(var rowTiming in this.rowTimings) {
				totalTime += rowTiming.Time - this.rowTimings[0].Time;
			}

			return totalTime / this.rowTimings.Count;
		}

		void RowNow () {
			Balancing balancing = Balancing.Instance;

			float numPlayers = this.Players.Count;
			float maxPower = balancing.MaxRowPowerBaseValue + balancing.MaxRowPowerPerPlayerValue * numPlayers;

			float numRowedPlayers = this.rowTimings.Count;
			float powerFactor = balancing.PowerFactorCurve.Evaluate( numRowedPlayers / numPlayers );

			float averageRowTime = GetAverageNeededTimeForRowing();
			float timingFactor = balancing.TimeFactorCurve.Evaluate( averageRowTime / balancing.RowTimeOut );

			var power = maxPower * powerFactor * timingFactor;


			Debug.LogFormat( "Side {0} Rowed with {1} Force. {2}/{3} Players Rowed with a timing of {4}", id, power, numRowedPlayers, numPlayers, averageRowTime );

			this.rowTimings.Clear();

			ExecuteRow(power);
		}
	}

	public Side LeftSide = new Side("Left");
	public Side RightSide = new Side("Right");

	List<BoatRow> InstantiatedRows = new List<BoatRow>(); 

	public GameObject RowPrefab;
	public Vector3 FirstRowPosition;
	public Vector3 RowOffset;
	public GameObject BoatEndPiece;

	RowingView GetRower(Player player) {
		foreach(var row in InstantiatedRows) {
			if(row.LeftSeat.Player == player) {
				return row.LeftSeat.rowingView;
			}
			if (row.RightSeat.Player == player) {
				return row.RightSeat.rowingView;
			}
		}
		return null;
	}

	public void Row(Player player, float rowDuration) {
		switch (player.Side) {
			case 0:
				LeftSide.Row( player );
				break;
			case 1:
				RightSide.Row( player );
				break;
			default:
				throw new System.NotSupportedException();
		}
		// get rower view
		var rower = GetRower( player );
		rower.SetAnimationDuration( rowDuration );
		rower.StartRowing();
	}

	void Start () {
		LeftSide.ExecuteRow = OnLeftSideRowed;
		RightSide.ExecuteRow = OnRightSideRowed;
	}

	void FixedUpdate () {
		this.LeftSide.OnFixedUpdate();
		this.RightSide.OnFixedUpdate();
	}

	void OnLeftSideRowed(float force) {
		GetComponent<BoatMovement>().RowLeft( force );
	}

	void OnRightSideRowed(float force) {
		GetComponent<BoatMovement>().RowRight( force );
	}

	public void UpdateView (ViewInfo.Boat boatInfo) {
		int numRows = boatInfo.Rows.Count;
		if(this.InstantiatedRows.Count > 0) {
			for(int i = 0; i < this.InstantiatedRows.Count; i++) {
				GameObject.Destroy( this.InstantiatedRows[i].gameObject );
			}
			this.InstantiatedRows.Clear();
		}
		for(int i = 0; i < numRows; i++) {
			var rowInstance = GameObject.Instantiate( this.RowPrefab, transform );
			var rowScript = rowInstance.GetComponent<BoatRow>();
			rowInstance.transform.localPosition = this.FirstRowPosition + i * this.RowOffset;
			rowScript.UpdateView( boatInfo.Rows[i] );
			this.InstantiatedRows.Add( rowScript );
		}
		this.BoatEndPiece.transform.localPosition = this.FirstRowPosition + (numRows - 1) * this.RowOffset;
	}
}
