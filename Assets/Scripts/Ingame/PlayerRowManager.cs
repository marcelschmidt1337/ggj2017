using System.Collections.Generic;
using UnityEngine;

public class PlayerRowManager : MonoBehaviour {

	public List<Boat> Boats;

	public void PlayerRowed(Player player) {
		this.Boats[player.Group].Row( player );
	}
}
