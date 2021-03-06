﻿using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameState
{
	public event Action OnGameStateChanged = delegate { };
	public event Action OnGameStarted = delegate { };
	public event Action<int> OnGameOver = delegate { };
	public int WinnerGroupId { get; private set; }

	private List<Player> connectedPlayers = new List<Player> ();

	public GameState ()
	{
		WinnerGroupId = PlayerConstants.NO_GROUP;
	}

	public void SetPlayerState (List<Player> playerState)
	{
		connectedPlayers = playerState;
		OnGameStateChanged ();
	}

	public Player GetPlayer (int id)
	{
		foreach (var player in connectedPlayers)
		{
			if (player.Id == id)
				return player;
		}
		return null;
	}

	public List<Player> GetPlayerState ()
	{
		return new List<Player> (connectedPlayers);
	}

	public bool HasPlayer (int id)
	{
		return FindPlayer (id) != null;
	}

	public void AddPlayer (int id, int groupId = -1, int sideId = -1)
	{
		var newPlayer = new Player ();
		newPlayer.Id = id;
		newPlayer.Group = groupId;
		newPlayer.Side = sideId;

		connectedPlayers.Add (newPlayer);
		OnGameStateChanged ();
	}

	public void RemovePlayer (int id)
	{
		var player = FindPlayer (id);
		if (player != null)
		{
			connectedPlayers.Remove (player);
			OnGameStateChanged ();
		}
	}

	public void SetGroupId (int playerId, int groupId)
	{
		var player = FindPlayer (playerId);
		if (player != null)
		{
			player.Group = groupId;
			OnGameStateChanged ();
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
			OnGameStateChanged ();
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

	public void SetHatIndex (int playerId, int index)
	{
		var player = FindPlayer (playerId);
		if (player != null)
		{
			player.HatIndex = index;
		}
	}

	public int GetPlayerInGroup (int groupId)
	{
		return connectedPlayers.Where (p => p.Group == groupId).Count ();
	}

	public int GetPlayerOnSide (int groupId, int sideId)
	{
		return connectedPlayers.Where (p => p.Group == groupId && p.Side == sideId).Count ();
	}

	private Player FindPlayer (int id)
	{
		return connectedPlayers.Find (p => p.Id == id);
	}

	public void StartGame ()
	{
		Debug.Log ("Game Started!");
		OnGameStarted ();
	}

	public void GameOver (int winnerGroupId)
	{
		WinnerGroupId = winnerGroupId;
		OnGameOver (winnerGroupId);
	}
}
