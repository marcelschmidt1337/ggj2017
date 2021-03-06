﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

public class TestClient : NetworkManager
{
	public RowingView View;
	public Slider Slider;
	public UIManager UIManager;
	public GameObject Yeah;
	private float CountdownMove;
	private float CountdownWait;
	private float MoveDuration;
	private float WaitDuration;

	bool IsMoving = false;

	private int GroupId = PlayerConstants.NO_GROUP;

	public void JoinGroup (int group)
	{
		if (client != null)
		{
			GroupId = group;
			client.Send ((short)CustomMsgType.JoinGroup, new IntegerMessage (group));
		}
	}

	public void LeaveGroup()
	{
		if (client != null)
		{
			GroupId = PlayerConstants.NO_GROUP;
			client.Send ((short)CustomMsgType.LeaveGroup, new EmptyMessage ());
		}
	}

	public void JoinSide(int side)
	{
		if (client != null)
		{
			client.Send((short)CustomMsgType.JoinSide, new IntegerMessage(side));
		}
	}

	public void LeaveSide()
	{
		if (client != null)
		{
			client.Send((short)CustomMsgType.LeaveSide, new EmptyMessage());
		}
	}

	public void ChangeHat(int hatIndex)
	{
		if (client != null)
		{
			client.Send((short)CustomMsgType.ChangeHat, new IntegerMessage(hatIndex));
			UIManager.ShowWaiting(false);
		}
	}

	public void StartClient(string ip)
	{
		if (client == null)
		{
			networkAddress = ip;
			StartClient ();
			client.RegisterHandler ((short)CustomMsgType.StartGame, StartGame);
			client.RegisterHandler ((short)CustomMsgType.GameOver, GameOver);
		}

		if (client != null)
		{
			Debug.Log("Started client and connecting to " + ip);
		}
		else
		{
			Debug.LogError("Failed to start clien!");
		}
	}

	private void StartGame(NetworkMessage netMsg)
	{
		Debug.Log("CLIENT GAME STARTED");
		UIManager.ShowClientUi();
	}

	private void GameOver (NetworkMessage netMsg)
	{
		var winnerGroupId = netMsg.ReadMessage<IntegerMessage> ().value;

		if (GroupId == winnerGroupId)
		{
			Debug.Log ("Game Over! You won! :)");
			UIManager.ShowGameOverScreen (true);
		}
		else
		{
			UIManager.ShowGameOverScreen (false);
			Debug.Log ("Game Over! You suck, loser! :O");
		}
	}

	public void ValueChanged (float value)
	{
		if (client == null || !client.isConnected)
		{
			return;
		}

		if (value > .98)
		{
			Yeah.SetActive(false);
			Yeah.SetActive(true);
		}

		if (!IsMoving)
		{
			client.Send((short)CustomMsgType.StartRowing, new IntegerMessage(0));
			IsMoving = true;
		}
		else
		{
			if (value > 0.98f)
			{
				client.Send((short)CustomMsgType.StopRowing, new IntegerMessage(0));
				StartCoroutine(WaitForMoveReady());
			}
		}
	}

	public void OnSliderRelease()
	{
		if (Slider.value > .98)
		{
			Slider.value = 0;
		}
	}

	private IEnumerator WaitForMoveReady()
	{
		yield return new WaitForSeconds(0.2f);
		IsMoving = false;
	}
}
