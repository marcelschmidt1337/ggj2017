using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class TestView : NetworkManager
{
	public GameState GameState { get; private set; }

	public List<RowingView> Views;

	ViewManager _ViewManager;
	ViewManager ViewManager
	{
		get
		{
			if (_ViewManager == null)
				_ViewManager = GameObject.FindObjectOfType<ViewManager> ();
			return _ViewManager;
		}
	}

	public void StartWorldView ()
	{
		StartClient ();
		StartCoroutine (Co_WaitForClientReady ());

		GameState = new GameState ();
	}

	public void SendStartGame ()
	{
		client.Send (MsgType.Ready, new EmptyMessage ());
		GameState.StartGame ();
	}

	public void SendGameOver (int winnerGroupId)
	{
		Debug.Log ("Send Game Over - Winner: " + winnerGroupId);
		client.Send ((short)CustomMsgType.GameOver, new IntegerMessage (winnerGroupId));
		GameState.GameOver (winnerGroupId);
	}

	private IEnumerator Co_WaitForClientReady ()
	{
		while (client == null || !client.isConnected)
		{
			yield return null;
		}
		Debug.Log ("registering GameView client...");
		client.Send ((short)CustomMsgType.RegisterView, new IntegerMessage (0));
		client.RegisterHandler ((short)CustomMsgType.UpdateFromFinishedRow, OnViewUpdate);
		client.RegisterHandler ((short)CustomMsgType.UpdateGameState, OnGameStateUpdate);
		client.RegisterHandler ((short)CustomMsgType.StartGame, delegate { });
		client.RegisterHandler ((short)CustomMsgType.GameOver, delegate { });
	}

	private void OnViewUpdate (NetworkMessage netMsg)
	{
		var rowerData = netMsg.ReadMessage<ViewUpdateRowerMessage> ();
		this.ViewManager.PlayerRowed (GameState.GetPlayer (rowerData.Id), rowerData.Duration);
	}

	private void OnGameStateUpdate (NetworkMessage netMsg)
	{
		var msg = netMsg.ReadMessage<UpdateGameStateMessage> ();
		GameState.SetPlayerState (msg.ConnectedPlayers);
	}
}
