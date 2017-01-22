using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class TestClient : NetworkManager
{
	public RowingView View;
	private float CountdownMove;
	private float CountdownWait;
	private float MoveDuration;
	private float WaitDuration;

	bool IsMoving = false;

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
		Debug.LogError ("CLIENT GAME STARTED");
	}

	public void ValueChanged (float value)
	{

	}

	void Update ()
	{
		if (client == null || !client.isConnected)
		{
			return;
		}
		if (!IsMoving)
		{
			if (CountdownWait <= 0)
			{
				client.Send ((short)CustomMsgType.StartRowing, new IntegerMessage (0));
				View.StartRowing ();
				MoveDuration = 2; // Random.Range(0.5f, 4);
				View.SetAnimationDuration (MoveDuration);
				CountdownMove = MoveDuration;
				IsMoving = true;
			}
			else
			{
				CountdownWait -= Time.deltaTime;
			}
		}
		else
		{
			CountdownMove -= Time.deltaTime;
			if (CountdownMove <= 0)
			{
				WaitDuration = Random.Range (1, 3);
				CountdownWait = 6; // WaitDuration;
				client.Send ((short)CustomMsgType.StopRowing, new IntegerMessage (0));
				View.StopRowing ();
				IsMoving = false;
			}
		}
	}
}
