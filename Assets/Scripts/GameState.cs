using UnityEngine;
using System.Collections.Generic;
using System;

public class GameState : MonoBehaviour
{
	public event Action OnGameStateChanged = delegate { };

	List<Player> connectedPlayers = new List<Player> ();

	public bool HasPlayer (int id)
	{
		return FindPlayer (id) != null;
	}

	public void AddPlayer (int id, int groupId = -1, int sideId = -1)
	{
		var newPlayer = new Player ();
		newPlayer.Id = id;

		connectedPlayers.Add (newPlayer);
	}

	public void RemovePlayer (int id)
	{
		var player = FindPlayer (id);
		if (player != null)
		{
			connectedPlayers.Remove (player);
		}
	}

	public void SetGroupId (int playerId, int groupId)
	{
		var player = FindPlayer (playerId);
		if (player != null)
		{
			player.Group = groupId;
		}
	}

	public int GetGroupId (int playerId)
	{
		var player = FindPlayer (playerId);
		if (player != null)
		{
			return player.Group;
		}
		else
		{
			return -1;
		}
	}

	public void SetSideId (int playerId, int sideId)
	{
		var player = FindPlayer (playerId);
		if (player != null)
		{
			player.Side = sideId;
		}
	}

	public int GetSideId (int playerId)
	{
		var player = FindPlayer (playerId);
		if (player != null)
		{
			return player.Side;
		}
		else
		{
			return -1;
		}
	}

	private Player FindPlayer (int id)
	{
		return connectedPlayers.Find (p => p.Id == id);
	}
}
