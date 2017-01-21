using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class TestView : NetworkManager {

	public List<RowingView> Views; 
	// Use this for initialization
	void Start () {
		StartClient();
		StartCoroutine(Co_WaitForClientReady()); 
		
	}

	private IEnumerator Co_WaitForClientReady()
	{
		while (!client.isConnected) {
			yield return null; 
		}
		client.Send((short)CustomMsgType.RegisterView, new IntegerMessage(0));
		client.RegisterHandler((short)CustomMsgType.UpdateFromFinishedRow, OnViewUpdate);
	}

	private void OnViewUpdate(NetworkMessage netMsg)
	{
		var rowerData = netMsg.ReadMessage<ViewUpdateRowerMessage>();
		foreach (var rower in Views) {
			if (rower.Id == rowerData.Id) {
				rower.SetAnimationDuration(rowerData.Duration);
				rower.StartRowing();
				StartCoroutine(Co_WaitForAnimationFinish(rowerData)); 
			}
		}
	}


	private IEnumerator Co_WaitForAnimationFinish(ViewUpdateRowerMessage rower)
	{
		yield return new WaitForSeconds(rower.Duration);
		Views[rower.Id].StopRowing(); 
	}

}
