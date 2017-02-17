using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSocketConnection : MonoBehaviour {

	public UnityEngine.UI.Text Text;

	private WebSocketConnection Connection; 

	// Use this for initialization
	void Start () {
		this.Connection = new WebSocketConnection("ws://localhost:7777");
		//this.Connection = new WebSocketConnection("ws://echo.websocket.org"); 
		this.Connection.OnMessageReceived += PrintMessage;
		this.Connection.Connect(SendStartMessage);
		
	}

	private void PrintMessage(string obj)
	{
		this.Text.text = obj;
		var cmd = JsonMapper.ToObject<Command>(obj); 
		Debug.Log("Message Received: Id: " + cmd.CommandId);
		StartCoroutine(Pull()); 
	}

	private IEnumerator Pull()
	{
		while (true) {
			
			yield return new WaitForSeconds(1);
			SendStartMessage(); 
			/*var startRow = new Command() { CommandId = CommandType.StartRow };
			var json = JsonMapper.ToJson(startRow);
			Debug.Log("Send StartRow Message: " + json);
			this.Connection.Send(json);
			yield return new WaitForSeconds(1);
			var stopRow = new Command() { CommandId = CommandType.StopRow };
			this.Connection.Send(JsonMapper.ToJson(stopRow));
			Debug.Log("Pull finished"); */
		}
	}

	private void SendStartMessage()
	{
		this.Text.text = "Connection established"; 
		Debug.Log("Conenction established");
		var cmd = new Command() { CommandId = CommandType.RegisterClient };
		var json = JsonMapper.ToJson(cmd); 
		this.Connection.Send(json); 
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI() {
		
	}

	void OnDestroy() {
		StopAllCoroutines(); 
	}
}
