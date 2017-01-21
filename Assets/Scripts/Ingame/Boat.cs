using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {

	public class Side
	{
		class PlayerRowTiming
		{
			Player Player;
			float Time;
		}

		public List<Player> Players = new List<Player>();

		// Welcher Spieler hat schon gerudert? (Wann hat der Spieler gerudert?)
		// Welcher Spieler muss noch rudern?
		// Balancing Variablen (BasePower, MaxPower usw.)

		public void Row(Player player) {
			// speichern, dass der Spieler gerudert hat. + Zeit merken.
			// prüfen, ob das der letzte Spieler war, der noch rudern musste.
			//		falls ja: RowNow().
		}

		public void OnFixedUpdate () {
			// timeout handlen.
			//		falls timeout: RowNow().
		}

		void RowNow () {
			// Formeln -> Power berechnen
		}
	}

	public Side LeftSide;
	public Side RightSide;

	public void Row(Player player) {
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
	}

	void FixedUpdate () {
		this.LeftSide.OnFixedUpdate();
		this.RightSide.OnFixedUpdate();
	}
}
