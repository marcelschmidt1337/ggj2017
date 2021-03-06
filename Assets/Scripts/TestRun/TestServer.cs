﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class TestServer : NetworkManager
{
	private Dictionary<int, RowerData> RowerData = new Dictionary<int, RowerData> ();

	private bool ServerStarted = false;

	private int GameViewClientId;
	private bool GameViewClientSet = false;

	private GameState GameState;

	// Use this for initialization
	void Start ()
	{
		TryStartServer ();


		GameState = new GameState ();
	}

	public void TryStartServer ()
	{
		if (ServerStarted)
		{
			return;
		}

		ServerStarted = StartServer ();

		if (!ServerStarted)
		{
			Debug.LogError ("Failed to start server!");
			return;
		}

		NetworkServer.RegisterHandler ((short)CustomMsgType.StartRowing, OnStartRowing);
		NetworkServer.RegisterHandler ((short)CustomMsgType.StopRowing, OnStopRowing);
		NetworkServer.RegisterHandler ((short)CustomMsgType.RegisterView, (netMsg) =>
		 {
			 GameViewClientId = netMsg.conn.connectionId;
			 GameViewClientSet = true;
		 });
		NetworkServer.RegisterHandler ((short)CustomMsgType.JoinGroup, OnJoinGroup);
		NetworkServer.RegisterHandler ((short)CustomMsgType.LeaveGroup, OnLeaveGroup);
		NetworkServer.RegisterHandler ((short)CustomMsgType.JoinSide, OnJoinSide);
		NetworkServer.RegisterHandler ((short)CustomMsgType.LeaveSide, OnLeaveSide);
		NetworkServer.RegisterHandler ((short)CustomMsgType.ChangeHat, OnChangeHat);
		NetworkServer.RegisterHandler ((short)CustomMsgType.GameOver, OnGameOver);

		NetworkServer.RegisterHandler ((short)CustomMsgType.Test, (netMsg) =>
		{
			var msg = netMsg.ReadMessage<IntegerMessage> ();
			NetworkServer.SendToClient (GameViewClientId, (short)CustomMsgType.Test, msg);
		});

		Debug.Log ("Server started!");
	}

	private void SendGameStateToView ()
	{
		UpdateGameStateMessage msg = new UpdateGameStateMessage ();
		msg.ConnectedPlayers = GameState.GetPlayerState ();
		NetworkServer.SendToClient (GameViewClientId, (short)CustomMsgType.UpdateGameState, msg);
	}

	private void OnJoinGroup (NetworkMessage netMsg)
	{
		var playerId = netMsg.conn.connectionId;
		var group = netMsg.ReadMessage<IntegerMessage> ().value;

		if (!GameState.HasPlayer (playerId))
		{
			GameState.AddPlayer (playerId, groupId: group);
		}
		else
		{
			GameState.SetGroupId (playerId, group);
		}
	}

	private void OnLeaveGroup (NetworkMessage netMsg)
	{
		var playerId = netMsg.conn.connectionId;

		GameState.SetGroupId (playerId, PlayerConstants.NO_GROUP);
	}

	private void OnJoinSide (NetworkMessage netMsg)
	{
		var playerId = netMsg.conn.connectionId;
		var side = netMsg.ReadMessage<IntegerMessage> ().value;

		if (!GameState.HasPlayer (playerId))
		{
			GameState.AddPlayer (playerId, sideId: side);
		}
		else
		{
			GameState.SetSideId (playerId, side);
		}

		SendGameStateToView ();
	}

	private void OnLeaveSide (NetworkMessage netMsg)
	{
		var playerId = netMsg.conn.connectionId;
		GameState.SetSideId (playerId, PlayerConstants.NO_SIDE);

		SendGameStateToView ();
	}

	private void OnChangeHat (NetworkMessage netMsg)
	{
		var playerid = netMsg.conn.connectionId;
		GameState.SetHatIndex (playerid, netMsg.ReadMessage<IntegerMessage> ().value);
		SendGameStateToView ();
	}

	private void OnGameOver (NetworkMessage netMsg)
	{
		if (netMsg.conn.connectionId == GameViewClientId)
		{
			int winnerId = netMsg.ReadMessage<IntegerMessage> ().value;
			GameState.GameOver (winnerId);
			NetworkServer.SendToAll ((short)CustomMsgType.GameOver, new IntegerMessage (winnerId));
		}
	}

	public override void OnClientConnect (NetworkConnection conn)
	{
		GameState.AddPlayer (conn.connectionId);
		base.OnClientConnect (conn);
	}

	public override void OnClientDisconnect (NetworkConnection conn)
	{
		GameState.RemovePlayer (conn.connectionId);
		base.OnClientDisconnect (conn);
	}

	private void OnStopRowing (NetworkMessage netMsg)
	{
		var data = RowerData[netMsg.conn.connectionId];
		data.Stop = DateTime.Now;

		var updateMsg = new ViewUpdateRowerMessage { Id = netMsg.conn.connectionId, Duration = (float)(data.Stop - data.Start).TotalSeconds };

		if (GameViewClientSet)
		{
			NetworkServer.SendToClient (GameViewClientId, (short)CustomMsgType.UpdateFromFinishedRow, updateMsg);
		}
	}

	private void OnStartRowing (NetworkMessage netMsg)
	{
		RowerData[netMsg.conn.connectionId] = new RowerData { Start = DateTime.Now };
	}

	public override void OnServerReady (NetworkConnection conn)
	{
		if (conn.connectionId == GameViewClientId)
		{
			GameState.StartGame ();
			NetworkServer.SendToAll ((short)CustomMsgType.StartGame, new EmptyMessage ());
		}
		base.OnServerReady (conn);
	}
}
