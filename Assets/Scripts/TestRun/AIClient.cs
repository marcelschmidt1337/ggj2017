using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

public class AIClient : NetworkManager
{
	public RowingView View;
	public Slider Slider;
	public UIManager UIManager;
	private float CountdownMove;
	private float CountdownWait;
	private float MoveDuration;
	private float WaitDuration;

	bool IsMoving = false;
	private bool GameIsStarted;
	private float Wait = 2; 


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


	void Update() {
		/*if (Wait > 0) {
			Wait -= Time.deltaTime; 

		} else if (Wait > -10 && Wait <0) {
			Wait = -100;
			if (useWebSockets) {
				StartClient("35.157.62.87");
			}
			else {
				StartClient("localhost");
			}
			StartCoroutine(WaitForConnect());
		}*/
	}

	private IEnumerator WaitForConnect()
	{
		
		while (client == null || !client.isConnected) {
			yield return null; 
		}
		var rnd = new System.Random(); 
		JoinGroup(rnd.Next(0, 2));
		var waitASec = new WaitForSeconds(1);
		yield return waitASec; 
		JoinSide(0);

		
	}

	public IEnumerator AIRow() {
		var rnd = new System.Random();
		while (GameIsStarted) {

			client.Send((short)CustomMsgType.StartRowing, new IntegerMessage(0));
			Slider.value = 1;
			yield return new WaitForSeconds(rnd.Next(4, 20) / 10.0f);
			client.Send((short)CustomMsgType.StopRowing, new IntegerMessage(0));
			Slider.value = 0;
		}
	}

	public void StartClient (string ip)
	{
		if (client == null)
		{
			networkAddress = ip;
			StartClient ();
			client.RegisterHandler ((short)CustomMsgType.StartGame, StartGame);
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

	private void StartGame (NetworkMessage netMsg)
	{
		Debug.Log("CLIENT GAME STARTED");
		GameIsStarted = true; 
		UIManager.ShowClientUi();
		StartCoroutine(AIRow()); 
	}


	private IEnumerator WaitForMoveReady()
	{
		yield return new WaitForSeconds(0.2f);
		IsMoving = false;
	}
}
