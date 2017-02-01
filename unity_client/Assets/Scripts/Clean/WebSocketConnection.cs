using System;
using System.Collections;
using UnityEngine;


public class WebSocketConnection {

	public event Action<string> OnMessageReceived = (data) => { }; 
	private WebSocket Socket;
	private string Adress;

	public WebSocketConnection(string adress) {
		this.Adress = adress;
		
	}

	public void Connect(Action callback) {
		this.Socket = new WebSocket(new Uri(this.Adress));
		CoRoutineProvider.Instance.StartCoroutine(Co_Connect(callback)); 
	}

	private IEnumerator Co_Connect(Action callback) {
		yield return this.Socket.Connect();
		callback();
		while (true) {
			string reply = this.Socket.RecvString();
			if (reply != null) {
				Debug.Log("Received: " + reply);
				OnMessageReceived(reply); 
			}
			if (this.Socket.error != null) {
				Debug.LogError("Error: " + this.Socket.error);
				break;
			}
			yield return 0;
		}
		this.Socket.Close();
	}

	public void Send(string data){

		this.Socket.SendString(data); 
	}


}
