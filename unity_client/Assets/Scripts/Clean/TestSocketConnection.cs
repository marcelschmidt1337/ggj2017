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
		Debug.Log("Message Received: Id: " + cmd.Id + " data: " + cmd.Data); 
	}

	private void SendStartMessage()
	{
		this.Text.text = "Connection established"; 
		Debug.Log("Conenction established");
		var cmd = new Command() { Id = CommandType.RegisterClient, Data = "TestData" };
		var json = JsonMapper.ToJson(cmd); 
		this.Connection.Send(json); 
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI() {
		
	}
}
