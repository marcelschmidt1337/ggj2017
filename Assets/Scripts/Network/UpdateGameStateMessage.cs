using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using LitJson; 

public class UpdateGameStateMessage : MessageBase
{
	public List<Player> ConnectedPlayers;

	public override void Serialize (NetworkWriter writer)
	{
		string json = JsonMapper.ToJson(ConnectedPlayers); 
			//JsonUtility.ToJson (ConnectedPlayers);
		Debug.LogError ("Send JSON: " + json);
		writer.Write (json);
	}

	public override void Deserialize (NetworkReader reader)
	{
		string json = reader.ReadString ();
		Debug.LogError("Received JSON: " + json); 
		ConnectedPlayers = JsonMapper.ToObject<List<Player>> (json);
	}
}
