using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class Manager : NetworkManager
{
	enum CustomMsgType
	{
		JoinGroup = MsgType.Highest + 1,
		LeaveGroup,
		JoinSide,
		LeaveSide,
		PlayerStatus,

		Test = 99,
		RegisterView = 100
	}

	private bool serverStarted = false;
	private int gameViewClientId;

	public void JoinGroup (int group)
	{
		if (client != null)
		{
			client.Send ((short)CustomMsgType.JoinGroup, new IntegerMessage (group));
		}
	}

	public void LeaveGroup ()
	{
		if (client != null)
		{
			client.Send ((short)CustomMsgType.LeaveGroup, new EmptyMessage ());
		}
	}

	public void JoinSide (int side)
	{
		if (client != null)
		{
			client.Send ((short)CustomMsgType.JoinSide, new IntegerMessage (side));
		}
	}

	public void LeaveSide ()
	{
		if (client != null)
		{
			client.Send ((short)CustomMsgType.LeaveSide, new EmptyMessage ());
		}
	}

	public void StartGameServer ()
	{
		TryStartServer ();
		Debug.Log ("Server started!");
	}

	public void StartClient (string ip)
	{
		if (client == null)
		{
			networkAddress = ip;
			StartClient ();
		}

		if (client != null)
		{
			Debug.Log ("Started client and connecting to " + ip);
		}
		else
		{
			Debug.LogError ("Failed to start clien!");
		}
	}

	public void StartGameViewClient (string ip)
	{
		StartClient (ip);

		if (client == null)
		{
			Debug.LogError ("Client not started!");
			return;
		}

		Debug.Log ("registering GameView client...");
		client.Send ((short)CustomMsgType.RegisterView, new IntegerMessage (0));
		client.RegisterHandler ((short)CustomMsgType.Test, (netMsg) =>
		 {

		 });
	}

	private void TryStartServer ()
	{
		if (serverStarted)
		{
			return;
		}


		serverStarted = StartServer ();

		if (!serverStarted)
		{
			Debug.LogError ("Failed to start server!");
			return;
		}
		NetworkServer.RegisterHandler ((short)CustomMsgType.JoinGroup, OnJoinGroup);
		NetworkServer.RegisterHandler ((short)CustomMsgType.LeaveGroup, OnLeaveGroup);
		NetworkServer.RegisterHandler ((short)CustomMsgType.JoinSide, OnJoinSide);
		NetworkServer.RegisterHandler ((short)CustomMsgType.LeaveSide, OnLeaveSide);

		NetworkServer.RegisterHandler ((short)CustomMsgType.Test, (netMsg) =>
		{
			var msg = netMsg.ReadMessage<IntegerMessage> ();
			NetworkServer.SendToClient (gameViewClientId, (short)CustomMsgType.Test, msg);
		});
		NetworkServer.RegisterHandler ((short)CustomMsgType.RegisterView, (netMsg) =>
		{
			gameViewClientId = netMsg.conn.connectionId;
		});

		Debug.Log ("Server started!");
	}

	private void OnJoinGroup (NetworkMessage netMsg)
	{
		var gameState = GetComponent<GameState> ();
		var playerId = netMsg.conn.connectionId;
		var group = netMsg.ReadMessage<IntegerMessage> ().value;

		if (!gameState.HasPlayer (playerId))
		{
			gameState.AddPlayer (playerId, groupId: group);
		}
		else
		{
			gameState.SetSideId (playerId, group);
		}
	}

	private void OnLeaveGroup (NetworkMessage netMsg)
	{
		var gameState = GetComponent<GameState> ();
		var playerId = netMsg.conn.connectionId;

		gameState.SetGroupId (playerId, Player.NO_GROUP);
	}

	private void OnJoinSide (NetworkMessage netMsg)
	{
		var gameState = GetComponent<GameState> ();
		var playerId = netMsg.conn.connectionId;
		var side = netMsg.ReadMessage<IntegerMessage> ().value;

		if (!gameState.HasPlayer (playerId))
		{
			gameState.AddPlayer (playerId, sideId: side);
		}
		else
		{
			gameState.SetSideId (playerId, side);
		}
	}

	private void OnLeaveSide (NetworkMessage netMsg)
	{
		var gameState = GetComponent<GameState> ();
		var playerId = netMsg.conn.connectionId;
		gameState.SetSideId (playerId, Player.NO_SIDE);
	}


	public override void OnClientConnect (NetworkConnection conn)
	{
		var gameState = GetComponent<GameState> ();
		gameState.AddPlayer (conn.connectionId);
		base.OnClientConnect (conn);
	}

	public override void OnClientDisconnect (NetworkConnection conn)
	{
		var gameState = GetComponent<GameState> ();
		gameState.RemovePlayer (conn.connectionId);
		base.OnClientDisconnect (conn);
	}
}