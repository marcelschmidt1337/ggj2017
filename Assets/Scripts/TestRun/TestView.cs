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
	// Use this for initialization


	public void StartWorldView ()
	{
		StartClient ();
		StartCoroutine (Co_WaitForClientReady ());

		GameState = new GameState ();
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
	}

	private void OnViewUpdate (NetworkMessage netMsg)
	{
		var rowerData = netMsg.ReadMessage<ViewUpdateRowerMessage> ();
		foreach (var rower in Views)
		{
			if (rower.Id == rowerData.Id)
			{
				rower.SetAnimationDuration (rowerData.Duration);
				rower.StartRowing ();
				StartCoroutine (Co_WaitForAnimationFinish (rowerData));
			}
		}
	}

	private void OnGameStateUpdate (NetworkMessage netMsg)
	{
		var msg = netMsg.ReadMessage<UpdateGameStateMessage> ();
		GameState.SetPlayerState (msg.ConnectedPlayers);
	}


	private IEnumerator Co_WaitForAnimationFinish (ViewUpdateRowerMessage rower)
	{
		yield return new WaitForSeconds (rower.Duration);
		Views[rower.Id].StopRowing ();
	}
}
