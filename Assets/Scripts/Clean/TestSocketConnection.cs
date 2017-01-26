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
		Debug.Log("Message Received: " + obj); 
	}

	private void SendStartMessage()
	{
		this.Text.text = "Connection established"; 
		Debug.Log("Conenction established");
		this.Connection.Send("Hello World"); 
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI() {
		
	}
}
