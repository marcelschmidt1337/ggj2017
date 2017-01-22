using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class ViewInfo
{
	[System.Serializable]
	public class Boat
	{
		[System.Serializable]
		public class Row
		{
			[System.Serializable]
			public class Seat
			{
				public Player Player;
			}

			public Seat LeftSeat = new Seat();
			public Seat RightSeat = new Seat();
		}

		public List<Row> Rows = new List<Row>();

		public void AddPlayer (Player player) {
			for(int i = 0; i < this.Rows.Count; i++) {
				switch (player.Side) {
					case 0:
						if (this.Rows[i].LeftSeat.Player == null) {
							this.Rows[i].LeftSeat.Player = player;
							return;
						}
						break;
					case 1:
						if (this.Rows[i].RightSeat.Player == null) {
							this.Rows[i].RightSeat.Player = player;
							return;
						}
						break;
				}
			}
			PointerEventData eventData;
			var row = new Row();
			switch (player.Side) {
				case 0:
					row.LeftSeat.Player = player;
					break;
				case 1:
					row.RightSeat.Player = player;
					break;
			}
			this.Rows.Add( row );
		}
	}

	public Boat FirstBoat = new Boat();
	public Boat SecondBoat = new Boat();
}

public class ViewManager : MonoBehaviour {

	public Boat BoatA;
	public Boat BoatB;

	public List<Player> TestGameState;
	public bool UpdateTestView;

	public void PlayerRowed (Player player, float rowDuration) {
		switch (player.Group) {
			case 0:
				this.BoatA.Row( player, rowDuration );
				break;
			case 1:
				this.BoatB.Row( player, rowDuration );
				break;
		}
	}

	List<Player> aiState = new List<Player>();
	public void AddAiGameState (List<Player> players) {
		this.aiState.AddRange( players );
		UpdateView( GetViewInfoForGameState( this.aiState ) );
	}

	void Update () {
		if (this.UpdateTestView) {
			UpdateView( GetViewInfoForGameState(this.TestGameState) );
			this.UpdateTestView = false;
		}
	}

	void Start () {
		var gameState = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<TestView>().GameState;
		gameState.OnGameStarted += CreateViews;
	}

	private void CreateViews () {
		var gameState = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<TestView>().GameState;
		var state = gameState.GetPlayerState();
		var viewInfo = GetViewInfoForGameState( state );

		UpdateView( viewInfo );	
	}

	public void UpdateView (ViewInfo viewInfo) {
		this.BoatA.UpdateView( viewInfo.FirstBoat );
		this.BoatB.UpdateView( viewInfo.SecondBoat );
	}

	private ViewInfo GetViewInfoForGameState(List<Player> state) {
		var viewInfo = new ViewInfo();

		for(int i = 0; i < state.Count; i++) {
			var player = state[i];
			ViewInfo.Boat boat = null;
			switch (player.Group) {
				case 0:
					boat = viewInfo.FirstBoat;
					break;
				case 1:
					boat = viewInfo.SecondBoat;
					break;
			}

			boat.AddPlayer( player );
		}

		return viewInfo;
	}
}
