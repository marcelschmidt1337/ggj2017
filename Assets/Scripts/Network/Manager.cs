using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class Manager : NetworkManager
{

	private string received = "NOTHING"; 
  private void OnGUI ()
  {
	if (GUILayout.Button ("Server"))
	{
	  StartServer ();
	  NetworkServer.RegisterHandler (MsgType.Highest + 1, ReceiveCommand);

	}
	else if (GUILayout.Button ("Client"))
	{
	  client = StartClient ();
	}

	if (client != null && GUILayout.Button ("SEND"))
	{
	  client.Send (MsgType.Highest + 1, new StringMessage ("UNITY SUCKTT!"));
	}
		GUILayout.Label(received); 


  }

  private void ReceiveCommand (NetworkMessage netMsg)
  {
		received = netMsg.ReadMessage<StringMessage>().value; 
		Debug.LogError (received);
  }
}